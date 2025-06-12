﻿using OpenTK.Compute.OpenCL;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OpenCLBenchmark
{
	public class OpenClMemoryRegister
	{
		private string Repopath;
		private ListBox LogList;
		private CLContext Context;
		private CLDevice Device;
		private CLPlatform Platform;





		// ----- ----- ----- ATTRIBUTES ----- ----- ----- \\
		public CLCommandQueue QUE;

		private readonly object _memoryLock = new();
		public List<ClMem> Memory = [];


		// ----- ----- ----- CONSTRUCTORS ----- ----- ----- \\
		public OpenClMemoryRegister(string repopath, CLContext context, CLDevice device, CLPlatform platform, ListBox? logList = null)
		{
			this.Repopath = repopath;
			this.Context = context;
			this.Device = device;
			this.Platform = platform;
			this.LogList = logList ?? new ListBox();

			// Init. queue
			this.QUE = CL.CreateCommandQueueWithProperties(this.Context, this.Device, 0, out CLResultCode error);
			if (error != CLResultCode.Success)
			{
				this.Log("Failed to create CL-CommandQueue.");
			}


		}




		// ----- ----- ----- METHODS ----- ----- ----- \\
		public void Log(string message = "", string inner = "", int indent = 0)
		{
			string msg = "[Memory]: " + new string(' ', indent * 2) + message;

			if (!string.IsNullOrEmpty(inner))
			{
				msg += " (" + inner + ")";
			}

			// Invoke optionally
			if (this.LogList.InvokeRequired)
			{
				// Wenn nicht, rufen wir die Methode über den UI-Thread auf
				this.LogList.Invoke(new MethodInvoker(() =>
				{
					this.InternalLog(msg);
				}));
			}
			else
			{
				// Wenn wir bereits im UI-Thread sind, direkt hinzufügen
				this.InternalLog(msg);
			}
		}

		private void InternalLog(string msg)
		{
			// Add to logList
			this.LogList.Items.Add(msg);

			// Scroll down
			this.LogList.SelectedIndex = this.LogList.Items.Count - 1;
		}

		// Dispose
		public void Dispose()
		{
			// Dispose every memory buffer
			foreach (ClMem mem in this.Memory)
			{
				foreach (CLBuffer buffer in mem.Buffers)
				{
					CLResultCode err = CL.ReleaseMemoryObject(buffer);
					if (err != CLResultCode.Success)
					{
						this.Log("Failed to release buffer", buffer.Handle.ToString("X16"), 1);
					}
				}
			}

		}


		// Free buffer
		public long FreeBuffer(IntPtr pointer, bool readable = false)
		{
			ClMem? mem = this.GetBuffer(pointer);
			if (mem == null)
			{
				// If no buffer found, return 0
				// this.Log("No buffer found to free", pointer.ToString("X16"));
				return 0;
			}

			long freedSizeBytes = mem.Size.ToInt64();

			foreach (CLBuffer buffer in mem.Buffers)
			{
				// Free the buffer
				CLResultCode err = CL.ReleaseMemoryObject(buffer);
				if (err != CLResultCode.Success)
				{
					this.Log("Failed to release buffer", buffer.Handle.ToString("X16"), 1);
				}
			}

			// Remove from memory list
			this.Memory.Remove(mem);

			// Make readable if requested
			if (readable)
			{
				freedSizeBytes /= 1024 * 1024; // Convert to MB
			}

			return freedSizeBytes;
		}


		// Buffer info
		public Type? GetBufferType(IntPtr pointer)
		{
			ClMem? mem = this.GetBuffer(pointer);
			if (mem == null || mem.Buffers.Length < 1)
			{
				this.Log("No memory found for pointer", pointer.ToString("X16"));
				return null;
			}


			// Return the type of the first buffer
			return mem.ElementType;
		}

		public ClMem? GetBuffer(IntPtr pointer)
		{
			// Sperre den Zugriff auf die Memory-Liste während der Iteration
			lock (this._memoryLock)
			{
				// Find by indexHandle of Memory
				foreach (ClMem mem in this.Memory.ToList())
				{
					try
					{
						if (mem.IndexHandle == pointer)
						{
							return mem;
						}
					}
					catch (Exception ex)
					{
						// Log the error if something goes wrong during the comparison
						this.Log("Error comparing memory index handle", ex.Message, 1);
					}
					finally
					{
						//this.Memory.Remove(mem);
					}

				}
			} // Das Schloss wird hier freigegeben

			// Return null if not found
			return null;
		}




		// Single buffer
		public ClMem? PushData<T>(T[] data) where T : unmanaged
		{
			// Check data
			if (data.LongLength < 1)
			{
				return null;
			}

			// Get IntPtr length
			IntPtr length = new(data.LongLength);

			// Create buffer
			CLBuffer buffer = CL.CreateBuffer<T>(this.Context, MemoryFlags.CopyHostPtr | MemoryFlags.ReadWrite, data, out CLResultCode error);
			if (error != CLResultCode.Success)
			{
				this.Log("Error creating CL-Buffer", error.ToString());
				return null;
			}

			// Add to list
			ClMem mem = new(buffer, length, typeof(T));

			// Lock memory list to avoid concurrent access issues
			lock (this._memoryLock)
			{
				// Add to memory list
				this.Memory.Add(mem);
			}

			return mem;
		}

		public T[] PullData<T>(IntPtr pointer, bool keep = false) where T : unmanaged
		{
			// Get buffer & length
			ClMem? mem = this.GetBuffer(pointer);
			if (mem == null || mem.Count == 0)
			{
				return [];
			}

			// New array with length
			long length = mem.IndexLength.ToInt64();
			T[] data = new T[length];

			// Read buffer
			CLResultCode error = CL.EnqueueReadBuffer(
				this.QUE,
				mem.Buffers.FirstOrDefault(),
				true,
				0,
				data,
				null,
				out CLEvent @event
			);

			// Check error
			if (error != CLResultCode.Success)
			{
				this.Log("Failed to read buffer", error.ToString(), 1);
				return [];
			}

			// If not keeping, free buffer
			if (!keep)
			{
				this.FreeBuffer(pointer);
			}

			// Return data
			return data;
		}

		public ClMem? AllocateSingle<T>(IntPtr size) where T : unmanaged
		{
			// Check size
			if (size.ToInt64() < 1)
			{
				return null;
			}

			// Create empty array of type and size
			T[] data = new T[size.ToInt64()];
			data = data.Select(x => default(T)).ToArray();

			// Create buffer
			CLBuffer buffer = CL.CreateBuffer<T>(this.Context, MemoryFlags.CopyHostPtr | MemoryFlags.ReadWrite, data, out CLResultCode error);
			if (error != CLResultCode.Success)
			{
				this.Log("Error creating CL-Buffer", error.ToString());
				return null;
			}

			// Add to list
			ClMem mem = new(buffer, size, typeof(T));

			// Lock memory list to avoid concurrent access issues
			lock (this._memoryLock)
			{
				// Add to memory list
				this.Memory.Add(mem);
			}

			// Return handle
			return mem;
		}



		// Array buffers
		public ClMem? PushChunks<T>(List<T[]> chunks) where T : unmanaged
		{
			// Check chunks
			if (chunks.Count < 1 || chunks.Any(chunk => chunk.LongLength < 1))
			{
				return null;
			}

			// Get IntPtr[] lengths
			IntPtr[] lengths = chunks.Select(chunk => new IntPtr(chunk.LongLength)).ToArray();

			// Create buffers for each chunk
			CLBuffer[] buffers = new CLBuffer[chunks.Count];
			for (int i = 0; i < chunks.Count; i++)
			{
				buffers[i] = CL.CreateBuffer(this.Context, MemoryFlags.CopyHostPtr | MemoryFlags.ReadWrite, chunks[i], out CLResultCode error);
				if (error != CLResultCode.Success)
				{
					this.Log("Error creating CL-Buffer for chunk " + i);
					return null;
				}
			}

			// Add to list
			ClMem mem = new(buffers, lengths, typeof(T));

			// Lock memory list to avoid concurrent access issues
			lock (this._memoryLock)
			{
				// Add to memory list
				this.Memory.Add(mem);
			}

			// Return object
			return mem;
		}

		public List<T[]> PullChunks<T>(IntPtr indexPointer) where T : unmanaged
		{
			// Get clmem by index pointer
			ClMem? mem = this.GetBuffer(indexPointer);
			if (mem == null || mem.Count < 1)
			{
				this.Log("No memory found for index pointer", indexPointer.ToString("X16"));
				return [];
			}

			// Chunk list & lengths
			List<T[]> chunks = [];
			IntPtr[] lengths = mem.Lengths;

			// Read every buffer
			for (int i = 0; i < mem.Count; i++)
			{
				T[] chunk = new T[lengths[i].ToInt64()];
				CLResultCode error = CL.EnqueueReadBuffer(
					this.QUE,
					mem.Buffers[i],
					true,
					0,
					chunk,
					null,
					out CLEvent @event
				);

				if (error != CLResultCode.Success)
				{
					this.Log("Failed to read buffer for chunk " + i, error.ToString(), 1);
					return [];
				}

				chunks.Add(chunk);
			}

			// Return chunks
			return chunks;
		}

		public ClMem? AllocateGroup<T>(IntPtr count, IntPtr size) where T : unmanaged
		{
			// Check count and size
			if (count < 1 || size.ToInt64() < 1)
			{
				return null;
			}

			// Create array of IntPtr for handles
			CLBuffer[] buffers = new CLBuffer[count];
			IntPtr[] lengths = new IntPtr[count];
			Type type = typeof(T);

			// Allocate each buffer
			for (int i = 0; i < count; i++)
			{
				buffers[i] = CL.CreateBuffer<T>(this.Context, MemoryFlags.CopyHostPtr | MemoryFlags.ReadWrite, new T[size.ToInt64()], out CLResultCode error);
				if (error != CLResultCode.Success)
				{
					this.Log("Error creating CL-Buffer for group " + i, error.ToString(), 1);
					return null;
				}
				lengths[i] = size;
			}

			ClMem mem = new(buffers, lengths, type);

			// Lock memory list to avoid concurrent access issues
			lock (this._memoryLock)
			{
				this.Memory.Add(mem);
			}

			return mem;
		}



		// UI
		public void FillPointersListbox(ListBox listBox)
		{
			// Clear listbox
			listBox.Items.Clear();

			// Fill with memory pointers
			foreach (ClMem mem in this.Memory)
			{
				if (mem.IsSingle)
				{
					listBox.Items.Add($"{mem.IndexHandle.ToString("X16")} ({mem.ElementType.Name}) - {mem.Size} bytes");
				}
				else if (mem.IsArray)
				{
					listBox.Items.Add($"{mem.IndexHandle.ToString("X16")} ({mem.ElementType.Name}) - {mem.Size} bytes, Count: {mem.Count}");
				}
			}
		}




		// ----- ----- ----- ACCESSIBLE METHODS ----- ----- ----- \\
		public IntPtr PushImage(ImageObject obj, bool log = false)
		{
			byte[] pixels = obj.GetPixelsAsBytes(true);
			if (pixels == null || pixels.LongLength == 0)
			{
				if (log)
				{
					this.Log("Couldn't get pixels (byte[]) from image object", "Aborting", 1);
				}
				return IntPtr.Zero;
			}

			obj.Pointer = this.PushData<byte>(pixels)?.IndexHandle ?? IntPtr.Zero;
			if (obj.Pointer == IntPtr.Zero)
			{
				if (log)
				{
					this.Log("Couldn't get pointer from pushing pixels to device", "Aborting", 1);
				}
				return IntPtr.Zero;
			}
			else
			{
				if (log)
				{
					this.Log(" => Pushed " + pixels.LongLength + " bytes to device", obj.Pointer.ToString("X16"), 1);
				}
			}

			return obj.Pointer;
		}

		public IntPtr PullImage(ImageObject obj, bool log = false)
		{
			IntPtr pointer = obj.Pointer;
			if (pointer == IntPtr.Zero)
			{
				if (log)
				{
					this.Log("Couldn't pull image", "Pointer was zero", 1);
					return IntPtr.Zero;
				}
			}

			byte[] pixels = this.PullData<byte>(pointer);
			if (pixels == null || pixels.LongLength == 0)
			{
				if (log)
				{
					this.Log("Couldn't pull pixels from pointer", "Pixels count was 0", 1);
				}
				return IntPtr.Zero;
			}

			obj.SetImageFromBytes(pixels, true);
			if (log)
			{
				this.Log(" <= Pulled " + pixels.LongLength + " bytes from device", obj.Pointer.ToString("X16"), 1);
			}
			return obj.Pointer;
		}




	}



	public class ClMem
	{
		public CLBuffer[] Buffers { get; set; } = [];
		
		public IntPtr[] Lengths { get; set; } = [];
		public Type ElementType { get; set; } = typeof(void);


		public bool IsSingle => this.Buffers.Length == 1;
		public bool IsArray => this.Buffers.Length > 1;

		public IntPtr Count => (nint) this.Buffers.LongLength;
		public IntPtr Size => (nint) this.Lengths.Sum(length => length.ToInt64() * Marshal.SizeOf(this.ElementType));

		public IntPtr IndexHandle => this.Buffers.FirstOrDefault().Handle;
		public IntPtr IndexLength => this.Lengths.FirstOrDefault();


		public ClMem(CLBuffer[] buffers, IntPtr[] lengths, Type? elementType = null)
		{
			this.Buffers = buffers;
			this.Lengths = lengths;
			this.ElementType = elementType ?? typeof(void);
		}

		public ClMem(CLBuffer buffer, IntPtr length, Type? elementType = null)
		{
			this.Buffers = [buffer];
			this.Lengths = [length];
			this.ElementType = elementType ?? typeof(void);
		}



		public override string ToString()
		{
			return this.IndexHandle.ToString("X16");
		}
	}
}

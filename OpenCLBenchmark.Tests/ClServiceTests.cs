using System.Drawing;
using Xunit.Sdk;

namespace OpenCLBenchmark.Tests
{
    [TestClass]
    public sealed class ClServiceTests
    {
        // ----- ATTRIBUTES
        private string Repopath => Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..", "OpenCLBenchmark"));

        private OpenClService Service;



        [TestInitialize]
        public void Initialize()
        {
            this.Service = new OpenClService(this.Repopath);
            this.Service.SelectDeviceLike("Intel");

            Assert.IsNotNull(this.Service.CTX);
            Assert.IsNotNull(this.Service.DEV);
            Assert.IsNotNull(this.Service.PLAT);
            Assert.IsFalse(this.Service.INDEX == -1);
        }

        [TestCleanup]
        public void Cleanup()
        {
            this.Service.Dispose();
        }



        [TestMethod]
        public void MoveImage_NewObj_ShouldReturnIntPtr()
        {
            // Arrange
            ImageObject obj = new ImageObject(new Bitmap(1024, 1024), "testObj01");

            // Act
            IntPtr result = this.Service.MoveImage(obj);

            // Assert
            Assert.IsFalse(result == IntPtr.Zero);
        }

        [TestMethod]
        public void MoveImage_Twice_NewObj_ShouldReturnIntPtrZero()
        {
            // Arrange
            ImageObject obj = new ImageObject(new Bitmap(1024, 1024), "testObj01");

            // Act
            IntPtr result = this.Service.MoveImage(obj);
            Assert.IsFalse(obj.OnHost);
            Assert.IsTrue(obj.OnDevice);

            result = this.Service.MoveImage(obj);

            // Assert
            Assert.IsTrue(result == IntPtr.Zero);
            Assert.IsTrue(obj.OnHost);
            Assert.IsFalse(obj.OnDevice);
        }
    }
}

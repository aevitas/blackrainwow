using BlackRain.Common.Objects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Magic;

namespace Tester
{
    
    
    /// <summary>
    ///This is a test class for ObjectManagerTest and is intended
    ///to contain all ObjectManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ObjectManagerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Me
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BlackRainObjects.dll")]
        public void MeTest()
        {
            WowPlayer expected = null; // TODO: Initialize to an appropriate value
            WowPlayer actual;
            ObjectManager_Accessor.Me = expected;
            actual = ObjectManager_Accessor.Me;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetMainThread
        ///</summary>
        [TestMethod()]
        public void GetMainThreadTest()
        {
            int pid = 0; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = ObjectManager.GetMainThread(pid);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Initialize
        ///</summary>
        [TestMethod()]
        public void InitializeTest()
        {
            int pid = 0; // TODO: Initialize to an appropriate value
            ObjectManager.Initialize(pid);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for Pulse
        ///</summary>
        [TestMethod()]
        public void PulseTest()
        {
            ObjectManager.Pulse();
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for ResumeMainThread
        ///</summary>
        [TestMethod()]
        public void ResumeMainThreadTest()
        {
            int pid = 0; // TODO: Initialize to an appropriate value
            ObjectManager.ResumeMainThread(pid);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for SuspendMainThread
        ///</summary>
        [TestMethod()]
        public void SuspendMainThreadTest()
        {
            int pid = 0; // TODO: Initialize to an appropriate value
            ObjectManager.SuspendMainThread(pid);
            Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for CConnection
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BlackRainObjects.dll")]
        public void CConnectionTest()
        {
            uint expected = 0; // TODO: Initialize to an appropriate value
            uint actual;
            ObjectManager_Accessor.CConnection = expected;
            actual = ObjectManager_Accessor.CConnection;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CConnectionOffset
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BlackRainObjects.dll")]
        public void CConnectionOffsetTest()
        {
            uint expected = 0; // TODO: Initialize to an appropriate value
            uint actual;
            ObjectManager_Accessor.CConnectionOffset = expected;
            actual = ObjectManager_Accessor.CConnectionOffset;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CConnectionPointer
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BlackRainObjects.dll")]
        public void CConnectionPointerTest()
        {
            uint expected = 0; // TODO: Initialize to an appropriate value
            uint actual;
            ObjectManager_Accessor.CConnectionPointer = expected;
            actual = ObjectManager_Accessor.CConnectionPointer;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CurrentManager
        ///</summary>
        [TestMethod()]
        public void CurrentManagerTest()
        {
            uint expected = 0; // TODO: Initialize to an appropriate value
            uint actual;
            ObjectManager.CurrentManager = expected;
            actual = ObjectManager.CurrentManager;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Initialized
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BlackRainObjects.dll")]
        public void InitializedTest()
        {
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            ObjectManager_Accessor.Initialized = expected;
            actual = ObjectManager_Accessor.Initialized;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for LocalGUID
        ///</summary>
        [TestMethod()]
        public void LocalGUIDTest()
        {
            ulong expected = 0; // TODO: Initialize to an appropriate value
            ulong actual;
            ObjectManager.LocalGUID = expected;
            actual = ObjectManager.LocalGUID;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Me
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BlackRainObjects.dll")]
        public void MeTest1()
        {
            WowPlayer expected = null; // TODO: Initialize to an appropriate value
            WowPlayer actual;
            ObjectManager_Accessor.Me = expected;
            actual = ObjectManager_Accessor.Me;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Memory
        ///</summary>
        [TestMethod()]
        public void MemoryTest()
        {
            BlackMagic expected = null; // TODO: Initialize to an appropriate value
            BlackMagic actual;
            ObjectManager.Memory = expected;
            actual = ObjectManager.Memory;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TLSMask
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BlackRainObjects.dll")]
        public void TLSMaskTest()
        {
            string actual;
            actual = ObjectManager_Accessor.TLSMask;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for TLSPattern
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BlackRainObjects.dll")]
        public void TLSPatternTest()
        {
            string actual;
            actual = ObjectManager_Accessor.TLSPattern;
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ThreadLocalStorage
        ///</summary>
        [TestMethod()]
        [DeploymentItem("BlackRainObjects.dll")]
        public void ThreadLocalStorageTest()
        {
            uint expected = 0; // TODO: Initialize to an appropriate value
            uint actual;
            ObjectManager_Accessor.ThreadLocalStorage = expected;
            actual = ObjectManager_Accessor.ThreadLocalStorage;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for WowHandle
        ///</summary>
        [TestMethod()]
        public void WowHandleTest()
        {
            IntPtr expected = new IntPtr(); // TODO: Initialize to an appropriate value
            IntPtr actual;
            ObjectManager.WowHandle = expected;
            actual = ObjectManager.WowHandle;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}

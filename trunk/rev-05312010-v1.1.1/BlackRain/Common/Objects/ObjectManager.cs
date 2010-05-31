using System;
using System.Collections.Generic;
using System.Diagnostics;
using BlackRain.Constants;
using Magic;

namespace BlackRain.Common.Objects
{
    public static class ObjectManager
    {
        public static BlackMagic Memory { get; set; }
        public static IntPtr WowHandle { get; set; }
        public static WowPlayer Me { get; private set; }
        public static bool Initialized { get; private set; }

        #region <Initialization Fields>
        // private static uint CConnectionPointer { get; set; }
        private static uint CConnection { get; set; }
        private static uint CConnectionOffset { get; set; }
        private static string TLSPattern { get { return "EB 02 33 C0 8B D 00 00 00 00 64 8B 15 00 00 00 00 8B 34 8A 8B D 00 00 00 00 89 81 00 00 00 00"; } }
        private static string TLSMask { get { return "xxxxxx????xxx????xxxxx????xx????"; } }
        private static uint ThreadLocalStorage { get; set; }
        public static uint CurrentManager { get; set; }
        #endregion
        #region <Properties>
        public static ulong LocalGUID { get; set; }
        #endregion

        public static List<WowObject> Objects  = new List<WowObject>();

        /// <summary>
        /// Initializes the ObjectManager, attaching it to the selected process.
        /// </summary>
        /// <param name="pid">The World of Warcraft process ID.</param>
        public static void Initialize(int pid)
        {
            if (Memory != null) return;
                Memory = new BlackMagic(pid); // Assign Memory to the selected Wow instance.

            try
            {
                ThreadLocalStorage = Memory.FindPattern(TLSPattern, TLSMask);

                if (ThreadLocalStorage != uint.MaxValue) // Ensure we have found the TLS.
                {
                    CConnection = Memory.ReadUInt(Memory.ReadUInt(ThreadLocalStorage + 0x16));
                    CConnectionOffset = Memory.ReadUInt(ThreadLocalStorage + 0x1C);
                    CurrentManager = Memory.ReadUInt(CConnection + CConnectionOffset);

                    Logging.Write(string.Format("CurrentManager found at 0x{0:X}", CurrentManager));

                    // Retrieve the local player's GUID for later use.
                    LocalGUID = Memory.ReadUInt64(CurrentManager + (uint)Offsets.ObjectManager.LocalGUID);
                    Logging.Write(string.Format("[Player] Local GUID: {0}", LocalGUID));

                    if (CurrentManager != uint.MinValue && CurrentManager != uint.MaxValue)
                    {
                        Initialized = true;
                        WowHandle = Memory.ProcessHandle;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Write(ex.Message);
            }
        }

        /// <summary>
        /// Pulses the ObjectManager, refreshing any objects it holds.
        /// <!-- This is a slow way to go around things, but it's accurate, and that's what counts. -->
        /// </summary>
        public static void Pulse()
        {
            if (Objects.Count > 0)
                Objects.Clear(); // Clean up any previous objects in the list, if we have them.

            try
            {
                var currentObject = new WowObject(Memory.ReadUInt(CurrentManager + (uint)Offsets.ObjectManager.FirstObject));

                while (currentObject.BaseAddress != uint.MinValue && currentObject.BaseAddress % 2 == uint.MinValue)
                {
                    switch (currentObject.Type)
                    {
                        case (int)Offsets.WowObjectType.Unit:
                            Objects.Add(new WowUnit(currentObject.BaseAddress));
                            break;
                        case (int)Offsets.WowObjectType.Item:
                            Objects.Add(new WowItem(currentObject.BaseAddress));
                            break;
                        case (int)Offsets.WowObjectType.Container:
                            Objects.Add(new WowContainer(currentObject.BaseAddress));
                            break;
                        case (int)Offsets.WowObjectType.Corpse:
                            Objects.Add(new WowCorpseObject(currentObject.BaseAddress));
                            break;
                        case (int)Offsets.WowObjectType.GameObject:
                            Objects.Add(new WowGameObject(currentObject.BaseAddress));
                            break;
                        case (int)Offsets.WowObjectType.DynamicObject:
                            Objects.Add(new WowDynamicObject(currentObject.BaseAddress));
                            break;
                        case (int)Offsets.WowObjectType.Player:
                            Objects.Add(new WowPlayer(currentObject.BaseAddress));
                            break;
                    }

                    if (currentObject.GUID == LocalGUID)
                        Me = new WowPlayer(currentObject.BaseAddress);

                    currentObject.BaseAddress = Memory.ReadUInt(currentObject.BaseAddress + (uint)Offsets.ObjectManager.NextObject);
                }
            }
            catch (Exception ex)
            {
                Logging.Write("Exception in ObjectManager: " + ex.Message);
            }
        }


        #region <Public Helper Methods>

        /// <summary>
        /// Suspends the thread with given ID.
        /// </summary>
        /// <param name="pid"></param>
        public static void SuspendMainThread(int pid)
        {
            ProcessThread mainthread = SThread.GetMainThread(Memory.ProcessId);
            IntPtr threadHandle = SThread.OpenThread(mainthread.Id);
            SThread.SuspendThread(threadHandle);
        }

        /// <summary>
        /// Resumes the thread with given ID.
        /// </summary>
        /// <param name="pid"></param>
        public static void ResumeMainThread(int pid)
        {
            ProcessThread mainthread = SThread.GetMainThread(Memory.ProcessId);
            IntPtr threadHandle = SThread.OpenThread(mainthread.Id);
            SThread.ResumeThread(threadHandle);
        }

        /// <summary>
        /// Obtains the main thread from the process with given ID.
        /// </summary>
        /// <param name="pid">The Process ID.</param>
        /// <returns>Main Thread</returns>
        public static int GetMainThread(int pid)
        {
            ProcessThread mainthread = SThread.GetMainThread(pid);
            return mainthread.Id;
        }

        #endregion
    }
}

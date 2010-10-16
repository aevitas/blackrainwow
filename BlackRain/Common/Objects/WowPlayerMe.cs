namespace BlackRain.Common.Objects
{
    /// <summary>
    /// Represents your player (character).
    /// </summary>
    public class WowPlayerMe : WowPlayer
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="baseAddress"></param>
        public WowPlayerMe(uint baseAddress)
            : base (baseAddress)
        {
        }

        /// <summary>
        /// Your character's current Zone.
        /// </summary>
        public string Zone
        {
            get
            {
                return ObjectManager.Memory.ReadASCIIString(ObjectManager.Memory.ReadUInt(ObjectManager.GlobalBaseAddress + (uint)Offsets.WoWPlayerMe.Zone), 255);
            }
        }

        /// <summary>
        /// Your character's current SubZone.
        /// </summary>
        public string SubZone
        {
            get
            {
                return ObjectManager.Memory.ReadASCIIString(ObjectManager.Memory.ReadUInt(ObjectManager.GlobalBaseAddress + (uint)Offsets.WoWPlayerMe.SubZone), 255);
            }
        }

        /// <summary>
        /// Your character's money (in copper).
        /// </summary>
        public int Money
        {
            get { return GetStorageField<int>((uint)Offsets.WowPlayerFields.PLAYER_FIELD_COINAGE); }
        }
    }
}

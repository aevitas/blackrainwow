
namespace BlackRain.Common.Objects
{
    /// <summary>
    /// Represents a player.
    /// </summary>
    public class WowPlayer : WowUnit
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="baseAddress"></param>
        public WowPlayer(uint baseAddress)
            : base (baseAddress)
        {
        }

        /// <summary>
        /// The player's experience points.
        /// </summary>
        public int Experience
        {
            get { return GetStorageField<int>((uint)Offsets.WowPlayerFields.PLAYER_XP); }
        }

        /// <summary>
        /// The experience the player requires to advance to the next level.
        /// </summary>
        public int NextLevel
        {
            get { return GetStorageField<int>((uint)Offsets.WowPlayerFields.PLAYER_NEXT_LEVEL_XP); }
        }

        /// <summary>
        /// The ID of the guild the player resides in.
        /// </summary>
        public int GuildID
        {
            get { return /*GetStorageField<int>((uint)Offsets.WowPlayerFields.PLAYER_GUILDID)*/ 0; }
        }

        /// <summary>
        /// The amount of experience the player has rested.
        /// <!-- You can calculate the double experience rate, and thus shorten the time to the next level when using this in a calculation. -->
        /// </summary>
        public int RestExperience
        {
            get { return GetStorageField<int>((uint)Offsets.WowPlayerFields.PLAYER_REST_STATE_EXPERIENCE); }
        }

        /// <summary>
        /// Determines if the player is a Ghost. i.e: dead, and released from corpse.
        /// </summary>
        public bool Ghost
        {
            get { return false; }
        }

        /// <summary>
        /// Returns true if in combat, false if not.
        /// </summary>
        public bool Combat
        {
            get { return HasUnitFlag(Offsets.UnitFlags.Combat); }
        }

        /// <summary>
        /// The name of the player.
        /// </summary>
        public override string Name
        {
            get
            {
                uint nMask = ObjectManager.Memory.ReadUInt(ObjectManager.GlobalBaseAddress + (uint)Offsets.WoWPlayer.NameStore + (uint)Offsets.WoWPlayer.NameMask);
                uint nBase = ObjectManager.Memory.ReadUInt(ObjectManager.GlobalBaseAddress + (uint)Offsets.WoWPlayer.NameStore + (uint)Offsets.WoWPlayer.NameBase);

                ulong nShortGUID = this.GUID & 0xFFFFFFFF; // only need part of the GUID
                ulong nOffset = 0xC * (nMask & nShortGUID);

                uint nCurrentObject = ObjectManager.Memory.ReadUInt((uint)(nBase + (12 * (nMask & nShortGUID)) + 0x8));
                nOffset = ObjectManager.Memory.ReadUInt((uint)(nBase + nOffset));

                if ((nCurrentObject & 0x1) == 0x1) 
                    return "Unknown Player";

                uint nTestAgainstGUID = ObjectManager.Memory.ReadUInt((uint)(nCurrentObject));

                while (nTestAgainstGUID != nShortGUID)
                {
                    nCurrentObject = ObjectManager.Memory.ReadUInt((uint)(nCurrentObject + nOffset + 0x4));

                    if ((nCurrentObject & 0x1) == 0x1) 
                        return "Unknown Player";

                    nTestAgainstGUID = ObjectManager.Memory.ReadUInt((uint)(nCurrentObject));
                }

                return ObjectManager.Memory.ReadASCIIString((uint)(nCurrentObject + (uint)Offsets.WoWPlayer.NameString), 40);
            }
        }

        /// <summary>
        /// Returns true if the unit is mounted, false if not.
        /// </summary>
        public bool Mounted
        {
            get
            {
                return MountDisplayID > 0;
            }
        }
    }
}

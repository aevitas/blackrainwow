using BlackRain.Constants;

namespace BlackRain.Common.Objects
{
    public class WowPlayer : WowUnit
    {
        public WowPlayer(uint baseAddress)
            : base(baseAddress)
        {
        }

        /// <summary>
        /// The player's experience points.
        /// </summary>
        public int Experience
        {
            get { return GetStorageField<int>((uint) Offsets.WowPlayerFields.PLAYER_XP); }
        }

        /// <summary>
        /// The experience the player requires to advance to the next level.
        /// </summary>
        public int NextLevel
        {
            get { return GetStorageField<int>((uint) Offsets.WowPlayerFields.PLAYER_NEXT_LEVEL_XP); }
        }

        /// <summary>
        /// The ID of the guild the player resides in.
        /// </summary>
        public int GuildID
        {
            get { return GetStorageField<int>((uint) Offsets.WowPlayerFields.PLAYER_GUILDID); }
        }

        /// <summary>
        /// The amount of experience the player has rested.
        /// <!-- You can calculate the double experience rate, and thus shorten the time to the next level when using this in a calculation. -->
        /// </summary>
        public int RestExperience
        {
            get { return GetStorageField<int>((uint) Offsets.WowPlayerFields.PLAYER_REST_STATE_EXPERIENCE); }
        }
    }
}

using System;
using System.IO;

namespace BlackRain.Common
{
    /// <summary>
    /// Handles all generic logging for BlackRain.
    /// </summary>
    public static class Logging
    {
        /// <summary>
        /// The Prefix for the BlackRain log filename.
        /// </summary>
        private const string Prefix = "BlackRain_";

        /// <summary>
        /// Writes to the general output log.
        /// </summary>
        /// <param name="sender">The object the log entry came from.</param>
        /// <param name="value">The value to be logged.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public static bool Write(object sender, string value)
        {
            using (var writer = new StreamWriter(Prefix + "Debug.html", true))
            {
                if (!string.IsNullOrEmpty(value))
                {
                    writer.WriteLine("[" + DateTime.Now + "] - " + value + " - From: " + sender + "<br/>");
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Writes to the general output log.
        /// </summary>
        /// <param name="value">The value to be logged.</param>
        /// <returns>true if successful, otherwise false.</returns>
        public static bool Write(string value)
        {
            using (var writer = new StreamWriter(Prefix + "Debug.html", true))
            {
                if (!string.IsNullOrEmpty(value))
                {
                    writer.WriteLine("[" + DateTime.Now + "] - " + value + "<br/>");
                    return true;
                }
            }
            return false;
        }
    }
}

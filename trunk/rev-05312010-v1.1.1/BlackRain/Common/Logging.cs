using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;

namespace BlackRain.Common
{
    /// <summary>
    /// Handles all generic logging for BlackRain.
    /// </summary>
    public static class Logging
    {
        #region Delegates

        /// <summary>
        /// 
        /// </summary>
        public delegate void DebugDelegate(string message, Color col);

        /// <summary>
        /// 
        /// </summary>
        public delegate void WriteDelegate(string message, Color col);

        #endregion

        private static readonly Thread QueueThread;
        private static readonly Queue<string> LogQueue = new Queue<string>();
        private static string _logSpam = "";

        static Logging()
        {
            LogOnWrite = true;
            QueueThread = new Thread(_WriteQueue) { IsBackground = true };
            QueueThread.Start();
        }

        /// <summary>
        /// Occurs when [on write].
        /// </summary>
        public static event WriteDelegate OnWrite;

        /// <summary>
        /// Gets or sets a value indicating whether every Write action should also be logged.
        /// </summary>
        /// <value><c>true</c> if [log on write]; otherwise, <c>false</c>.</value>
        /// 1/14/2009 7:40 PM
        public static bool LogOnWrite { get; set; }

        private static string TimeStamp { get { return string.Format("[{0}] ", DateTime.Now.ToLongTimeString() + ":" + DateTime.Now.Millisecond); } }
        private static string LogDate { get { return DateTime.Now.ToShortDateString().Replace("/", "-"); } }
        public static string ApplicationPath { get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); } }

        private static void _WriteQueue()
        {
            if (!Directory.Exists(string.Format("{0}\\Logs", System.Windows.Forms.Application.StartupPath)))
            {
                Directory.CreateDirectory(string.Format("{0}\\Logs", System.Windows.Forms.Application.StartupPath));
            }
            while (true)
            {
                try
                {
                    using (TextWriter tw = new StreamWriter(string.Format("{0}\\Logs\\{1} Log.txt", System.Windows.Forms.Application.StartupPath, LogDate), true))
                    {
                        while (LogQueue.Count != 0)
                        {
                            tw.WriteLine(LogQueue.Dequeue());
                        }
                    }
                    Thread.Sleep(500);
                }
                catch
                {
                    break;
                }
            }
        }

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
        /// Writes the specified message to the message queue.
        /// </summary>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// 1/14/2009 7:33 PM
        public static void Write(string format, params object[] args)
        {
            Write(Color.Black, format, args);
        }

        /// <summary>
        /// Writes the specified message to the message queue.
        /// </summary>
        /// <param name="color">The color to write the message in.</param>
        /// <param name="format">The format.</param>
        /// <param name="args">The args.</param>
        /// 1/14/2009 7:33 PM
        public static void Write(Color color, string format, params object[] args)
        {
            try
            {
                string s = string.Format(format, args);

                if (_logSpam == s)
                    return;

                _logSpam = s;

                InvokeOnWrite(s, color);
                s = TimeStamp + string.Format(format, args);

                if (LogOnWrite)
                    LogQueue.Enqueue(s);
            }

            catch (Exception ex)
            {
                _logSpam = "";
            }
        }

        /// <summary>
        /// Writes the specified message to the message queue.
        /// </summary>
        /// <param name="format">The format.</param>
        /// 1/14/2009 7:33 PM
        public static void Write(string format)
        {
            Write(Color.Black, format, "");
        }

        private static void InvokeOnWrite(string message, Color col)
        {
            if (OnWrite != null)
                OnWrite(message, col);
        }
    }
}

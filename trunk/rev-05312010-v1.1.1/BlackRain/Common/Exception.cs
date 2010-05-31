using System;

namespace BlackRain.Common
{
    /// <summary>
    /// A BlackRain exception which should be logged.
    /// </summary>
    [Serializable]
    public class BlackRainException : Exception
    {
        public BlackRainException()
        {
        }

        public BlackRainException(string exception)
            : base(exception)
        {
        }

        public BlackRainException(string exception, Exception inner)
            : base(exception, inner)
        {
        }
    }

    public class BlackRainObjectException : BlackRainException
    {
        public BlackRainObjectException(string exception)
            : base("Object Exception: " + exception)
        {
        }
    }

    public class BlackRainMemoryException : BlackRainException
    {
        public BlackRainMemoryException(string exception)
            : base("Memory Exception: " + exception)
        {
        }
    }
}

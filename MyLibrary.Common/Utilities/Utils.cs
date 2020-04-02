using NLog;

namespace MyLibrary.Common.Utilities
{
    public static class Utils
    {
        public static Logger Log { get; } = LogManager.GetCurrentClassLogger();
    }
}
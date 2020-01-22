using NLog;

namespace MyLibrary.Common.Utilities
{
    public static class Utils
    {
        private static readonly Logger log;

        public static Logger Log { get { return log; } }

        static Utils()
        {
            log = LogManager.GetCurrentClassLogger();
        }
    }
}
using ZephyrHelper.Extensions;

namespace ZephyrHelper
{
    public class Enums
    {
        public enum ExecutionStatus
        {
            [Text("{\"status\":\"1\"}")] Pass,
            [Text("{\"status\":\"2\"}")] Fail,
            [Text("{\"status\":\"3\"}")] WIP
        }

        public static string Pass = "{\"status\":\"1\"}";
        public static string Fail = "{\"status\":\"2\"}";
        public static string WIP = "{\"status\":\"3\"}";
    }
}

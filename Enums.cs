using ZephyrHelper.Extensions;

namespace ZephyrHelper
{
    public class Enums
    {
        public enum ExecutionStatus
        {
            [Text("{\"status\":\"-1\"}")] UNEXECUTED,
            [Text("{\"status\":\"1\"}")] PASS,
            [Text("{\"status\":\"2\"}")] FAIL,
            [Text("{\"status\":\"3\"}")] WIP,
            [Text("{\"status\":\"4\"}")] BLOCKED
        }
    }
}

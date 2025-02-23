using UtilityDLL;
using UtilityDLL.QUEUE.TODO;

namespace UtilityDLL.QUEUE.RESPONSE;

// public enum EResponseTarget
// {
//     [System.ComponentModel.Description("Private")]
//     PRIVATE,
//     [System.ComponentModel.Description("Group")]
//     BROADCAST,
//     GROUP
// }

public enum ERequest
{
    [System.ComponentModel.Description("Message")]
    S = '|',
    MESSAGE_RESPONSE,
    MESSAGE_BROADCAST,
    MESSAGE_PRIVATE,
    [System.ComponentModel.Description("Command")]
    COMMAND_RESPONSE,
    COMMAND_BROADCAST,
    COMMAND_PRIVATE,
}

public struct ResponseEvent
{
    public ERequest Request { get; set; }
    public ETodoRequest TodoRequest { get; set; }
    public string Message { get; set; }
    public string IDTarget { get; set; }
    public string ID { get; set; }
}

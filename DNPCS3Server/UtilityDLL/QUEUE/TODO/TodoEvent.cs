namespace UtilityDLL.QUEUE.TODO;



public enum ETodoTarget
{
    [System.ComponentModel.Description("USER")]
    USER,
    [System.ComponentModel.Description("NPC")]
    TISSUES,
    NPC,
    RED,
    GREEN,
    BLUE
}

public enum ETodoRequest
{
    [System.ComponentModel.Description("USER")]
    JOIN,
    EXCEPT,
    MOVE_LEFT,
    MOVE_RIGHT,
    MOVE_UP,
    MOVE_DOWN,
    MOVE_FRONT,
    MOVE_REAR,
    [System.ComponentModel.Description("NPC")]
    BLOOD,
    ANS_FOWARD,
    ANS_BACKWARD,
    FLOW_L,
    FLOW_R
}

public struct TodoEvent
{
    public static char Seperator { get;} = '|';
    public ETodoRequest Req { get; set; }
    public ETodoTarget Target { get; set; }
    public string IDorLevel { get; set; }
}

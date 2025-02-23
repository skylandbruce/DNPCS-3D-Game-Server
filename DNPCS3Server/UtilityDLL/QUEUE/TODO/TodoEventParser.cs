namespace UtilityDLL.QUEUE.TODO;
public class TodoEventParser
{
    public TodoEvent Parse(string input)
    {
        // 문자열 파싱하여 TodoEvent 요소 추출
        string[] parts = input.Split(','); // 쉼표를 기준으로 문자열 분리

        // 예외 처리: 파싱할 문자열의 형식이 올바르지 않을 경우
        if (parts.Length != 3)
        {
            throw new ArgumentException("Invalid input string format.");
        }

        // TodoEvent 객체 생성 및 요소 할당
        TodoEvent todoEvent = new TodoEvent();

        // ETarget 파싱
        if (Enum.TryParse(parts[0], out ETodoTarget target))
        {
            todoEvent.Target = target;
        }
        else
        {
            throw new ArgumentException("Invalid ETarget value.");
        }
        
        // ERequest 파싱
        if (Enum.TryParse(parts[1], out ETodoRequest req))
        {
            todoEvent.Req = req;
        }
        else
        {
            throw new ArgumentException("Invalid ERequest value.");
        }

        // IDorLevel 파싱
        if (long.TryParse(parts[2], out long idOrLevel))
        {
            // todoEvent.IDorLevel = idOrLevel;
        }
        else
        {
            throw new ArgumentException("Invalid IDorLevel value.");
        }

        return todoEvent;
    }
}
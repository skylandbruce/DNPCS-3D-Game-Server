namespace UtilityDLL.UNIQUE;

public sealed class UniqueLongGeneratorAsync
{
    private static readonly Lazy<UniqueLongGeneratorAsync> lazy =
        new Lazy<UniqueLongGeneratorAsync>(() => new UniqueLongGeneratorAsync());

    public static UniqueLongGeneratorAsync Instance { get { return lazy.Value; } }

    private long _currentValue;
    private readonly object _lock = new object();

    private UniqueLongGeneratorAsync()
    {
        _currentValue = 0;
    }

    public async Task<long> GetNextUniqueLongAsync()
    {
        // 비동기 작업의 모의를 위해 약간의 지연을 추가합니다.
        await Task.Delay(10); // 실제 시나리오에서는 I/O 바운드 작업일 가능성이 높습니다.

        lock (_lock)
        {
            _currentValue++;
            return _currentValue;
        }
    }
}


public class Example
{
    public async void Test()
    {
        // 싱글톤 인스턴스 가져오기
        var generator = UniqueLongGeneratorAsync.Instance;

        // 여러 개의 비동기 작업 생성 및 실행
        var tasks = new Task<long>[5];
        for (int i = 0; i < tasks.Length; i++)
        {
            tasks[i] = generator.GetNextUniqueLongAsync();
        }

        // 모든 작업이 완료될 때까지 기다리고 결과 출력
        long[] results = await Task.WhenAll(tasks);

        foreach (long result in results)
        {
            Console.WriteLine($"Generated Value: {result}");
        }

                // 추가 테스트: 연속 호출 시 값의 증가 확인
        Console.WriteLine("\nAdditional Test (Sequential Calls):");
        for (int i = 0; i < 3; i++)
        {
            long nextValue = await generator.GetNextUniqueLongAsync();
            Console.WriteLine($"Generated Value: {nextValue}");
        }

        Console.ReadKey();
    }
}
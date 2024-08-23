

public class SomeGamePlayService : System.IDisposable
{
    private readonly SomeCommonService _service;

    public SomeGamePlayService(SomeCommonService service)
    {
        _service = service;
        UnityEngine.Debug.Log($"{GetType().Name} is Created!");
    }

    public void Dispose() => UnityEngine.Debug.Log("Удаляем подписки");
    
}


public class SomeMainMenuService
{
    private readonly SomeCommonService _service;
    public SomeMainMenuService(SomeCommonService service)
    {
        _service = service;
        UnityEngine.Debug.Log($"{GetType().Name} is Created!");
    }
}
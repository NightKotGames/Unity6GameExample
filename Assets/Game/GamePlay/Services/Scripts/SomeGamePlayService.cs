

using R3;
using ObservableCollections;
using StateGame;
using System.Linq;

public class SomeGamePlayService : System.IDisposable
{
    private readonly SomeCommonService _service;
    private readonly GameStateProxy _gameState;

    // Допустим, для какого то Общего Игрового Сервиса требуется состояние игры...
    internal SomeGamePlayService(GameStateProxy gameState, SomeCommonService service)
    {
        _service = service;
        _gameState = gameState;

        UnityEngine.Debug.Log($"{GetType().Name} is Created!");

        gameState.Buildings.ForEach(b => UnityEngine.Debug.Log($"Building: {b.TypeId}"));
        // Например подписываеися на добавление 
        gameState.Buildings.ObserveAdd().Subscribe(e => UnityEngine.Debug.Log($"Building Added: {e.Value.TypeId}"));
        // или удаление
        gameState.Buildings.ObserveRemove().Subscribe(e => UnityEngine.Debug.Log($"Building Removed: {e.Value.TypeId}"));

        /// Тестим...

        AddBuildings("Added_Building_01");
        AddBuildings("Added_Building_02");
        AddBuildings("Added_Building_03");
        RemoveBuildings("Added_Building_03");

    }

    public void Dispose() => UnityEngine.Debug.Log("Удаляем подписки");

    private void AddBuildings(string buildingsTypeId)
    {
        var building = new BuildingEntity
        {
            TypeId = buildingsTypeId,
        };

        var buildingProxy = new BuildingEntityProxy(building);
        _gameState.Buildings.Add(buildingProxy);
    }

    private void RemoveBuildings(string buildingsTypeId)
    {
        var biilding  = _gameState.Buildings.FirstOrDefault(b => b.TypeId == buildingsTypeId);
        if (biilding != null)
        {
            _gameState.Buildings.Remove(biilding);
        }
    }
}
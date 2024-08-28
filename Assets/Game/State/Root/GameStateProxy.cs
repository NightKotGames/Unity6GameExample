
using R3;
using ObservableCollections;
using System.Linq;


namespace StateGame
{
    internal class GameStateProxy
    {
        /// Proxy
        internal ObservableList<BuildingEntityProxy> Buildings { get; } = new();

        /// Передаем в Конструкторе Оригинальное состояние
        internal GameStateProxy(GameState state) 
        {
            state.Buildings.ForEach(b => Buildings.Add(new BuildingEntityProxy(b)));

            Buildings.ObserveAdd().Subscribe(e =>
            {                
                var addedBuildingEntity = e.Value;
                state.Buildings.Add(new BuildingEntity
                {
                    Id = addedBuildingEntity.Id,
                    TypeId = addedBuildingEntity.TypeId,
                    Position = addedBuildingEntity.Position.Value,
                    Level = addedBuildingEntity.Level.Value
                });
            });

            Buildings.ObserveRemove().Subscribe(e =>
            {
                var removeBuildingEntityProxy = e.Value;
                var removeBuildingEntity = state.Buildings.FirstOrDefault(b => b.Id == removeBuildingEntityProxy.Id);
                state.Buildings.Remove(removeBuildingEntity);
            });
        }
    }
}


using R3;

public class BuildingEntityProxy
{
    /// Неизменяемые
    public int Id { get; }
    public string TypeId { get; }

    /// Изменяемые
    public ReactiveProperty<UnityEngine.Vector3Int> Position { get; }
    public ReactiveProperty<int> Level { get; }

    public BuildingEntityProxy(BuildingEntity entity)
    {
        Id = entity.Id;
        TypeId = entity.TypeId;

        /// Создаем реактивные свойства
        Position = new ReactiveProperty<UnityEngine.Vector3Int>(entity.Position);
        Level = new ReactiveProperty<int>(entity.Level);

        /// Подписываемся на изменения, пропуская первоначальное значение (Skip(1))
        Position.Skip(1).Subscribe(value => entity.Position = value);
        Level.Skip(1).Subscribe(value => entity.Level = value);
    }
}
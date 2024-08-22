

public abstract class SceneEnterParams
{
    public string SceneName { get; }

    public SceneEnterParams(string sceneName) => SceneName = sceneName;

    // Кастуем этот класс в Дочерний...
    public T As<T>() where T : SceneEnterParams => (T)this;

}

using EntryPoint;

public class GamePlayEnterParams : SceneEnterParams
{    
    /// Какие то входные параметры Сцены 
    public string SaveFileName {  get; }
    public int LevelNumber { get; }
    
    public GamePlayEnterParams(string saveFileName, int levelNumber) : base(Scenes.GAMEPLAY)
    {
        SaveFileName = saveFileName;
        LevelNumber = levelNumber;
    }
}
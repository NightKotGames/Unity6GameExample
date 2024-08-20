
public class GamePlayExitParams
{
    public MainMenuEnterParams MainMenuEnterParams { get; }

    public GamePlayExitParams(MainMenuEnterParams mainMenuEnterParams) => MainMenuEnterParams = mainMenuEnterParams;
}
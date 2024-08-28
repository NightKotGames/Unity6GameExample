
using R3;

namespace StateGame
{
    internal class GameSettingsStateProxy
    {
        public ReactiveProperty<int> MusicVolume { get; }
        public ReactiveProperty<int> SFXVolume { get; }

        public GameSettingsStateProxy(GameSettings gameSettings)
        {
            MusicVolume = new ReactiveProperty<int>(gameSettings.MusicVolume);
            SFXVolume = new ReactiveProperty<int>(gameSettings.SFXVolume);

            MusicVolume.Skip(1).Subscribe(value  => gameSettings.MusicVolume = value);
            SFXVolume.Skip(1).Subscribe(value  => gameSettings.SFXVolume = value);
        }

    }
}

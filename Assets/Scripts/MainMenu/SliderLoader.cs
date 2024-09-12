using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SliderLoader : MonoBehaviour
{
    public Slider MusicVolumeSlider;
    public Slider SfxVolumeSlider;

    public SetAudioLevels audioLevel;

    void Start()
    {
        int musicVolume = PlayerPrefs.GetInt("MusicVolume", 0);
        int sfxVolume = PlayerPrefs.GetInt("SfxVolume", 0);

        MusicVolumeSlider.value = musicVolume;
        SfxVolumeSlider.value = sfxVolume;

        audioLevel.SetMusicLevel(musicVolume);
        audioLevel.SetSfxLevel(sfxVolume);
    }
}

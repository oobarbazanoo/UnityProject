using UnityEngine;

public class SoundManager
{
    private bool soundOn, musicOn;

    public bool isSoundOn()
    {return this.soundOn;}

    public void setSound(bool val)
    {
        this.soundOn = val;
        PlayerPrefs.SetInt("sound", this.soundOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public bool isMusicOn()
    { return this.musicOn; }

    public void setMusic(bool val)
    {
        this.musicOn = val;
        PlayerPrefs.SetInt("music", this.musicOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    private SoundManager()
    {
        soundOn = PlayerPrefs.GetInt("sound", 1) == 1;
        musicOn = PlayerPrefs.GetInt("music", 1) == 1;
    }

    public static SoundManager Instance = new SoundManager();
}
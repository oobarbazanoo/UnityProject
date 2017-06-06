using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpWindowConfig : MonoBehaviour
{
    public ClickTrigger background, buttClose, buttSound, buttMusic;

    public Sprite soundOnSprite, musicOnSprite, soundOffSprite, musicOffSprite;

    void Start ()
    {
        addListenersToButts();  
        Time.timeScale = 0;

        setInitialSpritesForSettingsButtons();
    }

    private void setInitialSpritesForSettingsButtons()
    {
        SoundManager sm = SoundManager.Instance;
        setSpriteForSoundButton(sm.isSoundOn());
        setSpriteForMusicButton(sm.isMusicOn());
    }

    private void addListenersToButts()
    {
        background.signalOnClick.AddListener(this.closePopUp);
        buttClose.signalOnClick.AddListener(this.closePopUp);

        buttSound.signalOnClick.AddListener(this.toggleSound);

        buttMusic.signalOnClick.AddListener(this.toggleMusic);
    }

    private void toggleSound()
    {
        SoundManager sm = SoundManager.Instance;

        bool newVal = !sm.isSoundOn();

        sm.setSound(newVal);

        setSpriteForSoundButton(newVal);
    }

    private void setSpriteForSoundButton(bool newVal)
    {
        if (newVal)
        { changeButtonsSprite(buttSound.GetComponent<UIButton>(), soundOnSprite); }
        else
        { changeButtonsSprite(buttSound.GetComponent<UIButton>(), soundOffSprite); }
    }

    private void toggleMusic()
    {
        SoundManager sm = SoundManager.Instance;

        bool newVal = !sm.isMusicOn();

        sm.setMusic(newVal);

        setSpriteForMusicButton(newVal);
    }

    private void setSpriteForMusicButton(bool newVal)
    {
        if (newVal)
        { changeButtonsSprite(buttMusic.GetComponent<UIButton>(), musicOnSprite); }
        else
        { changeButtonsSprite(buttMusic.GetComponent<UIButton>(), musicOffSprite); }
    }

    private void changeButtonsSprite(UIButton buttonToChangeSprite, Sprite newSprite)
    {
        buttonToChangeSprite.normalSprite2D = newSprite;
        buttonToChangeSprite.hoverSprite2D = newSprite;
        buttonToChangeSprite.pressedSprite2D = newSprite;
        buttonToChangeSprite.disabledSprite2D = newSprite;
    }

    private void closePopUp()
    {
        Time.timeScale = 1;
        Destroy(this.gameObject);
    }

	// Update is called once per frame
	void Update () {
		
	}
}

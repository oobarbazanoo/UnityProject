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

        if(newVal)
        {buttSound.GetComponent<UI2DSprite>().sprite2D = soundOnSprite;}
        else
        {buttSound.GetComponent<UI2DSprite>().sprite2D = soundOffSprite;}
    }

    private void toggleMusic()
    {
        SoundManager sm = SoundManager.Instance;

        bool newVal = !sm.isMusicOn();

        sm.setMusic(newVal);

        if (newVal)
        {changeButtonsSprite(buttMusic.GetComponent<UIButton>(), musicOnSprite);}
        else
        {changeButtonsSprite(buttMusic.GetComponent<UIButton>(), musicOffSprite);}
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

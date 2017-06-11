using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WonPanel : MonoBehaviour
{
    public ClickTrigger background, buttClose, buttMenu, buttRetry;

    public Sprite noCrystal;

    public GameObject crystalsHolder;

    public UILabel numberOfFruit, numberOfCoins;

    void Start()
    {
        LevelController.current.cameraWhichLooksForRabbit.playSoundOfTheVictory(); 
        initializeCrystalSprites();
        initializeFruitsAndCoinsStat();
        addListenersToButts();
        Time.timeScale = 0;
    }

    private void initializeFruitsAndCoinsStat()
    {
        numberOfCoins.text = "+ " + LevelController.current.coinsWhichWillBeCollected;
        numberOfFruit.text = LevelController.current.fruitsCollected + "/" + LevelController.current.allFruitsOnTheLvl.Length;
    }

    private void initializeCrystalSprites()
    {
        bool[] collected = LevelController.current.brgCrystalsCollected;

        for(int i = 0; i < 3; i++)
        {
            if (!collected[i])
            { setToNoCrystal(i); }
        }
    }

    private void setToNoCrystal(int i)
    {
        GameObject crystalToChange = crystalsHolder.transform.Find(i + "").gameObject;

        crystalToChange.GetComponent<UI2DSprite>().sprite2D = noCrystal;
    }

    private void addListenersToButts()
    {
        background.signalOnClick.AddListener(this.goToMenu);
        buttClose.signalOnClick.AddListener(this.goToMenu);
        buttMenu.signalOnClick.AddListener(this.goToMenu);

        buttRetry.signalOnClick.AddListener(this.startAgain);
    }

    private void startAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void goToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("mainMenu");
    }
}

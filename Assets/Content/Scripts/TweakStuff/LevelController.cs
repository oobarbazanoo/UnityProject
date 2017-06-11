using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public Fruit[] allFruitsOnTheLvl;

    public int lvlNumber;

    public SpriteRenderer lockSprite;

    public SpriteRenderer check1LvlSprite, crystals1LvlSprite, fruits1LvlSprite,
                          check2LvlSprite, crystals2LvlSprite, fruits2LvlSprite;

    Vector3 startingPosition;
    private static GameObject rabbitObj;

    private static GameObject lifeBarObj;
    private static GameObject coinsBarObj;
    private static GameObject crystalsBarObj;
    private static GameObject fruitBarObj;

    public GameObject fruitFromEnvironment;

    public CameraConfig cameraWhichLooksForRabbit;

    public int lives, coinsAlreadyCollected, coinsWhichWillBeCollected, fruitsCollected, crystalsCollected;

    public Sprite notLife, life, blueCrystal, redCrystal, greenCrystal, greenFruit;

    public bool[] brgCrystalsCollected = { false, false, false };

    public GameObject lostPrefab;

    internal void fruitWasCollected()
    {
        GameObject fruitBar = GetFruitBar();

        fruitsCollected++;

        FruitBar fruitBarScript = fruitBar.GetComponent<FruitBar>();

        fruitBarScript.writeToLabel(fruitsCollected + "/" + this.allFruitsOnTheLvl.Length);
    }

    internal void crystalWasCollected(Crystal.TypeOfCrystal type)
    {
        GameObject crystalsBar = GetCrystalsBar();

        crystalsCollected++;

        GameObject crystalToChange = crystalsBar.transform.Find(type + "").gameObject;

        crystalToChange.GetComponent<UI2DSprite>().sprite2D = GetCrystalByType(type);

        checkCrystalInArray(type);
    }

    internal bool[] fruitsCollectedBoolArray()
    {
        bool[] res = new bool[allFruitsOnTheLvl.Length];

        for(int i = 0; i < res.Length; i++)
        {
            res[i] = allFruitsOnTheLvl[i].collected;
        }

        return res;
    }

    private void checkCrystalInArray(Crystal.TypeOfCrystal type)
    {
        if (type == Crystal.TypeOfCrystal.Blue)
        { brgCrystalsCollected[0] = true; }
        else if (type == Crystal.TypeOfCrystal.Red)
        { brgCrystalsCollected[1] = true; }
        else if (type == Crystal.TypeOfCrystal.Green)
        { brgCrystalsCollected[2] = true; }
    }

    private Sprite GetCrystalByType(Crystal.TypeOfCrystal type)
    {
        if(type == Crystal.TypeOfCrystal.Blue)
        { return blueCrystal; }

        if (type == Crystal.TypeOfCrystal.Red)
        { return redCrystal; }

        if (type == Crystal.TypeOfCrystal.Green)
        { return greenCrystal; }

        return null;
    }

    public void setStartPosition(Vector3 pos)
    {this.startingPosition = pos;}

    public static void SetRabbit(GameObject rabbit)
    { rabbitObj = rabbit; }

    public static void SetLifeBar(GameObject lifeBar)
    { lifeBarObj = lifeBar; }

    public static GameObject GetLifeBar()
    {
        if (lifeBarObj == null)
        { throw new UnityException("lifeBarObj is null!"); }
        return lifeBarObj;
    }

    public static void SetCoinsBar(GameObject coinsBar)
    { coinsBarObj = coinsBar; }

    public static GameObject GetCoinsBar()
    {
        if (coinsBarObj == null)
        { throw new UnityException("coinsBarObj is null!"); }
        return coinsBarObj;
    }

    public static void SetCrystalsBar(GameObject crystalsBar)
    { crystalsBarObj = crystalsBar; }

    public static GameObject GetCrystalsBar()
    {
        if (crystalsBarObj == null)
        { throw new UnityException("crystalsBarObj is null!"); }
        return crystalsBarObj;
    }

    public static void SetFruitBar(GameObject fruitBar)
    { fruitBarObj = fruitBar; }

    public static GameObject GetFruitBar()
    {
        if (fruitBarObj == null)
        { throw new UnityException("fruitBarObj is null!"); }
        return fruitBarObj;
    }

    public void onRabitDeath(RabbitController rabit)
    {
        current.cameraWhichLooksForRabbit.followRabbit = false;
        rabit.enabled = false;
        cameraWhichLooksForRabbit.playSoundRabbitDies();
        StartCoroutine(MoveAtTheBeginningLater(rabit));
    }

    System.Collections.IEnumerator MoveAtTheBeginningLater(RabbitController rabit)
    {
        yield return new WaitForSeconds(4f);
        LevelController.current.cameraWhichLooksForRabbit.followRabbit = true;
        rabit.enabled = true;
        rabit.gameObject.GetComponent<AnimateController>().animate("die", false);
        

        if (SceneManager.GetActiveScene().name == "chooseLevel")
        {
            moveRabbitToTheStartingPosition(rabit);
            yield break;
        }
        else
        { 
            if (lives == 0)
            {
                GameObject parent = UICamera.first.transform.parent.gameObject;
                NGUITools.AddChild(parent, lostPrefab);
            }
            else
            {
                decrementLives();
                moveRabbitToTheStartingPosition(rabit);

                RabbitStats rabbitStats = rabit.gameObject.GetComponent<RabbitStats>();
                rabbitStats.isDead = false;
            }
        }
    }

    private void moveRabbitToTheStartingPosition(RabbitController rabit)
    { rabit.transform.position = this.startingPosition;}

    private void decrementLives()
    {
        decrementLivesInLifeBar();
        lives--;
    }

    private void decrementLivesInLifeBar()
    {
        GameObject lifeBar = GetLifeBar();

        GameObject heartToChange = lifeBar.transform.Find(lives + "").gameObject;

        heartToChange.GetComponent<UI2DSprite>().sprite2D = notLife;
    }

    internal void addALife()
    {
        if (lives == 3)
        { return; }
        else
        {
            lives++;
            incrementLivesInLifebar();
        }
    }

    private void incrementLivesInLifebar()
    {
        GameObject lifeBar = GetLifeBar();

        GameObject heartToChange = lifeBar.transform.Find(lives + "").gameObject;

        heartToChange.GetComponent<UI2DSprite>().sprite2D = life;
    }

    public static GameObject getRabbit()
    {return rabbitObj;}

    public static LevelController current;
    void Awake()
    {
        current = this;
        lives = 3;
        crystalsCollected = 0;
        fruitsCollected = 0;
    }

    public void coinCollected()
    {
        this.coinsWhichWillBeCollected++;
        this.coinsAlreadyCollected++;
        incrementCoinsInBar();
    }

    private void incrementCoinsInBar()
    {
        if (coinsBarObj == null)
        { return; }

        UILabel labelWithNumberOfCoins = coinsBarObj.transform.Find("Label").gameObject.GetComponent<UILabel>();
        labelWithNumberOfCoins.text = makeNumberOfCoinsAnAppropriateString();
    }

    private string makeNumberOfCoinsAnAppropriateString()
    {
        string res = "";

        string withoutSpacesYet = this.coinsAlreadyCollected + "";

        for (int i = 0; i < 7; i++)
        {
            if (i % 2 == 0)
            {
                if(i/2 >= withoutSpacesYet.Length)
                {
                    res = "0" + res;
                }
                else
                {
                    res = withoutSpacesYet[withoutSpacesYet.Length - 1 - i/2] + res;
                }
            }
            else
            {
                res = " " + res;
            }

        }

        return res;
    }


    // Use this for initialization
    void Start ()
    {
        configCoins();

        if (fruitFromEnvironment != null)
        {
            this.allFruitsOnTheLvl = getAllFruitsInEnvironment();
            configFruits();
        }
        
        if(lockSprite != null)
        {
            configDoorWithLock();
            configDoorsInfo();
        }

	}

    private void configDoorsInfo()
    {
        configFirstDoor();
        configSecondDoor();
    }

    private void configSecondDoor()
    {
        if (!LevelStats.Instance.isPassedLvlNumber("2"))
        { check2LvlSprite.sprite = null; }

        if (LevelStats.Instance.isCollectedCrystalsLvlNumber("2"))
        { crystals2LvlSprite.sprite = blueCrystal; }

        if (LevelStats.Instance.isCollectedFruitsLvlNumber("2"))
        { fruits2LvlSprite.sprite = greenFruit; }
    }

    private void configFirstDoor()
    {
        if (!LevelStats.Instance.isPassedLvlNumber("1"))
        { check1LvlSprite.sprite = null; }

        if (LevelStats.Instance.isCollectedCrystalsLvlNumber("1"))
        { crystals1LvlSprite.sprite = blueCrystal; }

        if (LevelStats.Instance.isCollectedFruitsLvlNumber("1"))
        { fruits1LvlSprite.sprite = greenFruit; }
    }

    private void configFruits()
    {
        bool[] arrayForFruits = LevelStats.Instance.getFruitPrevBoolArray(this.lvlNumber);
        bool[] fruitsCollectedBoolArr = fruitsCollectedBoolArray();

        if ((arrayForFruits == null) || (arrayForFruits.Length != fruitsCollectedBoolArr.Length))
        { }
        else
        {
            for(int i = 0; i < arrayForFruits.Length; i++)
            {
                if(arrayForFruits[i])
                {
                    this.allFruitsOnTheLvl[i].makeTranslucent();
                }
            }
        }
    }

    private Fruit[] getAllFruitsInEnvironment()
    {
        return this.fruitFromEnvironment.GetComponentsInChildren<Fruit>();
    }

    private void configCoins()
    {
        this.coinsAlreadyCollected = LevelStats.Instance.getWholeNumberOfCollectedCoins();
        incrementCoinsInBar();
        this.coinsWhichWillBeCollected = 0;
    }

    private void configDoorWithLock()
    {
        bool getRidOfLock = LevelStats.Instance.isSecondLevelAvailable();

        if (getRidOfLock)
        { lockSprite.sprite = null; }
    }
}

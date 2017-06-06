using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    Vector3 startingPosition;
    private static GameObject rabbitObj;

    private static GameObject lifeBarObj;
    private static GameObject coinsBarObj;
    private static GameObject crystalsBarObj;
    private static GameObject fruitBarObj;

    public CameraConfig cameraWhichLooksForRabbit;

    private int lives;
    private int coinsCollected;
    private int fruitsCollected;

    public Sprite notLife, life, blueCrystal, redCrystal, greenCrystal;

    internal void fruitWasCollected()
    {
        GameObject fruitBar = GetFruitBar();

        fruitsCollected++;

        FruitBar fruitBarScript = fruitBar.GetComponent<FruitBar>();

        fruitBarScript.writeToLabel(fruitsCollected + "/" + fruitBarScript.numberOfFruitsInScene);
    }

    internal void crystalWasCollected(Crystal.TypeOfCrystal type)
    {
        GameObject crystalsBar = GetCrystalsBar();

        GameObject crystalToChange = crystalsBar.transform.Find(type + "").gameObject;

        crystalToChange.GetComponent<UI2DSprite>().sprite2D = GetCrystalByType(type);
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
        if (SceneManager.GetActiveScene().name == "chooseLevel")
        {
            moveRabbitToTheStartingPosition(rabit);
            return;
        }


        if (lives == 0)
        {
            cameraWhichLooksForRabbit.playSoundRabbitDies();
            SceneManager.LoadScene("chooseLevel");
        }
        else
        {
            cameraWhichLooksForRabbit.playSoundRabbitDies();
            decrementLives();
            moveRabbitToTheStartingPosition(rabit);

            RabbitStats rabbitStats = rabit.gameObject.GetComponent<RabbitStats>();
            rabbitStats.isDead = false;
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

    public static GameObject getRabbit()
    {return rabbitObj;}

    public static LevelController current;
    void Awake()
    {
        current = this;
        lives = 3;
        fruitsCollected = 0;
    }

    public void coinCollected()
    {
        this.coinsCollected++;
        incrementCoinsInBar();
    }

    private void incrementCoinsInBar()
    {
        UILabel labelWithNumberOfCoins = coinsBarObj.transform.Find("Label").gameObject.GetComponent<UILabel>();
        labelWithNumberOfCoins.text = makeNumberOfCoinsAnAppropriateString();
    }

    private string makeNumberOfCoinsAnAppropriateString()
    {
        string res = "";

        string withoutSpacesYet = this.coinsCollected + "";

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
        this.coinsCollected = 0;	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

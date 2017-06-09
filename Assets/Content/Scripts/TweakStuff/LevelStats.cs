using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStats : MonoBehaviour
{
    public static LevelStats Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {

    }

    internal void passedLevelNumber(int lvlNumber)
    {saveLvl(lvlNumber + "");}

    public bool isSecondLevelAvailable()
    {
        LevelInfo prevInfo = getPrevInfo("1");
        return prevInfo.passedTheWholeLevelOnce;
    }

    private void saveLvl(string lvlNumber)
    {
        LevelInfo prevInfo = getPrevInfo(lvlNumber);

        bool allCrystalsWereCollected = (3 == LevelController.current.crystalsCollected);
        if (allCrystalsWereCollected)
        { prevInfo.collectedAllCrystalsOnTheLevel = true; }

        prevInfo.passedTheWholeLevelOnce = true;

        setNewNumberOfCollectedCoins(getWholeNumberOfCollectedCoins() + LevelController.current.coinsWhichWillBeCollected);

        bool[] collectedFruitsInTheCurrentLevel = LevelController.current.fruitsCollectedBoolArray();
        bool[] collectedEarlier = prevInfo.fruitsWhichWereCollected;
        if ((collectedEarlier == null) || (collectedEarlier.Length != collectedFruitsInTheCurrentLevel.Length))
        { collectedEarlier = collectedFruitsInTheCurrentLevel; }
        else
        {
            for (int i = 0; i < collectedEarlier.Length; i++)
            {
                if (collectedFruitsInTheCurrentLevel[i])
                {
                    collectedEarlier[i] = true;
                }
            }
        }
        prevInfo.fruitsWhichWereCollected = collectedEarlier;

        saveInfo(lvlNumber, prevInfo);
    }

    private LevelInfo getPrevInfo(string numberOfTheLevel)
    {
        string str = PlayerPrefs.GetString("LevelInfo" + numberOfTheLevel, null);
        LevelInfo prevInfo = JsonUtility.FromJson<LevelInfo>(str);
        if (prevInfo == null)
        { prevInfo = new LevelInfo(); }
        return prevInfo;
    }

    private void saveInfo(string numberOfTheLevel, LevelInfo info)
    {
        string str = JsonUtility.ToJson(info);
        PlayerPrefs.SetString("LevelInfo" + numberOfTheLevel, str);
    }

    public int getWholeNumberOfCollectedCoins()
    {return PlayerPrefs.GetInt("allCoinsWhichWereCollected", 0);}

    private void setNewNumberOfCollectedCoins(int newAmount)
    { PlayerPrefs.SetInt("allCoinsWhichWereCollected", newAmount); }

    internal bool[] getFruitPrevBoolArray(int lvlNumber)
    {
        LevelInfo prevInfo = getPrevInfo(lvlNumber + "");
        return prevInfo.fruitsWhichWereCollected;
    }
}

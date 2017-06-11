using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStats : MonoBehaviour
{

    private const string SAVE_LVL_MARK = "lvlInfo2", SAVE_COINS_MARK = "coinsInfo2";
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

    public bool isPassedLvlNumber(string lvlNumber)
    {
        LevelInfo prevInfo = getPrevInfo(lvlNumber);
        return prevInfo.passedTheWholeLevelOnce;
    }

    public bool isCollectedCrystalsLvlNumber(string lvlNumber)
    {
        LevelInfo prevInfo = getPrevInfo(lvlNumber);
        return prevInfo.collectedAllCrystalsOnTheLevel;
    }

    public bool isCollectedFruitsLvlNumber(string lvlNumber)
    {
        LevelInfo prevInfo = getPrevInfo(lvlNumber);
        bool[] collectedFruits = prevInfo.fruitsWhichWereCollected;

        if (collectedFruits == null)
        { return false; }

        foreach (bool collectedThisOne in collectedFruits)
        {
            if (!collectedThisOne)
            { return false; }
        }
        return true;
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
        string str = PlayerPrefs.GetString(SAVE_LVL_MARK + numberOfTheLevel, null);
        LevelInfo prevInfo = JsonUtility.FromJson<LevelInfo>(str);
        if (prevInfo == null)
        { prevInfo = new LevelInfo(); }
        return prevInfo;
    }

    private void saveInfo(string numberOfTheLevel, LevelInfo info)
    {
        string str = JsonUtility.ToJson(info);
        PlayerPrefs.SetString(SAVE_LVL_MARK + numberOfTheLevel, str);
    }

    public int getWholeNumberOfCollectedCoins()
    {return PlayerPrefs.GetInt(SAVE_COINS_MARK, 0);}

    private void setNewNumberOfCollectedCoins(int newAmount)
    { PlayerPrefs.SetInt(SAVE_COINS_MARK, newAmount); }

    internal bool[] getFruitPrevBoolArray(int lvlNumber)
    {
        LevelInfo prevInfo = getPrevInfo(lvlNumber + "");
        return prevInfo.fruitsWhichWereCollected;
    }
}

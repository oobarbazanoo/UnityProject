

using System.Collections.Generic;

[System.Serializable]
public class LevelInfo
{
    public bool collectedAllCrystalsOnTheLevel = false;
    public bool collectedAllfruitsOnTheLevel = false;
    public bool passedTheWholeLevelOnce = false;

    public bool[] fruitsWhichWereCollected;
}

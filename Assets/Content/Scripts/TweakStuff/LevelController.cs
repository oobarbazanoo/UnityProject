using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    Vector3 startingPosition;
    private static GameObject rabbitObj;
    private static GameObject lifeBarObj;

    private int lives;

    public void setStartPosition(Vector3 pos)
    {this.startingPosition = pos;}

    public static void SetRabbit(GameObject rabbit)
    { rabbitObj = rabbit; }

    public static void SetLifeBar(GameObject lifeBar)
    { lifeBarObj = lifeBar; }

    public static GameObject GetLifeBar()
    {
        if (lifeBarObj == null)
        { throw new UnityException("lifeBarObj is not set yet!"); }
        return lifeBarObj;
    }

    public void onRabitDeath(RabbitController rabit)
    {
        //При смерті кролика повертаємо на початкову позицію
        rabit.transform.position = this.startingPosition;
        RabbitStats rabbitStats = rabit.gameObject.GetComponent<RabbitStats>();
        rabbitStats.isDead = false;

        if(lives == 0)
        { SceneManager.LoadScene("chooseLevel"); }
        else
        { decrementLives(); }
    }

    private void decrementLives()
    {
        decrementLivesInLifeBar();
        lives--;
    }

    private void decrementLivesInLifeBar()
    {
        GameObject lifeBar = GetLifeBar();

        GameObject heartToChange = lifeBar.transform.Find(lives + "").gameObject;

        heartToChange.GetComponent<UI2DSprite>().sprite2D = (Sprite)AssetDatabase.LoadAssetAtPath("Assets/Content/UI/level/notLife.png", typeof(Sprite));
    }

    public static GameObject getRabbit()
    {return rabbitObj;}

    public static LevelController current;
    void Awake()
    {
        current = this;
        lives = 3;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

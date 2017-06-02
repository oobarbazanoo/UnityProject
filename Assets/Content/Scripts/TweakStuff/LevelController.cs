using UnityEngine;

public class LevelController : MonoBehaviour
{
    Vector3 startingPosition;
    private static GameObject rabbitObj;

    public void setStartPosition(Vector3 pos)
    {
        this.startingPosition = pos;
    }

    public static void SetRabbit(GameObject rabbit)
    { rabbitObj = rabbit; }

    public void onRabitDeath(RabbitController rabit)
    {
        //При смерті кролика повертаємо на початкову позицію
        rabit.transform.position = this.startingPosition;
        RabbitStats rabbitStats = rabit.gameObject.GetComponent<RabbitStats>();
        rabbitStats.isDead = false;
    }

    public static GameObject getRabbit()
    {return rabbitObj;}

    public static LevelController current;
    void Awake()
    {
        current = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

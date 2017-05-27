using UnityEngine;
using System.Collections;

public class FlashObject : MonoBehaviour
{

    private Material mat;
    private Color[] colors = { Color.white, Color.red };
    private bool shouldBlink;

    public void Awake()
    {
        shouldBlink = false;
        mat = GetComponent<SpriteRenderer>().material;
    }

    public void startBlinking()
    {
        shouldBlink = true;
        StartCoroutine(Flash(0.05f));
    }

    IEnumerator Flash(float intervalTime)
    {
        int index = 0;
        while (shouldBlink)
        {
            mat.color = colors[index % 2];

            index++;
            yield return new WaitForSeconds(intervalTime);
        }
    }

    public void stopBlinking()
    {
        shouldBlink = false;
        mat.color = Color.white;
    }

}
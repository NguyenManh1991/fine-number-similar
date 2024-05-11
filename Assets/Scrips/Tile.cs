using System.Collections;

using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [SerializeField] TextMeshPro cube;
    [SerializeField] GameObject mask;
    [SerializeField] Collider TileCollider;
    public int tileValue;
    float delayTime;
    IEnumerator enumerator;

    public void SetText(int value)
    {
        cube.text = value.ToString();
        tileValue = value;
    }

    public void SetDelayTime(float time)
    {
       delayTime = time;
    }
    public void TurnOnPurdah()
    {
        mask.SetActive(false);
        enumerator = InactiveAfter(delayTime);
        StartCoroutine(enumerator);
        
    }

    public void TurnOffPurdah()
    {
        StopCoroutine(enumerator);
        TileCollider=gameObject.GetComponent<Collider>();   
       TileCollider.enabled = false;
    }
    IEnumerator InactiveAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        mask.SetActive(true);

    }


}

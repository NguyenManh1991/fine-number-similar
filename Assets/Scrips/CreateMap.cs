using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateMap : MonoBehaviour
{
    
    [SerializeField] Tile tiles;
    [SerializeField] int columb, row;

    Tile check;
    Vector3 hitPosition;
    Dictionary<Vector3,Tile> dicTile = new();
    [SerializeField] List<int> numberValue = new List<int>();
    [SerializeField] int setLength = 2;
    [SerializeField] float delayTime = 0.7f;
    private void Start()
    {
        SetMap();
        
        RandomShuffle();
    }
    private void Update()
    {
        Raycat();
    }
    void SetMap()
    {
        for (int i = 0; i < columb; i++)
        {
            for (int j = 0; j < row; j++)
            {
                var tile = Instantiate(tiles);
                Vector3 tilePosition;
                tilePosition.x = i; tilePosition.y = j; tilePosition.z = 0;
                tile.transform.position = tilePosition;
                tile.transform.SetParent(transform, false);
                dicTile.Add(tilePosition,tile);

            }
        }
    }
    

    void Raycat()
    {
        if (Input.GetMouseButtonDown(0))
        {

            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                hitPosition = hit.transform.position;
                if (dicTile.ContainsKey(hitPosition))
                {
                    dicTile[hitPosition].TurnOnPurdah();
                    if (check != null)
                    {
                        if (dicTile[hitPosition].tileValue == check.tileValue)
                        {
                            if (check.transform.position == hitPosition)
                            {
                                return;
                            }
                            dicTile[hitPosition].TurnOffPurdah();
                            check.TurnOffPurdah();
                            check = null;
                        }
                    }
                    check = dicTile[hitPosition];
                    StartCoroutine(ResetCheck());                   
                }
                
            }
        }


    }
    void RandomShuffle()
    {
        Random();
        numberValue.Shuffle();
        int count = 0;
        foreach(var item in dicTile)
        {
            item.Value.SetDelayTime(delayTime);
            item.Value.SetText(numberValue[count]);
            count++;
        }

    }
    void Random()
    {
        numberValue.Clear();
        if (setLength % 2 != 0) { return; }
        if ((row * columb) % setLength != 0)
        {
            return;
        }
        for (int i = 0; i < (row * columb) / setLength; i++)
        {
            for (int j = 0; j < setLength; j++)
            {
                numberValue.Add(i);
            }
        }
    }

    IEnumerator ResetCheck() 
    {
        yield return new WaitForSeconds(delayTime);
        check= null;    
    }

}

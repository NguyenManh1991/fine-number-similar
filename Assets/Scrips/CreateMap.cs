using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateMap : MonoBehaviour
{
    [SerializeField] GameObject purdah;
    [SerializeField] Tile map;
    [SerializeField] int columb, row;
    [SerializeField] Dictionary<Vector3, GameObject> dicPurdah = new();
    GameObject purdah1;
    Vector3 hitPosition;
    List<Tile> list = new();
    [SerializeField] List<int> list2 = new List<int>();
    [SerializeField]int h = 2;
    
    private void Start()
    {
        SetMap();
        SetPordah();
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
                var tile = Instantiate(map);
                Vector3 tilePosition;
                tilePosition.x = i; tilePosition.y = j; tilePosition.z = 0;
                tile.transform.position = tilePosition; 
                tile.transform.SetParent(transform, false);
                list.Add(tile);

            }
        }
    }
    void SetPordah()
    {
        for (int i = 0; i < columb; i++)
        {
            for (int j = 0; j < row; j++)
            {
                purdah1 = Instantiate(purdah);
                Vector3 tilePosition;
                tilePosition.x = i; tilePosition.y = j; tilePosition.z = -0.5f;
                purdah1.transform.position = tilePosition;
                purdah1.transform.SetParent(transform, false);
                dicPurdah.Add(tilePosition, purdah1);
                //listPurdah[i].SetActive(false);
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
                if (dicPurdah.ContainsKey(hitPosition))
                {
                    dicPurdah[hitPosition].SetActive(false);



                }
            }
        }


    }
    void RandomShuffle()
    {
        Random();
        list2.Shuffle();

        for (int i = 0; i < list2.Count; i++)
        {
            list[i].SetText($"{list2[i]}");
        }

    }
    void Random()
    {
        list2.Clear();
        if (h % 2 != 0) { return; }
        if ((row * columb) % h != 0)
        {
            return;
        }
        for (int i = 0; i < (row * columb) / h; i++)
        {
            for (int j = 0; j < h; j++)
            {
                list2.Add(i);
            }
        }
    }
}

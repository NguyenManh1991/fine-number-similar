using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] TextMeshPro cube;

    public void SetText(string text)
    {
        cube.text = text;
    }


        
}

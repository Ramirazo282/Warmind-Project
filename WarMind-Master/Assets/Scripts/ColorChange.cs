using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChange : MonoBehaviour {
    [Header ("Colors")]
    public Color startingColor;
    public Color selectedColor;
    //public Material evisiveMaterial;

    Player player;
    public void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void OnMouseEnter()
    {
        if (player.canBeSelected)
           GetComponent<Renderer>().material.SetColor("_Color", selectedColor);
    }
    void OnMouseExit()
    {
        if (player.canBeSelected)
            GetComponent<Renderer>().material.SetColor("_Color", startingColor);
    }
}

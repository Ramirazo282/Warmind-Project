using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCardID : MonoBehaviour
{
    public int CardID;
    public GameObject[] gameObjects;

    private void Start()
    {
        List<Material> materials = new List<Material>();

        foreach(GameObject g in gameObjects)
        {
            Renderer r = g.GetComponent<Renderer>();
            materials.AddRange(r.materials);
        }

        foreach(Material m in materials)
        {
            if(m.HasProperty("_ID"))
            {
                m.SetInt("_ID", CardID);
            }
        }
    }
}

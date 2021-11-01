using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    public List<BackgroundElement> elements;

    public static BackgroundScroll Instance;

    private void Awake()
    {
        Instance = this;
    }
    
    public void UpdateBG()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            float currentOffset = elements[i].rend.material.mainTextureOffset.x;
            currentOffset += Time.deltaTime * elements[i].speed;
            elements[i].rend.material.SetTextureOffset("_MainTex", new Vector2(currentOffset, 0));
        }
    }

}

[Serializable]
public class BackgroundElement
{
    public Renderer rend;
    public float speed;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBuffElement : MonoBehaviour
{
    [SerializeField] private Image fill;
    public BuffEnum buffType;
    public void Fill(float value)
    {
        fill.fillAmount = value;
    }
}

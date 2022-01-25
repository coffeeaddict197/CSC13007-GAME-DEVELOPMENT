using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDialog : MonoBehaviour
{

    public virtual void OnShow()
    {
        
    }

    public void OnHide()
    {
        this.gameObject.SetActive(false);
    }
}

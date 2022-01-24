using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public void Reset() => anim.SetTrigger("Reset");
}

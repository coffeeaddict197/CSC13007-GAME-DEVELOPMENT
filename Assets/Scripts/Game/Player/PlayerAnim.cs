using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    [SerializeField] private Animator anim;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            anim.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("Run");
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            anim.SetTrigger("Reset");
        }
    }
}

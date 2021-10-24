using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimCallBack : MonoBehaviour
{
    [Header("Refence")]
    public PlayerGear gears;

    public void OnAttack()
    {
        Debug.Log(gears.TakeDamage());
    }
}

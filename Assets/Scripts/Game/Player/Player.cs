using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoSingleton<Player>
{
    public PlayerGear gears;

    public static Action<int> onPlayerTakeDamagae;
}

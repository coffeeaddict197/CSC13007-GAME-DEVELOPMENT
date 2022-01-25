using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    private void Start()
    {
        SoundManager.Instance.Play("Game_BG",AudioType.Soundtrack,1f);
    }
}

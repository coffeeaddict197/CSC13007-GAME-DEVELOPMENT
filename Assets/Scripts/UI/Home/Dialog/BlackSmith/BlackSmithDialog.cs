using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackSmithDialog : BaseDialog
{
    public override void OnShow()
    {
        SoundManager.Instance.Play("Black_Smith", AudioType.FX, 1f);
    }

}

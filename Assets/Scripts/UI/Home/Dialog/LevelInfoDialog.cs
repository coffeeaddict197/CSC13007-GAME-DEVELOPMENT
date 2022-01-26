using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelInfoDialog : BaseDialog
{
    [SerializeField] private TextMeshProUGUI txt_levelStage;
    private void OnEnable()
    {
        txt_levelStage.text = "Stage " + ButtonLevel.levelClicked.ToString();
    }
}

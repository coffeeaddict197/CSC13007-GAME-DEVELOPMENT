using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GearDialog : BaseDialog
{
    [SerializeField] private Button upgradeButton;

    private void Start()
    {
        upgradeButton.onClick.AddListener(OnShowUpgradeDialog);
    }


    public void OnShowUpgradeDialog()
    {
        DialogManager.Instance.OnShowDialog<GearUpgradeDialog>("Dialog/Home/DialogUprade",DialogType.DialogScaleWithHeigh);
    }

}

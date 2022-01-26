using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastBattleButton : MonoBehaviour
{
    public void OnClick()
    {
        ButtonLevel.levelClicked = PlayerDataManager.Instance.data.LevelDatas.archiveLevel;
        DialogManager.Instance.OnShowDialog<BaseDialog>("Dialog/Home/LevelInfo",
            DialogType.DialogWithoutNavigate);
    }
}

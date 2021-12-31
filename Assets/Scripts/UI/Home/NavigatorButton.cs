using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NavigatorEnum
{
    Shop,
    Gears,
    Village,
    Map,
    Setting
}

public class NavigatorButton : MonoBehaviour
{
    public NavigatorEnum typeNavigator;

    private static bool oneTimesSetup;
    public static Dictionary<NavigatorEnum, Action> navigatorPrototype;

    private void Awake()
    {
        if (!oneTimesSetup)
        {
            navigatorPrototype = new Dictionary<NavigatorEnum, Action>()
            {
                {
                    NavigatorEnum.Map,
                    ShowMap
                },
                {
                    NavigatorEnum.Village,
                    ShowVillage
                },
                {
                    NavigatorEnum.Gears,
                    ShowGear
                }
            };

            oneTimesSetup = true;
        }
        
        GetComponent<Button>().onClick.AddListener(OnNavigate);
    }

    public void OnNavigate()
    {
        navigatorPrototype[typeNavigator]?.Invoke();
    }

    void ShowMap()
    {
        DialogManager.Instance.OnShowDialogWithTransition<BaseDialog>("Dialog/Home/Maps", DialogType.DialogWithNavigate);
    }

    void ShowVillage()
    {
        DialogManager.Instance.DisableAllDialog();
    }

    void ShowGear()
    {
        DialogManager.Instance.OnShowDialogWithTransition<BaseDialog>("Dialog/Home/Gears", DialogType.DialogScaleWithHeigh);
    }

    void ShowSetting()
    {
        
    }

    void ShowShop()
    {
        
    }
}
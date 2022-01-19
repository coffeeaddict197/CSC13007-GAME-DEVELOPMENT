using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public enum NavigatorEnum
{
    BlackSmith,
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
                },
                {
                    NavigatorEnum.BlackSmith,
                    ShowBlackSmith
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

    async void ShowVillage()
    {
        if (!DialogManager.Instance.IsDisableAll())
        {
            DialogManager.Instance.DisableAllDialog();
            await UniTask.Delay(TimeSpan.FromSeconds(0.8f), cancellationToken: this.GetCancellationTokenOnDestroy());
            HomeScene.Instance.OpenVillageAnim();
        }
    }

    void ShowGear()
    {
        DialogManager.Instance.OnShowDialogWithTransition<BaseDialog>("Dialog/Home/Gears", DialogType.DialogScaleWithHeigh);
    }

    void ShowSetting()
    {
        
    }

    void ShowBlackSmith()
    {
        DialogManager.Instance.OnShowDialogWithTransition<BaseDialog>("Dialog/Home/BlackSmithDialog", DialogType.DialogWithoutNavigate);
    }
}
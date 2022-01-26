using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterLevelInfo : MonoBehaviour
{
    public MonsterAssets monsterAssetsConfig;
    public List<MonsterLevelReview> monsterImg;

    private void OnEnable()
    {
        Setup();
    }

    void Setup()
    {
        var currentLevelClicked = ButtonLevel.levelClicked;
        var levelConfig = LevelAssetsConfigs.Instance.GetLevel(currentLevelClicked);
        var listMonster = levelConfig?.listMonster;
        if (listMonster != null)
        {
            for (int i = 0; i < listMonster.Count; i++)
            {
                monsterImg[i].monsterImg.sprite = monsterAssetsConfig.GetMonsterAsset(listMonster[i].name).sprRepresent;
                monsterImg[i].monsterGen.sprite = monsterAssetsConfig.GetMonsterGenAsset(listMonster[i].gen).sprRepresent;
            }
        }
    }

}

[Serializable]
public class MonsterLevelReview
{
    public Image monsterImg;
    public Image monsterGen;
}

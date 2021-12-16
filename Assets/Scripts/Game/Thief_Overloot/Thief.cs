using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Thief : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private float PosX = 3;
    

    public IEnumerator ThiefAppear()
    {
        bool appearComplete = false;
        _animator.SetTrigger("Appear");
        this.transform.DOLocalMoveX(0, 0.3F).From(PosX).OnComplete(() =>
        {
            appearComplete = true;
        });
        yield return new WaitUntil(() => appearComplete);
        yield return new WaitForSeconds(0.5f);
        _animator.SetTrigger("Talk");

    }

    public IEnumerator ThiefStartLoot()
    {
        _animator.SetTrigger("Rush");
        this.transform.DOMoveX(Player.Instance.transform.position.x, 0.6f);
        yield return new WaitForSeconds(0.6f);
        _animator.SetTrigger("Loot");
        yield return new WaitForSeconds(2f);
        _animator.SetTrigger("Rush");
        this.transform.localScale = new Vector3(-1, 1, 1);
        this.transform.DOLocalMoveX(3, 0.8f);
        OverlootEvent.Instance.isPlayingEvent = false;

    }
}

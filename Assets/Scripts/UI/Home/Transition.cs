using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : MonoSingleton<Transition>
{
    [SerializeField] private Animator _animator;

    public void PlayTransition(string transitionName)
    {
        _animator.Play(transitionName);
    }
    
}

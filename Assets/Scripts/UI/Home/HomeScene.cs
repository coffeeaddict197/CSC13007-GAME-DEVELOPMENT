using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeScene : MonoSingleton<HomeScene>
{
    [SerializeField] Animator _openMapAnim;
    // Start is called before the first frame update
    void Start()
    {
        Transition.Instance.PlayTransition("OpenAnim");
        SoundManager.Instance.Play("Home_BG",AudioType.Soundtrack,1f);
    }

    public void OpenVillageAnim()
    {
        _openMapAnim.Rebind();
        _openMapAnim.Update(0);
    }
    
}

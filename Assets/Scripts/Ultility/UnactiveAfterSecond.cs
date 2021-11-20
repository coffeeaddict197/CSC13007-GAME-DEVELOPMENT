using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnactiveAfterSecond : MonoBehaviour
{
    [SerializeField] private float time;
    void OnEnable()
    {
        StartCoroutine(UnactiveAfterTime(time));
    }

    // Update is called once per frame
    
    IEnumerator UnactiveAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
    }
}

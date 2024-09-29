using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpherePicker : MonoBehaviour {

    public GameObject comingSoon;
    Animator animator;
    float secondsToWait = 2f;

    private void Start()
    {
        animator = comingSoon.GetComponent<Animator>();
    }

    public void OpenMenu()
    {
        animator.SetBool("Close", false);
        animator.SetBool("Open", true);
        Invoke("CloseMenu", secondsToWait);
    }

    public void CloseMenu()
    {
        animator.SetBool("Open", false);
        animator.SetBool("Close", true);
    }
}

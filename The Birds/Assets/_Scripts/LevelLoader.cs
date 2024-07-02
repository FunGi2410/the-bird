using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Animator animator;
    public float transitionTime = 1f;

    public void StartTransitionLoadLevel()
    {
        StartCoroutine(LoadLevelTransition());
    }

    IEnumerator LoadLevelTransition()
    {
        this.animator.SetTrigger("start");

        yield return new WaitForSeconds(transitionTime);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ObjectTest : MonoBehaviour, IPointerDownHandler
{
    public Transform posTarget;
    public void OnPointerDown(PointerEventData eventData)
    {
        MoveToTarget(posTarget);
    }

    public void MoveToTarget(Transform posTarget)
    {
        transform.DOMove(posTarget.position, 0.5f);
    }

    public void LoadScene()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}

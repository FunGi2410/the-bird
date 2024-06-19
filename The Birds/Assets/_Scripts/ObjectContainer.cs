using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectContainer : MonoBehaviour
{
    public bool isFull;
    public GameManager gameManager;
    public Image backgroundImage;

    private void Start()
    {
        this.gameManager = GameManager.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ObjectDrag"))
        {
            if (this.gameManager.draggingObj != null && this.isFull == false)
            {
                this.gameManager.currentContaiter = this.gameObject;
                this.backgroundImage.enabled = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        this.backgroundImage.enabled = false;
        this.gameManager.currentContaiter = null;
    }
}

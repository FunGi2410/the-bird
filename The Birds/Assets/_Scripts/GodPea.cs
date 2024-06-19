using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodPea : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            print("Game Over");
            GameManager.instance.OnGameOver();
        }
    }
}

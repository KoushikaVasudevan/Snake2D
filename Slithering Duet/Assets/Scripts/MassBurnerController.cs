using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MassBurnerController : MonoBehaviour
{
    public SnakeController snakeController;

    void OnTriggerEnter2D(Collider2D collision)
    {
        snakeController = collision.gameObject.GetComponent<SnakeController>();
        if (snakeController != null)
        {
            SoundManager.Instance.Play(SoundManager.Sounds.HeartPickup);
            snakeController.RemoveSnakeSegment();

            Debug.Log("MassBurner destroyed");
            Destroy(gameObject);
        }
    }
}

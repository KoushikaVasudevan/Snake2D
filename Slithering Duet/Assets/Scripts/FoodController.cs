using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour
{
    public SnakeController snakeController;

    void OnTriggerEnter2D(Collider2D collision)
    {
        snakeController = collision.gameObject.GetComponent<SnakeController>();
        if(snakeController != null)
        {
            SoundManager.Instance.Play(SoundManager.Sounds.HeartPickup);
            snakeController.AddSnakeSegment();

            Debug.Log("Food destroyed");
            Destroy(gameObject);
        }
    }
}

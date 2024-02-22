using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedPowerup : MonoBehaviour
{
    public SnakeController snakeController;

    void OnTriggerEnter2D(Collider2D collision)
    {
        snakeController = collision.gameObject.GetComponent<SnakeController>();
        if (snakeController != null)
        {
            SoundManager.Instance.Play(SoundManager.Sounds.PowerupPickup);
            snakeController.IsSpeedIncreased = true;

            Debug.Log("Speed increase powerup activated! " + collision.gameObject.tag + "'s speed is increased");
            Destroy(gameObject);
        }
    }
}

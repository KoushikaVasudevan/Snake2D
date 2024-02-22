using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreController : MonoBehaviour
{
    public SnakeController snakeController;

    void OnTriggerEnter2D(Collider2D collision)
    {
        snakeController = collision.gameObject.GetComponent<SnakeController>();
        if (snakeController != null)
        {
            SoundManager.Instance.Play(SoundManager.Sounds.PowerupPickup);
            snakeController.IsDoubleScore = true;

            Debug.Log("Double the score powerup activated! " + collision.gameObject.tag + "'s score is doubled");
            Destroy(gameObject);
        }
    }
}

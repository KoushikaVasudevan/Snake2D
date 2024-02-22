using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerupController : MonoBehaviour
{
    public SnakeController snakeController;

    void OnTriggerEnter2D(Collider2D collision)
    {
        snakeController = collision.gameObject.GetComponent<SnakeController>();
        if (snakeController != null)
        {
            SoundManager.Instance.Play(SoundManager.Sounds.PowerupPickup);
            snakeController.IsShieldActivated = true;

            Debug.Log("Shield Activated");
            Destroy(gameObject);
        }
    }
}

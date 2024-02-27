using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseSpeedPowerup : MonoBehaviour, IPowerup
{
    public void ApplyPowerup(SnakeController snakeController)
    {
        SoundManager.Instance.Play(SoundManager.Sounds.PowerupPickup);
        snakeController.ActivateIncreaseSpeed();

        Destroy(gameObject);
    }
}

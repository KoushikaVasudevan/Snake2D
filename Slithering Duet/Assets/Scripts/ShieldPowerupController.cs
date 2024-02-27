using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPowerupController : MonoBehaviour, IPowerup
{
    public void ApplyPowerup(SnakeController snakeController)
    {
        SoundManager.Instance.Play(SoundManager.Sounds.PowerupPickup);
        snakeController.ActivateShield();

        Destroy(gameObject);
    }
}

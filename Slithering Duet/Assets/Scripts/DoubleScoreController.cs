using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleScoreController : MonoBehaviour, IPowerup
{
    public void ApplyPowerup(SnakeController snakeController)
    {
        SoundManager.Instance.Play(SoundManager.Sounds.PowerupPickup);
        snakeController.ActivateDoubleScore();

        Destroy(gameObject);
    }
}

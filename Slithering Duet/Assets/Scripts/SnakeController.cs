using System.Collections.Generic;
using System;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float StepDuration = 0.01f;
    private float timeBeforeNextStep;
    public Direction snakeDirection;

    public bool IsUsingCursor = false;
    public bool IsShieldActivated = false;
    public bool IsDoubleScore = false;
    public bool IsSpeedIncreased = false;

    public float CoolDownTime = 3f;
    private float powerupDuration;

    private Vector2 headPos;
    private int unitsToMove;

    public List<GameObject> SnakeSegments;

    public float topBoundary;
    public float bottomBoundary;

    public float rightBoundary;
    public float leftBoundary;

    public ScoreController scoreController;
    public GameOverController gameOverController;

    void Start()
    {
        snakeDirection = Direction.UP;
        timeBeforeNextStep = StepDuration;
        powerupDuration = CoolDownTime;
        unitsToMove = 1;

        headPos = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        if (!IsUsingCursor)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if(snakeDirection == Direction.DOWN)
                {
                    snakeDirection = Direction.DOWN;
                }
                else
                {
                    snakeDirection = Direction.UP;
                }
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                if (snakeDirection == Direction.UP)
                {
                    snakeDirection = Direction.UP;
                }
                else
                {
                    snakeDirection = Direction.DOWN;
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (snakeDirection == Direction.RIGHT)
                {
                    snakeDirection = Direction.RIGHT;
                }
                else
                {
                    snakeDirection = Direction.LEFT;
                }
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                if (snakeDirection == Direction.LEFT)
                {
                    snakeDirection = Direction.LEFT;
                }
                else
                {
                    snakeDirection = Direction.RIGHT;
                }
            }
        } 
        else if(IsUsingCursor)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (snakeDirection == Direction.DOWN)
                {
                    snakeDirection = Direction.DOWN;
                }
                else
                {
                    snakeDirection = Direction.UP;
                }
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (snakeDirection == Direction.UP)
                {
                    snakeDirection = Direction.UP;
                }
                else
                {
                    snakeDirection = Direction.DOWN;
                }
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (snakeDirection == Direction.RIGHT)
                {
                    snakeDirection = Direction.RIGHT;
                }
                else
                {
                    snakeDirection = Direction.LEFT;
                }
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (snakeDirection == Direction.LEFT)
                {
                    snakeDirection = Direction.LEFT;
                }
                else
                {
                    snakeDirection = Direction.RIGHT;
                }
            }
        }

        if((IsShieldActivated) || (IsDoubleScore) || (IsSpeedIncreased))
        {
            powerupDuration -= Time.deltaTime;
        }

        if (powerupDuration < 0)
        {
            if(IsShieldActivated)
            {
                Debug.Log(gameObject.tag + "'s shield deactivated");
                IsShieldActivated = false;
            }
            if (IsDoubleScore)
            {
                Debug.Log(gameObject.tag + "'s double the score powerup deactivated");
                IsDoubleScore = false;
            }
            if (IsSpeedIncreased)
            {
                Debug.Log(gameObject.tag + "'s increase in speed powerup deactivated");
                IsSpeedIncreased = false;
            }
            powerupDuration = CoolDownTime;
        }

        timeBeforeNextStep -= Time.deltaTime;

        if (timeBeforeNextStep < 0)
        {
            PlayCycle();

            timeBeforeNextStep = StepDuration;
        }
    }

    public void AddSnakeSegment()
    {
        GameObject massGainSegment = Instantiate(SnakeSegments[SnakeSegments.Count - 1], SnakeSegments[SnakeSegments.Count - 1].transform.position, SnakeSegments[SnakeSegments.Count - 1].transform.rotation, this.transform.parent);

        SnakeSegments.Add(massGainSegment);

        if(IsDoubleScore)
        {
            scoreController.IncreaseScore(2);
        }
        else
        {
            scoreController.IncreaseScore(1);
        }
    }

    public void RemoveSnakeSegment()
    {
        if(SnakeSegments.Count <= 2)
        {
            Debug.Log(gameObject.tag + " low on Health, cannot burn mass");
        }
        else
        {
            GameObject massBurnSegment = SnakeSegments[SnakeSegments.Count - 1];
            SnakeSegments.Remove(SnakeSegments[SnakeSegments.Count - 1]);

            Destroy(massBurnSegment);
        }
     
        scoreController.DecreaseScore(1);
    }

    private void PlayCycle()
    {
        if(IsSpeedIncreased)
        {
            unitsToMove = 2;
        }
        else
        {
            unitsToMove = 1;
        }
        for (int i = (SnakeSegments.Count-1); i > 0; i--)
        {
            SnakeSegments[i].transform.position = SnakeSegments[i - 1].transform.position;   
        }
        SnakeSegments[0].transform.position = headPos;

        switch (snakeDirection)
        {
            case Direction.UP:
                headPos.y += unitsToMove;
                if (headPos.y > topBoundary)
                {
                    headPos.y = bottomBoundary + 1;
                }
                break;

            case Direction.DOWN:
                headPos.y -= unitsToMove;
                if (headPos.y < bottomBoundary)
                {
                    headPos.y = topBoundary - 1;
                }
                break;

            case Direction.RIGHT:
                headPos.x += unitsToMove;
                if (headPos.x > rightBoundary)
                {
                    headPos.x = leftBoundary + 1;
                }
                break;

            case Direction.LEFT:
                headPos.x -= unitsToMove;
                if (headPos.x < leftBoundary)
                {
                    headPos.x = rightBoundary - 1;
                }
                break;
        }

        transform.position = headPos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Segment")
        {
            if (IsShieldActivated)
            {
                Debug.Log("Player2's shield activated");
            }
            else
            {
                Debug.Log("Game Over!");
                gameOverController.SnakeDied();

                Time.timeScale = 0;
            }
        }
    }
}

public enum Direction
{
    UP,
    DOWN,
    RIGHT,
    LEFT
}

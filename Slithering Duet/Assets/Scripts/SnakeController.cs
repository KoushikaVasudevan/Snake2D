using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    [SerializeField] private float stepDuration = 0.01f;
    private float timeBeforeNextStep;
    [SerializeField] private Direction snakeDirection;

    [SerializeField] private bool isUsingCursor = false;
    [SerializeField] private bool isShieldActivated = false;

    [SerializeField] private float CoolDownTime = 3f;
    [SerializeField] private int incrementalScore = 1;

    [SerializeField] private Vector2 headPos;
    [SerializeField] private int unitsToMove;

    [SerializeField] private List<GameObject> SnakeSegments;

    [SerializeField] private float topBoundary;
    [SerializeField] private float bottomBoundary;
    [SerializeField] private float rightBoundary;
    [SerializeField] private float leftBoundary;

    [SerializeField] private ScoreController scoreController;
    [SerializeField] private GameOverController gameOverController;

    void Start()
    {
        snakeDirection = Direction.UP;
        timeBeforeNextStep = stepDuration;
        unitsToMove = 1;

        headPos = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        if (!isUsingCursor)
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
        else if(isUsingCursor)
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

        timeBeforeNextStep -= Time.deltaTime;

        if (timeBeforeNextStep < 0)
        {
            PlayCycle();

            timeBeforeNextStep = stepDuration;
        }
    }

    public void AddSnakeSegment()
    {
        GameObject massGainSegment = Instantiate(SnakeSegments[SnakeSegments.Count - 1], SnakeSegments[SnakeSegments.Count - 1].transform.position, SnakeSegments[SnakeSegments.Count - 1].transform.rotation, this.transform.parent);

        SnakeSegments.Add(massGainSegment);
        
        scoreController.IncreaseScore(incrementalScore);
    }

    public void RemoveSnakeSegment()
    {
        if(SnakeSegments.Count >= 2)
        {
            GameObject massBurnSegment = SnakeSegments[SnakeSegments.Count - 1];
            SnakeSegments.Remove(SnakeSegments[SnakeSegments.Count - 1]);

            Destroy(massBurnSegment);
        }

        scoreController.DecreaseScore(incrementalScore);
    }

    private void PlayCycle()
    {
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
            if (!isShieldActivated)
            {
                Debug.Log("Game Over!");
                gameOverController.SnakeDied();
            }
        }

        if (collision.gameObject.tag == "Powerup")
        {
            collision.gameObject.GetComponent<IPowerup>().ApplyPowerup(this);
        }
    }

    public void ActivateDoubleScore()
    {
        incrementalScore = 2;

        StartCoroutine(DoubleScoreCooldownCoroutine());
    }

    public void ActivateShield()
    {
        isShieldActivated = true;
        StartCoroutine(ShieldCooldownCoroutine());
    }

    public void ActivateIncreaseSpeed()
    {
        stepDuration = stepDuration / 3;

        StartCoroutine(SpeedCooldownCoroutine());
    }

    IEnumerator DoubleScoreCooldownCoroutine()
    {
        yield return new WaitForSeconds(CoolDownTime);

        incrementalScore = 1;

    }
    IEnumerator ShieldCooldownCoroutine()
    {
        yield return new WaitForSeconds(CoolDownTime);

        isShieldActivated = false;
    }
    IEnumerator SpeedCooldownCoroutine()
    {
        yield return new WaitForSeconds(CoolDownTime);

        stepDuration = stepDuration * 3;
    }
}

public enum Direction
{
    UP,
    DOWN,
    RIGHT,
    LEFT
}

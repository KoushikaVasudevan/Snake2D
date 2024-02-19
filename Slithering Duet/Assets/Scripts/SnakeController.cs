using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    public float StepDuration = 0.1f;
    private float timeBeforeNextStep;
    public Direction snakeDirection;

    private Vector2 headPos;

    //public Transform SnakeHead;
    //public Transform [] SnakeSegments;

    private float headXPos;
    private float headYPos;

    public float topBoundary;
    public float bottomBoundary;
    public float rightBoundary;
    public float leftBoundary;


    void Start()
    {
        timeBeforeNextStep = StepDuration;

        headPos = new Vector2(transform.position.x, transform.position.y);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            snakeDirection = Direction.UP;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            snakeDirection = Direction.DOWN;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            snakeDirection = Direction.LEFT;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            snakeDirection = Direction.RIGHT;
        }
        timeBeforeNextStep -= Time.deltaTime;

        if (timeBeforeNextStep < 0)
        {
            PlayCycle();

            timeBeforeNextStep = StepDuration;
        }
    }

    private void PlayCycle()
    {
        switch (snakeDirection)
        {
            case Direction.UP:
                headPos.y += 1;
                if (headPos.y == topBoundary)
                {
                    headPos.y = bottomBoundary + 1;
                }
                break;

            case Direction.DOWN:
                headPos.y -= 1;
                if (headPos.y == bottomBoundary)
                {
                    headPos.y = topBoundary - 1;
                }
                break;

            case Direction.RIGHT:
                headPos.x += 1;
                if (headPos.x == rightBoundary)
                {
                    headPos.x = leftBoundary - 1;
                }
                break;

            case Direction.LEFT:
                headPos.x -= 1;
                if (headPos.x == leftBoundary)
                {
                    headPos.x = rightBoundary + 1;
                }
                break;
        }
        transform.position = headPos;
    }
}

public enum Direction
{
    UP,
    DOWN,
    RIGHT,
    LEFT
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareMovement : MonoBehaviour
{
    public LayerMask ignoreMe;

    public int stepOfSquare;

    public Sprite endLevelSprite;

    private bool isMoving, isMovingByPlate;
    private Vector2 startPos, targetPos, dir;
    private const float timeToMove = 10f;
    private int PlateStep;
    private new ParticleSystem particleSystem;
    private Collider2D thisCollision;


    private enum State
    {
        Swipe,
        Plate,
        None
    }

    private State state;    


    private void Start()
    {
        state = State.None;
        GlobalEventManager.MoveEvent.AddListener(Move);
        GlobalEventManager.SlideEvent.AddListener(Slide);
        GlobalEventManager.EndLevel.AddListener(EndLevel);
        thisCollision = transform.GetComponent<Collider2D>();
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    private void EndLevel()
    {
        GetComponent<SpriteRenderer>().sprite = endLevelSprite;
        GlobalEventManager.MoveEvent.RemoveListener(Move);
        GlobalEventManager.SlideEvent.RemoveListener(Slide);
    }

    private void Slide(Vector2 direction, int step, Collider2D collision)
    {
        if (collision == thisCollision)
        {
            dir = direction;
            state = State.Plate;
            PlateStep = step;
        }
        else
        {
            return;
        }
    }

    private void Move(Vector2 direction)
    {
        if(!isMoving)
        {
            dir = direction;
            state = State.Swipe;
        }
    }

    private void Update()
    {
        if (state == State.Swipe && !isMoving)
        {
            StartCoroutine(Move(stepOfSquare, state));
            state = State.None;
        }

        if(state == State.Plate && !isMoving)
        {
            StartCoroutine(Move(PlateStep, state));
            state = State.None;
        }

    }

    private IEnumerator Move(int steps, State state)
    {
        var shape = particleSystem.shape;
        if (state == State.Plate)
        {
            isMovingByPlate = true;
        }
        for (int i = 0; i < steps; i++)
        {
            if (canMove(dir))
            {
                isMoving = true;
                startPos = transform.position;
                targetPos = startPos + dir;
                float distance = Vector2.Distance(startPos, targetPos);
                float rate = timeToMove / distance;
                shape.rotation = new Vector3(0f, yParts(), 0f);
                particleSystem.Play();

                for(float t = 0; t < 1; t += rate * Time.deltaTime)
                {
                    transform.position = Vector2.Lerp(startPos, targetPos, Mathf.SmoothStep(0f, 1f, t));
                    yield return new WaitForEndOfFrame();
                }
                transform.position = targetPos;
                isMoving = false;
            }
        }
        if (state == State.Plate)
        {
            state = State.None;
            isMovingByPlate = false;
        }
    }

    private bool canMove(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1f, ~ignoreMe);
        if (hit)
        {
            if (isMovingByPlate && hit.collider.CompareTag("PassWall"))
            {
                return true;
            }                                    
            else
                return false;
        }  
        else
            return true;

    }

    private float yParts()
    {
        if(dir == Vector2.up)
            return 180f;
        else if (dir == Vector2.down)
            return 0f;
        else if (dir == Vector2.right)
            return 270f;
        else if (dir == Vector2.left)
            return 90f;
        else 
            return 0f;
    }

    private void OnDestroy()
    {
        GlobalEventManager.EndLevel.RemoveListener(EndLevel);
    }

}

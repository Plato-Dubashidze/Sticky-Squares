using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidePlate : MonoBehaviour
{
    [Range(1,3)]
    public int countOfSteps;
    public enum directionType
    {
        Up,
        Down,
        Left,
        Right
    }

    public directionType direction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            switch (direction)
            {
                case directionType.Up: GlobalEventManager.Slide(Vector2.up, countOfSteps, collision);
                    break;
                case directionType.Down: GlobalEventManager.Slide(Vector2.down, countOfSteps, collision);
                    break;
                case directionType.Left: GlobalEventManager.Slide(Vector2.left, countOfSteps, collision);
                    break;
                case directionType.Right: GlobalEventManager.Slide(Vector2.right, countOfSteps, collision);
                    break;
            }
        }
    }
}
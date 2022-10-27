using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GlobalEventManager
{
    public static UnityEvent<Vector2> MoveEvent = new UnityEvent<Vector2>();

    public static UnityEvent EndLevel = new UnityEvent();

    public static UnityEvent LoadNextLevel = new UnityEvent();

    public static UnityEvent<Vector2, int, Collider2D> SlideEvent = new UnityEvent<Vector2, int, Collider2D>();

    public static UnityEvent LoadLevelFromMenu = new UnityEvent();

    public static UnityEvent LoadMainMenu = new UnityEvent();



    public static void Move(Vector2 direction)
    {
        MoveEvent.Invoke(direction);
    }

    public static void loadNextLevel()
    {
        LoadNextLevel.Invoke();
    }

    public static void endLevel()
    {
        EndLevel.Invoke();
    }

    public static void Slide(Vector2 direction, int step, Collider2D collision)
    {
        SlideEvent.Invoke(direction, step, collision);
    }

    public static void loadLevelFromMenu()
    {
        LoadLevelFromMenu.Invoke();
    }

    public static void loadMainMenu()
    {
        LoadMainMenu.Invoke();
    }



}

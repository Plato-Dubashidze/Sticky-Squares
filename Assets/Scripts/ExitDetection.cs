using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDetection : MonoBehaviour
{
    private static int exitChecker;
    private static int ExitChecker
    {
        get 
        {
            return exitChecker;        
        }
        set 
        { 
            exitChecker = value; 
            if (ExitChecker == 2)
            {
                GlobalEventManager.endLevel();
            }
        }
    }

    private void Start()
    {
        ExitChecker = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            ExitChecker++;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            ExitChecker--;
    }

    private void OnDestroy()
    {
        ExitChecker = 0;
    }
}

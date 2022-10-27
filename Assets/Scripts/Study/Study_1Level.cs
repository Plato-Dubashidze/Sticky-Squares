using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study_1Level : MonoBehaviour
{
    public GameObject player_1, player_2, canvasFirst, canvasSecond;

    private Vector3 p1ReqPos = new Vector3(-7.5f, 2.5f, 0f);
    private Vector3 p2ReqPos = new Vector3(2.5f, 2.5f, 0f);

    private void Start()
    {
        GlobalEventManager.MoveEvent.AddListener(OnMove);
        canvasFirst.SetActive(true);
    }

    private void OnMove(Vector2 dir)
    {
        canvasFirst.SetActive(false);
        canvasSecond.SetActive(false);
    }

    private void Update()
    {
        if (player_1.transform.position == p1ReqPos && player_2.transform.position == p2ReqPos)
            canvasSecond.SetActive(true);
        else
            canvasSecond.SetActive(false);
    }
}

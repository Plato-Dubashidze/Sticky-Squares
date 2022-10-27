using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Study_2Level : MonoBehaviour
{
    public GameObject studyCanvas, player_1, player_2;

    private Vector3 p1_Pose, p2_Pose;

    private void Start()
    {
        p1_Pose = new Vector3(-5.5f, -0.5f, 0f);
        p2_Pose = new Vector3(1.5f, -0.5f, 0f);
    }

    private void Update()
    {
        if (player_1.transform.position == p1_Pose && player_2.transform.position == p2_Pose)
            studyCanvas.SetActive(true);
        else
            studyCanvas.SetActive(false);
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelParticles : MonoBehaviour
{
    private new ParticleSystem particleSystem;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        GlobalEventManager.EndLevel.AddListener(EndLevel);
    }

    private void EndLevel()
    {
        StartCoroutine(ParticlesDelay());
    }

    private IEnumerator ParticlesDelay()
    {
        while (true)
        {
            particleSystem.Play();
            yield return new WaitForSeconds(2.5f);
        }
    }

    private void OnDestroy()
    {
        GlobalEventManager.EndLevel.RemoveListener(EndLevel);
        StopCoroutine(ParticlesDelay());
    }
}

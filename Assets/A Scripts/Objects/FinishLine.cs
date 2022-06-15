using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] private ParticleSystem confetti;
    [SerializeField] private ParticleSystem confetti2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            confetti.Play();
            confetti2.Play();
        }
    }
}

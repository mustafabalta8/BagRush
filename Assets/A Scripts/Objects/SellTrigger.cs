using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellTrigger : MonoBehaviour
{
    Collider collider;

    [SerializeField] private float sellDuration = 0.3f;
    private void Start()
    {
        collider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bag"))
        {
            collider.enabled = false;
            StartCoroutine(ActivateCollider());
        }
        
    }
    IEnumerator ActivateCollider()
    {
        yield return new WaitForSeconds(sellDuration);
        collider.enabled = true;
    }
}

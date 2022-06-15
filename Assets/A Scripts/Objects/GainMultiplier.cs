using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GainMultiplier : MonoBehaviour
{
    private static bool isSendingLeftTheObject;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bag"))
        {
            MoveObjectToSides(other);
        }
    }

    private void MoveObjectToSides(Collider other)
    {
        if (isSendingLeftTheObject)
        {
            other.transform.DOMoveX(-4, 0.5f);
        }
        else
        {
            other.transform.DOMoveX(4, 0.5f);
        }
        other.transform.parent = null;
        Bag.OnInteract?.Invoke(false, other.transform);//Remove from stack
        isSendingLeftTheObject = !isSendingLeftTheObject;
        gameObject.SetActive(false);
    }
}

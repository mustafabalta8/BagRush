using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GainMultiplier : MonoBehaviour
{
    private static bool isSendingLeftTheObject=true;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bag"))
        {
            MoveObjectToSides(other);
        }
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StartHandleWinning());
        }
    }

    private IEnumerator StartHandleWinning()
    {
        yield return new WaitForSeconds(.25f);
        Player.HandleWinning();
    }

    private void MoveObjectToSides(Collider other)
    {
        print( other.gameObject.name+": "+isSendingLeftTheObject+" Time: "+Time.time);
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

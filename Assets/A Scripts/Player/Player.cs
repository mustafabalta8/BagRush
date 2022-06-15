using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Transform stackParent;

    private int collectedObjectNumber = 0;

    [SerializeField] private Vector3 localBagCollectionPosition;

    private void OnTriggerEnter(Collider other)
    {
        //print("collectedObjectNumber:" + collectedObjectNumber);
        if (other.CompareTag("Bag") && !other.GetComponent<Bag>().IsCollected)
        {
            PickUpBag(other);
        }
        if (other.tag == "Finish" && PlayerMovement.GameState == Screens.InGame)
        {
            //PlayerMovement.IsPlaying = false;
            PlayerMovement.GameState = Screens.Success;
            UIManager.instance.OpenSuccessScreen();
            StackController.instance.ClearStack();
            LevelManager.instance.UpdateLevel();
            LevelManager.instance.CreateNextLevel();

        }


    }
    private void PickUpBag(Collider other)
    {
        //e�er bag'i toplad���m�z� kontrol etmezsem ayn� bag iki defa bu triggera d���yor ve stackte bir bo�luk oluyor. tam player box collider'�n oldu�u yerde
        other.GetComponent<Bag>().IsCollected = true;
        other.transform.parent = stackParent;
        other.transform.localPosition = localBagCollectionPosition;
        collectedObjectNumber++;

        Bag.OnInteract?.Invoke(true, other.transform);

        MoneyManager.ChangeMoney(20, (other.transform.position+new Vector3(0,3,0)));
    }
}

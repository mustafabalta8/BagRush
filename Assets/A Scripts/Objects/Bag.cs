using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    public static Action<bool, Transform> OnInteract;

    [SerializeField] private GameObject form_01, form_02, form_03, form_04;
    private bool isCollected = false;
    private bool isTurnedFalse = false;
    [SerializeField] private int currentForm = 1;

    [SerializeField] private Material changedBagColor;
    [SerializeField] private Material defaultBagColor;
    public bool IsCollected { get => isCollected; set => isCollected = value; }
    public bool IsTurnedFalse { get => isTurnedFalse; set => isTurnedFalse = value; }


    [SerializeField] private Vector3 firstConvertor, secondConvertor, chainConvertor, painter, sellTrigger, obstacle;
    // 2,2,2.6 ;  0,2,-1.5 ; -2.5,3.7,-4.5  ; .4,-1,-1.5 ; 3,2.8,-.8  ; 0,2.5,-1

    private void OnEnable()
    {
        ChangeBagBaseColor(defaultBagColor);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FirstConvertor"))
        {
            DeactivateObject(currentForm);
            UpdateCurrentForm(2);
            MoneyManager.ChangeMoney(15, other.transform.position+firstConvertor);
        }
        else if (other.CompareTag("SecondConvertor"))
        {
            DeactivateObject(currentForm);
            UpdateCurrentForm(3);
            MoneyManager.ChangeMoney(15, other.transform.position+secondConvertor);
        }
        else if (other.CompareTag("ChainConvertor"))
        {
            DeactivateObject(currentForm);
            UpdateCurrentForm(4);
            MoneyManager.ChangeMoney(15, other.transform.position+chainConvertor);
        }
        else if (other.CompareTag("Painter"))
        {
            ChangeBagBaseColor(changedBagColor);
            MoneyManager.ChangeMoney(15, other.transform.position+painter);
        }
        if (other.CompareTag("SellTrigger"))
        {
            ReturnToObjectPool();
            OnInteract?.Invoke(false, transform);//RemoveFromStack; 
            MoneyManager.ChangeMoney(20, other.transform.position+sellTrigger);
            // todo: Sell process
        }
        if (other.CompareTag("Obstacle"))
        {
            ReturnToObjectPool();
            OnInteract?.Invoke(false, transform);//RemoveFromStack; 
            MoneyManager.ChangeMoney(-20, other.transform.position+obstacle);
        }
    }
    private void OnDisable()
    {
        DeactivateObject(currentForm);
        ActivateObject(1);
        IsCollected = false;
    }
    public void ReturnToObjectPool()
    {
        gameObject.SetActive(false);
    }
    private void UpdateCurrentForm(int newFormIndex)
    {
        currentForm = newFormIndex;
        ActivateObject(currentForm);
    }
    private void ChangeBagBaseColor(Material color)
    {
        form_01.GetComponent<MeshRenderer>().material = color;
        form_02.GetComponent<MeshRenderer>().material = color;
        form_03.GetComponent<MeshRenderer>().material = color;
        form_04.GetComponent<MeshRenderer>().material = color;
    }
    private void DeactivateObject(int type)
    {
        switch (type)
        {
            case 1:
                form_01.SetActive(false);
                break;
            case 2:
                form_02.SetActive(false);
                break;
            case 3:
                form_03.SetActive(false);
                break;
            case 4:
                form_04.SetActive(false);
                break;
        }
    }

    private void ActivateObject(int type)
    {
        switch (type)
        {
            case 1:
                form_01.SetActive(true);
                break;
            case 2:
                form_02.SetActive(true);
                break;
            case 3:
                form_03.SetActive(true);
                break;
            case 4:
                form_04.SetActive(true);
                break;
        }
    }
}

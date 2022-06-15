using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpMoney : MonoBehaviour
{
    [SerializeField] private Vector3 popUpMoneyGrowthRate;
    [SerializeField] private Vector3 direction;
    private void Start()
    {
        Destroy(gameObject, 1);
    }
    void Update()
    {
        transform.position += direction* Time.deltaTime;
        transform.localScale += popUpMoneyGrowthRate * Time.deltaTime;
    }
    
}

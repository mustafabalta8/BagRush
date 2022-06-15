using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateMover : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    [SerializeField] private Vector3 intervalVektor = new Vector3(0, 0, -10.13f);
    void Update()
    {
        transform.localPosition += (Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlateOrigin")
        {
            //print("trigger plate");
            transform.localPosition = transform.localPosition - (intervalVektor);
        }
    }
}

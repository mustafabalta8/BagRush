using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class test : MonoBehaviour
{

    private void Awake()
    {
        DOTween.Init();
    }

    void Start()
    {

    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            transform.DOMoveX(4, .5f);
        }
        if (Input.GetMouseButtonDown(1))
        {
            
            transform.DOMoveX(-4, .5f);
        }
    }


}

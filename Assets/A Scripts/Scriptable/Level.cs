using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LevelConfiguration")]
public class Level : ScriptableObject
{
    
    public Vector3[] collectibleBagPositions;
    public Vector3[] otherObjectsPositions;

    public GameObject interactiveObjectsSet;


}

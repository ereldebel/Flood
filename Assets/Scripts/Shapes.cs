using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


[CreateAssetMenu]
public class Shapes : ScriptableObject
{
    [SerializeField] private Vector3[] threeSapes1;

    [SerializeField] private Vector3[] threeShape2;
    
    [SerializeField] private Vector3[] threeShape3;
    
    [SerializeField] private Vector3[] threeShape4;

    private Vector3[][] _threeShapes;
    
    private void Awake()
    {
    }

    public Vector3[] GetShape()
    {
        _threeShapes = new [] {threeSapes1, threeShape2, threeShape3, threeShape4};
        return _threeShapes[Random.Range(0, 4)];
    }
}

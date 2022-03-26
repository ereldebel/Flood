using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private int type = 1;

    [SerializeField] private float speed = 20f;

    private float _originalY, _originalX;
    private void Update()
    {
        if (type == 1)
        {
            MovementOne();
        }
        if (type == 2)
        {
            MovementTwo();
        }
        if(type == 3)
        {
            MovementThree();
        }
    }

    private void Start()
    {
        transform.rotation = Quaternion.identity;
        _originalX = transform.localPosition.x;
        _originalY = transform.localPosition.y;
    }

    private void MovementOne()
    {
        transform.Rotate(new Vector3(0, speed * Time.deltaTime, 0));
    }
    
    private void MovementTwo()
    {
        transform.localPosition = new Vector3(_originalX, _originalY, (float)Math.Cos(Time.time) * speed/10);
    }
    
    private void MovementThree()
    {
        transform.rotation = Quaternion.Euler((float)Math.Cos(Time.time) * speed*5, 0, 0);
    }
}

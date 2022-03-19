using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class PosisionEnemy : MonoBehaviour
{

    public float radius;

    public GameObject enemy;

    private System.Random _random = new System.Random();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PositionEnemy(float degree)
    {
        enemy.transform.localPosition += Vector3.right * radius;
        transform.rotation = new Quaternion(0, degree, 0, 0);
        enemy.SetActive(true);
    }

    private void OnCollisionEnter(Collision other)
    {
        print("hello");
        Destroy(gameObject);
    }
}

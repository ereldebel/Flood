using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyAxis : MonoBehaviour
{

    public float radius;

    public GameObject[] enemy;

    public float dropSpeed = 1;

    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
    }

    private void FixedUpdate()
    {
        transform.position += Vector3.down * Time.deltaTime * dropSpeed;
        if (transform.position.y < -5)
        {
            PositionEnemy(Random.Range(0, 360));
        }
    }

    public void PositionEnemy(float degree)
    {
        transform.position = _startPos;
        enemy[0].transform.position += Vector3.right * radius;
        transform.rotation = Quaternion.EulerAngles(0, degree, 0);
        enemy[0].SetActive(true);
    }

    
}
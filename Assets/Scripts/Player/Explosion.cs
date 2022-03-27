using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void OnDisable()
    {
        GameManager.AddExplosion(gameObject);
    }
}

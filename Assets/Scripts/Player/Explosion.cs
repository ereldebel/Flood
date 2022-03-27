using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private static LinkedList<GameObject> explosions;


    public static void SetStack(LinkedList<GameObject> explosions)
    {
        Explosion.explosions = explosions;
    }
}

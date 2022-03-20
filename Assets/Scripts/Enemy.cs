using UnityEngine;

public class Enemy : MonoBehaviour, IHittable
{
    public void TakeHit()
    {
        GameManager.EnemyKilled();
        gameObject.SetActive(false);
    }
}

using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IHittable
    {
        public void TakeHit()
        {
            GameManager.EnemyKilled();
            gameObject.SetActive(false);
        }
    }
}

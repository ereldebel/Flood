using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IHittable
    {
        private EnemyAxis _axis;

        private void Awake()
        {
            _axis = transform.parent.parent.GetComponent<EnemyAxis>();
        }
        
        public void TakeHit()
        {
            GameManager.EnemyKilled();
            if (--_axis.RemainingEnemies > 0)
                gameObject.SetActive(false);
        }
    }
}

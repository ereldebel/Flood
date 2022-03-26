using System;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour
    {
        private EnemyAxis _axis;

        [SerializeField] private int type = 1;

        private void Awake()
        {
            _axis = transform.parent.parent.GetComponent<EnemyAxis>();
        }

        public int TakeHit(bool killedByPlayer)
        {
            if (killedByPlayer)
            {
                GameManager.EnemyKilled(type);
                AudioManager.EnemyHit();
            }
            else
                AudioManager.EnemyReachedWater();
            if (--_axis.RemainingEnemies > 0)
                gameObject.SetActive(false);
            return type;
        }
    }
}

using System;
using UnityEngine;

namespace Enemies
{
    public class Enemy : MonoBehaviour, IHittable
    {
        private EnemyAxis _axis;

        [SerializeField] private int type = 1;

        private void Awake()
        {
            _axis = transform.parent.parent.GetComponent<EnemyAxis>();
        }

        private void Start()
        {
            print("Enemy type " + type + " " + transform.position);
        }

        public void TakeHit(bool killedByPlayer)
        {
            if (killedByPlayer)
                GameManager.EnemyKilled(type);
            if (--_axis.RemainingEnemies > 0)
                gameObject.SetActive(false);
        }
    }
}

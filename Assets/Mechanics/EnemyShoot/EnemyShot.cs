using UnityEngine;

namespace Mechanics.EnemyShoot
{
    public class EnemyShot : MonoBehaviour
    {
        public Transform target;

        public Rigidbody bullet;

        public float speed = 10;
    
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }
        }

        void Shoot()
        {
            Vector3 dir = Vector3.Normalize(target.position - transform.position);
            bullet.AddForce(dir * speed, ForceMode.Impulse);
        }
    }
}

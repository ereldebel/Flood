using UnityEngine;

namespace Mechanics.EnemyGenarate
{
    public class PosisionEnemy : MonoBehaviour
    {

        public float radius;

        public GameObject enemy;

        public float dropSpeed = 1;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void FixedUpdate()
        {
            transform.position += Vector3.down * Time.deltaTime * dropSpeed;
        }

        public void PositionEnemy(float degree)
        {
            enemy.transform.position += Vector3.right * radius;
            transform.rotation = Quaternion.EulerAngles(0, degree, 0);
            enemy.SetActive(true);
        }

        private void OnCollisionEnter(Collision other)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

namespace Mechanics.EnemyShoot
{
    public class Bullet : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnCollisionEnter(Collision other)
        {
            print("col");
            transform.localPosition = Vector3.zero;
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        

        }
    }
}

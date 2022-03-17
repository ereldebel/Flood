using UnityEngine;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            collision.gameObject.SetActive(false);
            transform.position = Vector3.zero;
        }
    }
}

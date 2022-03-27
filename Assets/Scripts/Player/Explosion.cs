using UnityEngine;

namespace Player
{
    public class Explosion : MonoBehaviour
    {
        private void OnDisable()
        {
            GameManager.AddExplosion(gameObject);
        }
    }
}

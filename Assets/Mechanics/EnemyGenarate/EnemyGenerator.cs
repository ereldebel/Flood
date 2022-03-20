using UnityEngine;

namespace Mechanics.EnemyGenarate
{
    public class EnemyGenerator : MonoBehaviour
    {

        public GameObject enemy;
    
        public Vector3 startPos;
        // Start is called before the first frame update
        void Start()
        {
            startPos = enemy.transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                GenerateEnemy();
            }
        
        }

        void GenerateEnemy()
        {
            GameObject newEnemy = Instantiate(enemy);
            newEnemy.transform.position = startPos;
            newEnemy.SetActive(true);
            newEnemy.GetComponent<PosisionEnemy>().PositionEnemy(Random.Range(0f, 360f));
        }
    }
}

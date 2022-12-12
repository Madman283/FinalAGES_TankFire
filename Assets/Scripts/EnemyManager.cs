using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] Transform target_;
    [SerializeField] GameObject ePrefab;
    [SerializeField] float delaySpawn;

    float moveS;

    float moveSpeedS => 1 + moveS;

    float spawnTBefore = 0;

    float spawnTAfter => spawnTBefore + delaySpawn;

    EnemyObjectPool pool_;

    // Start is called before the first frame update
    void Start()
    {
        pool_ = gameObject.AddComponent<EnemyObjectPool>();
        pool_.enemyPrefab = ePrefab;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTAfter)
        {
            moveS += .01f;
            delaySpawn -= .01f;

            spawnTBefore = Time.time;
            Enemy currentEnemy = pool_.FromPoolTake();
            currentEnemy.targetThing = target_;
            currentEnemy.mSpeed *= moveSpeedS;

            float x = Random.Range(0f, 15f);
            float z = Random.Range(10f, 15f);

            if (Random.Range(0, 2) == 0)
            {
                x *= -1;
            }
            if (Random.Range(0, 2) == 0)
            {
                z *= -1;
            }

            currentEnemy.transform.position = new Vector3(x, 1, z);
        }

    }
}

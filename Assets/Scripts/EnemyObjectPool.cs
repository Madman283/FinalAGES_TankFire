using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EnemyObjectPool : MonoBehaviour
{
    public int poolMaxS = 50;
    public int defualtAmount = 10;
    public GameObject enemyPrefab;

    public IObjectPool<Enemy> Pool
    {
        get
        {
            if (pool_ == null)
                pool_ =
                    new ObjectPool<Enemy>(
                        PoolItemMade,
                        PoolTake,
                        PoolReturnItem,
                        PoolOBjDestroy,
                        true,
                        defualtAmount,
                        poolMaxS);
            return pool_;
        }
    }

    private IObjectPool<Enemy> pool_;

    private Enemy PoolItemMade()
    {
        var go = Instantiate(enemyPrefab);
        Enemy enemy = go.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Pool = Pool;
            return enemy;
        }
        else
        {
            return null;
        }
    }

    private void PoolReturnItem(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    private void PoolTake(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
    }

    private void PoolOBjDestroy(Enemy enemy)
    {
        Destroy(enemy.gameObject);
    }

    internal Enemy FromPoolTake()
    {
        Enemy currentEnemy = Pool.Get();
        return currentEnemy;
    }
}

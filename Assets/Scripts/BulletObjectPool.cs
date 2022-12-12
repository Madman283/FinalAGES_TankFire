using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletObjectPool : MonoBehaviour
{
    public int maxPool = 50;
    public int defualtAmount = 10;
    public GameObject bulletPrefab;

    public IObjectPool<Bullet> Pool
    {
        get
        {
            if (pool_ == null)
                pool_ =
                    new ObjectPool<Bullet>(
                        PoolItemMade,
                        TakeFromPool,
                        PoolOnReturn,
                        PoolItemDestroy,
                        true,
                        defualtAmount,
                        maxPool);
            return pool_;
        }
    }

    private IObjectPool<Bullet> pool_;

    private Bullet PoolItemMade()
    {
        var go = Instantiate(bulletPrefab);
        Bullet bullet = go.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.Pool = Pool;
            return bullet;
        }
        else
        {
            return null;
        }
    }

    private void PoolOnReturn(Bullet bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void TakeFromPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
    }

    private void PoolItemDestroy(Bullet bullet)
    {
        Destroy(bullet.gameObject);
    }

    internal Bullet PoolTake()
    {
        Bullet currentBullet = Pool.Get();
        return currentBullet;
    }
}

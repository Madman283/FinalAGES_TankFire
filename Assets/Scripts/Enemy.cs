using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float mSpeed;
    public Transform targetThing;
    public IObjectPool<Enemy> Pool { get; set; }
    bool targetFound = true;
    Vector3 velocity;
    Vector3 acceleration;
    private void OnEnable()
    {
        mSpeed = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Control();
    }

    void Control()
    {
        var direction = targetThing.position - transform.position;
        var control = direction.normalized - velocity.normalized;

        velocity += control;
        transform.position += Time.deltaTime * mSpeed * velocity;
    }

    void BackToPool()
    {
        Pool.Release(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.SetActive(false);
            targetFound = false;
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            BackToPool();
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [SerializeField] float despawnDelay;

    Rigidbody rb_;

    public IObjectPool<Bullet> Pool { get; set; }

    bool TimerR;
    float SpawnL;

    float TimeS => SpawnL + despawnDelay;

    private void OnEnable()
    {
        rb_ = gameObject.GetComponent<Rigidbody>();
        SpawnL = Time.time;
        TimerR = true;
    }

    private void OnDisable()
    {
        rb_.velocity = Vector3.zero;
        TimerR = false;
    }

    void ReturnPool()
    {
        Pool.Release(this);
    }

    private void Update()
    {
        if (transform.position.y < 0)
        {
            ReturnPool();
        }

        if (!TimerR) return;

        if (Time.time > TimeS)
        {
            ReturnPool();
        }
    }

    public void GoContinue(float force, Vector3 direction)
    {
        rb_.AddForce(force * direction, ForceMode.Impulse);
    }
}

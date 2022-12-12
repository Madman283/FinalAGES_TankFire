using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] GameObject BulletPrefab;
    [SerializeField] float mForce;
    [SerializeField] float mSpeed;
    [SerializeField] float rSpeed;
    [SerializeField] float delayF;
    [SerializeField] float offY;

    BulletObjectPool pool_;
    InputActionScript actionDone;

    float moveX;
    float moveY;

    float TimeLS;

    float nextShotTime => TimeLS + delayF;

    bool holdTF;

    Vector3 startOS => transform.position + transform.up + transform.forward;

    private void Awake()
    {
        actionDone = new InputActionScript();
    }

    void Start()
    {
        pool_ = gameObject.AddComponent<BulletObjectPool>();
        pool_.bulletPrefab = BulletPrefab;
    }

    private void OnEnable()
    {
        actionDone.Player.Enable();
        actionDone.Player.Move.performed += MoveHandle;
        actionDone.Player.Move.canceled += MoveHandle;
        actionDone.Player.Fire.performed += WhenFire;
        actionDone.Player.Fire.canceled += WhenFire;
    }

    private void OnDisable()
    {
        actionDone.Player.Move.performed -= MoveHandle;
        actionDone.Player.Move.canceled -= MoveHandle;
        actionDone.Player.Fire.performed -= WhenFire;
        actionDone.Player.Fire.canceled -= WhenFire;
        actionDone.Player.Disable();
    }

    private void Update()
    {
        if (holdTF && Time.time > nextShotTime)
        {
            Fire();
        }

        if (moveX == 0 && moveY == 0) return;
        transform.Translate(moveY * mSpeed * Time.deltaTime * Vector3.forward);
        transform.Rotate(Vector3.up, moveX * rSpeed * Time.deltaTime);
    }

    void MoveHandle(InputAction.CallbackContext context)
    {
        Vector2 current = context.ReadValue<Vector2>();
        moveX = current.x;
        moveY = current.y;
    }

    void WhenFire(InputAction.CallbackContext context)
    {
        holdTF = context.performed;
    }

    void Fire()
    {
        Bullet currentBullet = pool_.PoolTake();
        currentBullet.transform.position = startOS;

        currentBullet.GoContinue(mForce, transform.forward);
        TimeLS = Time.time;
    }
    
}

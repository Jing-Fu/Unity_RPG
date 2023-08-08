using System.Dynamic;
using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1;
    [SerializeField] bool isHooming = true;
    [SerializeField] GameObject hitEffect = null;
    Health target = null;
    float damage = 0;

    void Start()
    {
        transform.LookAt(GetAimLocation());
    }
    void Update()
    {
        if (target == null) return;

        if (isHooming && !target.IsDead)
        {
            transform.LookAt(GetAimLocation());
        }
        transform.Translate(speed * Time.deltaTime * Vector3.forward);
    }

    public void SetTarget(Health target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private Vector3 GetAimLocation()
    {
        CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();
        if (targetCapsule == null) return target.transform.position;
        return target.transform.position + Vector3.up * targetCapsule.height / 2;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Health>() != target)
        {
            return;
        }
        if (target.IsDead) return;
        target.TakeDamage(damage);
        if (hitEffect != null)
        {
            Instantiate(hitEffect, GetAimLocation(), transform.rotation);
        }
        Destroy(gameObject);
    }

}

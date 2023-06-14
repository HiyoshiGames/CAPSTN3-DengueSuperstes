using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dagger : Projectile
{
    public float speed;
    public float damage;

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out EnemyStat enemyStat))
        {
            enemyStat.TakeDamage(damage);
            gameObject.SetActive(false);
        }
    }
}

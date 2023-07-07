using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class Projectile : MonoBehaviour
{
    [Header(DS_Constants.ASSIGNABLE)]
    [SerializeField] protected bool isDestruct;
    public float projectileSpeed;
    protected Vector2 projectileSize;
    protected float projectileDamage;
    protected float damageTimeTick;
    [SerializeField] protected SO_Pool poolSO; 
    protected float destructTimer;

    protected virtual void OnEnable()
    {
        UpdateProjectileStats();
        StartCoroutine((SelfDestructTimer()));
    }

    protected virtual void OnDisable()
    {
        UpdateProjectileStats();
    }

    protected virtual void Start()
    {
        SingletonManager.Get<GameManager>().onUpdateUpgradesEvent.AddListener(UpdateProjectileStats);
    }

    protected virtual IEnumerator SelfDestructTimer()
    {
        yield return new WaitForSeconds(destructTimer);
        //Debug.Log("Self Destruct Projectile.");
        gameObject.SetActive(false);
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col);
        if (col.gameObject.TryGetComponent(out EnemyStat enemyStat))
        {
            enemyStat.TakeDamage(projectileDamage, 0);
            // Debug.Log("Enemy took " + projectileDamage + " damage.");
        }
    }
    
    protected virtual void UpdateProjectileStats()
    {
        foreach (WeaponStat ws in SingletonManager.Get<WeaponsManager>().weapons)
        {
            if (ws.weaponLevelSO.name.Equals(poolSO.name))
            {
                transform.localScale.Set(ws.size.x, ws.size.y, ws.size.z);
                if (TryGetComponent(out CircleCollider2D cc2D))
                {
                    cc2D.radius = ws.radius;
                }
                else if (TryGetComponent(out BoxCollider2D bc2D))
                {
                    bc2D.size = ws.boxSize;
                }
                projectileSpeed = ws.speed;
                projectileDamage = ws.damage;
                damageTimeTick = ws.damageTick;
                destructTimer = ws.destructTimer;
            }
        }
        foreach (PowerupStat ps in SingletonManager.Get<PowerupsManager>().powerups)
        {
            if (ps.powerupName.Equals("Radius"))
            {
                if (TryGetComponent(out CircleCollider2D cc2D))
                {
                    cc2D.radius += ps.increase;
                }
            }
            if (ps.powerupName.Equals("Damage"))
            {
                projectileDamage += ps.increase;
            }
            if (ps.powerupName.Equals("Speed"))
            {
                projectileSpeed += ps.increase;
            }
            if (ps.powerupName.Equals("Time"))
            {
                destructTimer += ps.increase;
            }
        }
    }
}

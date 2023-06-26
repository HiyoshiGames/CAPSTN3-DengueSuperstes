using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosMagnet : Projectile
{
    [Header(DS_Constants.ASSIGNABLE)] 
    [SerializeField] private bool isDestruct;
    [SerializeField] private float tickTime;

    protected override void OnEnable()
    {
        if (isDestruct)
        {
            base.OnEnable();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.TryGetComponent(out EnemyStat enemyStat))
        {
            enemyStat.StartDoT(projectileDamage, 0, tickTime);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyStat enemyStat))
        {
            enemyStat.StopDoT();
        }
    }

    protected override IEnumerator SelfDestructTimer()
    {
        yield return new WaitForSeconds(selfDestructTimer);
        SingletonManager.Get<GameManager>().onChangeTargetEvent.Invoke(null);
        transform.parent.gameObject.SetActive(false);
    }
}
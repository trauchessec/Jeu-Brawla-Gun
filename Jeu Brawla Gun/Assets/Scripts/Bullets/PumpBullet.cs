using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PumpBullet : Bullet
{
    public Bullet subBulletPrefab;
    public float nbSubBullet;

    protected override void Start()
    {
        Invoke("Hit", lifetime);
    }

    private void Hit()
    {
        Hit(null);
    }

    protected override void Hit(PlayerController p)
    {
        for (int i = 0; i < nbSubBullet; i++)
        {
            Vector3 v = new Vector3(0, 0, Random.value * 360);
            Instantiate(subBulletPrefab, transform.position, Quaternion.Euler(v));
        }

        if (p != null)
        {
            p.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}

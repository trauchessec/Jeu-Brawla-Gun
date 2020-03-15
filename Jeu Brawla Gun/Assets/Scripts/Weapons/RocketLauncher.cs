using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    public Bullet rocketPrefab;
    public float bulletUpForce = 70f;

    protected override Bullet SpawnBullet(Bullet b = null)
    {
        Bullet bulletToSpawn = b ?? bulletPrefab;
        Bullet go = Instantiate(bulletToSpawn, anchor.position, transform.parent.parent.rotation);
        go.Init(p);

        var rb = go.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.up * bulletUpForce, ForceMode2D.Force);

        return go;
    }

    public override void Interact()
    {
        Vector3 dir = new Vector3(0, 0, Random.value * 360);

        Bullet go = SpawnBullet(Quaternion.Euler(dir), rocketPrefab);
        go.transform.position = GameManager.singleton.centerOfMap;

        var rb = go.GetComponent<Rigidbody2D>();

        Destroy(gameObject);
    }
}

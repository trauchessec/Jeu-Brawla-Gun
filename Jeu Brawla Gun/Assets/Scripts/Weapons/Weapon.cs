using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float shotDelay;
    public Bullet bulletPrefab;
    public Transform anchor;

    public Sprite image;
    public string weaponName;

    [SerializeField]
    float maxBullet;
    public float Bullet { get { return bullet; } }
    public float MaxBullet { get { return maxBullet; } }

    float bullet, lastShot = 0;
    public float ReloadTime { get { return 1 - (lastShot + shotDelay - Time.time / 1); } }

    protected PlayerController p;

    public bool IsReady { get { return lastShot + shotDelay <= Time.time; } }

    private void Start()
    {
        bullet = maxBullet;
        lastShot = -shotDelay;
    }

    public virtual void Shoot()
    {
        if (lastShot + shotDelay <= Time.time && bullet > 0)
        {
            bullet--;
            lastShot = Time.time;
            SpawnBullet();
        }
    }

    #region Spawn Bullet
    protected virtual Bullet SpawnBullet(Bullet b = null)
    {
        Bullet bulletToSpawn = b ?? bulletPrefab;
        Bullet go = Instantiate(bulletToSpawn, anchor.position, transform.parent.parent.rotation);
        go.Init(p);

        return go;
    }

    protected virtual Bullet SpawnBullet(Quaternion rot, Bullet bullet)
    {
        Bullet b = Instantiate(bullet, anchor.position, rot);
        b.Init(p);

        return b;
    }
    #endregion

    public virtual Weapon Grab(PlayerController player)
    {
        p = player;

        GetComponent<Collider2D>().enabled = false;

        transform.SetParent(player.hand);
        transform.position = player.hand.transform.position;

        Vector2 weaponEuleur = transform.eulerAngles;
        weaponEuleur.y = player.hand.parent.eulerAngles.y;
        transform.eulerAngles = weaponEuleur;

        transform.GetComponentInChildren<SpriteRenderer>().enabled = false;

        FallingWeapon fw = GetComponent<FallingWeapon>();
        if (fw != null)
        {
            fw.Destroy();
        }

        return this;
    }

    public void Throw()
    {
        transform.parent = null;
        Destroy(gameObject);
        return;
        var collider = GetComponent<Collider2D>();
        collider.enabled = true;
        collider.isTrigger = false;

        Rigidbody2D rb = (Rigidbody2D)gameObject.AddComponent(typeof(Rigidbody2D));

        Vector2 dir = p.transform.right;
        rb.AddForce(dir * p.throwForce);

        transform.parent = null;
    }

    public abstract void Interact();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var pCol = collision.gameObject.GetComponent<PlayerController>();

       if (pCol == null || pCol != p)
       {
            //Interact();
       }
    }
}

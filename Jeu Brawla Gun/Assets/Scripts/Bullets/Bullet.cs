using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage, speed, lifetime;

    protected PlayerController p;

    public virtual void Init(PlayerController p)
    {
        this.p = p;
    }

    protected virtual void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    protected virtual void Hit(PlayerController p)
    {
        p.TakeDamage(damage);
        Destroy(gameObject);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            var pHit = collision.GetComponent<PlayerController>();
            if (pHit != p)
            {
                Hit(pHit);
            }
        }

        if (collision.tag == "Wall")
        {
            Destroy(gameObject);    
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingWeapon : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = (Rigidbody2D)gameObject.AddComponent(typeof(Rigidbody2D));
        rb.gravityScale = 0.2f;
        rb.angularDrag = 3f;
        rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        rb.freezeRotation = true;
    }

    public void Destroy()
    {
        Destroy(rb);
        Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "Weapon")
        {
            animator.Play("Spawn");

            Destroy();
        }
    }
}

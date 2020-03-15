using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public int id;
    PlayerInput input;
    public Transform hand;
    Weapon weapon;
    Animator animator;

    public Collider2D fullBody, crouchBody;

    public float speed = 50f, jumpForce = 900f, jumpDelay = 1f, throwForce = 1100f;
    private float lastJump, life, grabDistance = 2f;
    public float Life { get { return life; } }
    private const float maxLife = 100;

    [HideInInspector]
    public int resuLeft = 3;
    private bool isReviving = false;
    private bool hasResuProtection;
    public bool HasResuProtection { set
        {
            hasResuProtection = value;
            var sr = GetComponent<SpriteRenderer>();
            Color c = sr.color;
            c.a = value ? 0.5f : 1;
            sr.color = c;
        }
    }

    private bool readyToShoot = true;

    public float LifePercentage { get { return life / maxLife; } }
    public Weapon Weapon { get { return weapon; } }

    bool isGrounded;
    Rigidbody2D rb;

    void Start()
    {
        life = maxLife;

        input = new PlayerInput(id);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Move();

        ManageWeapon();

        if (Input.GetKeyDown(KeyCode.R))
        {
            TakeDamage(34);
        }
    }

    void ManageWeapon()
    {
        if (input.GetInputDown(input.interact))
            Interact();

        if (weapon == null)
            return;

        if (input.GetInput(input.fire) > 0 && readyToShoot && weapon.IsReady && isGrounded)
        {
            StartCoroutine(Shoot());
        }
    }

    void Move()
    {
        if (isReviving)
            return;

        Vector2 dir = Vector2.zero;

        dir.x = input.GetInput(input.horizontal);
        dir.y = input.GetInput(input.vertical);

        Vector3 euler = transform.eulerAngles;
        euler.y = dir.x > 0 ? 0 : dir.x < 0 ? 180 : euler.y;
        transform.eulerAngles = euler;

        if (isGrounded && dir.y > 0 && lastJump <= Time.time)
        {
            animator.SetTrigger("Jump");

            lastJump = Time.time + jumpDelay;
            Jump();
        }

        animator.SetBool("Falling", !isGrounded);
        animator.SetBool("Run", dir.x != 0);

        dir.x = euler.y == 0 ? dir.x : -dir.x;

        SetPlayerCollider(dir.y < 0);

        dir.y = 0;
        transform.Translate(dir * speed * Time.deltaTime);
    }

    void SetPlayerCollider(bool isCrouch)
    {
        fullBody.enabled = !isCrouch;
        crouchBody.enabled = isCrouch;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce);
    }

    void Interact()
    {
        if (weapon == null)
        {
            // grab weapon
            var collisions = Physics2D.OverlapCircleAll(transform.position, grabDistance);
            
            foreach (Collider2D c in collisions)
            {
                Weapon w = c.GetComponent<Weapon>();
                if (w != null)
                {
                    weapon = w.Grab(this);
                    animator.SetBool("HasWeapon", true);

                    break;
                }
            }
        }
        else if (readyToShoot)
        {
            // throw weapon
            weapon.Throw();
            animator.SetBool("HasWeapon", false);

            weapon = null;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!hasResuProtection)
            life -= damage;

        if (life <= 0 && !isReviving)
        {
            if (weapon != null) {
                weapon.Throw();
                animator.SetBool("HasWeapon", false);
                weapon = null;
            }

            animator.SetTrigger("Death");
            StartCoroutine(DeathAnimation());
        }
        else if (!hasResuProtection)
        {
            animator.SetTrigger("Hurt");
        }
    }

    IEnumerator DeathAnimation()
    {
        isReviving = true;
        animator.SetBool("Dead", true);
        
        yield return new WaitForSeconds(2f);

        animator.SetBool("Dead", false);

        resuLeft--;
        PlayerManager.Resurection(this);
        life = maxLife;
        isReviving = false;
    }

    IEnumerator Shoot()
    {
        readyToShoot = false;

        if (weapon.Bullet > 0)
        {
            animator.SetTrigger("Shoot " + weapon.weaponName);
        }
        else
        {
            readyToShoot = true;
        }

        yield return new WaitUntil(() => weapon == null || animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot " + weapon.weaponName));

        yield return new WaitForSeconds(0.08f);

        readyToShoot = true;
        if (weapon != null)
            weapon.Shoot();
    }

    #region collision
    private void OnCollisionStay2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
    #endregion
}

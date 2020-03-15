using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBullet : Bullet
{
    public float rotateSpeed = 0.125f;

    private PlayerController target;

    protected override void Start()
    {
        target = p == PlayerManager.singleton.player1 ? PlayerManager.singleton.player2 : PlayerManager.singleton.player1;
    }

    private void Update()
    {
        transform.right = Vector3.Lerp(transform.right, target.transform.position - transform.position, rotateSpeed);

        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}

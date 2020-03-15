using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerController player1;
    public PlayerController player2;

    public static PlayerManager singleton;

    public float resuTime = 3f;
    public float deathTime = 4f;

    private void Start()
    {
        if (singleton == null)
            singleton = this;
    }

    public static void Resurection(PlayerController p)
    {
        if (p.resuLeft > 0)
        {
            singleton.StartCoroutine(singleton.GiveResuProtect(p));
        }
        else
        {
            PlayerController pWinner = p == singleton.player1 ? singleton.player2 : singleton.player1;
            GameManager.Win(pWinner);
        }
    }

    private IEnumerator GiveResuProtect(PlayerController p)
    {
        p.gameObject.SetActive(false);
        yield return new WaitForSeconds(deathTime);
        p.gameObject.SetActive(true);

        p.transform.position = GameManager.singleton.resuPos;

        p.HasResuProtection = true;
        yield return new WaitForSeconds(resuTime);
        p.HasResuProtection = false;
    }
}

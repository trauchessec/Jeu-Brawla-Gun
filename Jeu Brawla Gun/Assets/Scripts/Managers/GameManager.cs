using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Vector3 centerOfMap;
    public Vector3 resuPos;

    public GameObject victorySceen;
    public Text playerWin;

    public static GameManager singleton;

    private void Start()
    {
        if (singleton == null)
            singleton = this;
    }

    public static void Win(PlayerController p)
    {
        singleton.victorySceen.SetActive(true);
        singleton.playerWin.text = "Player " + p.id.ToString() + " a gagné!";
    }
}

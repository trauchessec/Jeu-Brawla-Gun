using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput
{
    public string horizontal, vertical, fire, interact;

    public PlayerInput(int pId)
    {
        horizontal = "Horizontal Player " + pId; 
        vertical = "Vertical Player " + pId; 
        fire = "Fire Player " + pId; 
        interact = "Interact Player " + pId;
    }

    public float GetInput(string inputName)
    {
        return Input.GetAxis(inputName);
    }

    public bool GetInputDown(string inputName)
    {
        return Input.GetButtonDown(inputName);
    }
}

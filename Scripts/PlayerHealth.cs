using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerHealth : MonoBehaviour
{
    public Text HealthTex;

    public float PlayerHealthValue;
    public float PlayerHealthLeft;

    
    void Start()
    {
        PlayerHealthLeft = PlayerHealthValue;
    }

   
    public void PlayerHealthFunction(float damage)
    {
        PlayerHealthLeft -= damage;
        HealthTex.text = " " + PlayerHealthLeft;
    }
}

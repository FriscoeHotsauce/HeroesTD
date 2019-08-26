using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseHealthBar : MonoBehaviour
{
    public int baseHealth;

    //return true if the attack dealt fatal damage
    public bool dealDamage(int damage){
        baseHealth = baseHealth - damage;
        if(baseHealth <= 0){
            Debug.Log("You loose!");
            return true;
        }
        return false;
    }
}

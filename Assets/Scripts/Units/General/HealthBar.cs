using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public int maxHealth;   
    public int currentHealth;
    public Image healthBar;

    void Start()
    {
        maxHealth = GetComponent<Stats>().getHealth();
        currentHealth = maxHealth;
        
    }

    //return true if the attack dealt fatal damage
    public bool dealDamage(int damage){
        currentHealth = currentHealth - damage;
        Debug.Log(currentHealth / maxHealth);
        healthBar.fillAmount = ((float)currentHealth / (float)maxHealth); //why do you make me cast things C#, why do you hurt me so
        if(currentHealth <= 0){
            GetComponent<Die>()?.killUnit();
            return true;
        }
        return false;
    }
}

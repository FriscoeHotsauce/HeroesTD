using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public int maxHealth;   
    public int currentHealth;
    public GameObject healthBarCanvas;
    public Image healthBar;
    private bool isHealthBarActive;

    void Start()
    {
        maxHealth = GetComponent<Stats>().getHealth();
        currentHealth = maxHealth;
        healthBarCanvas.SetActive(false);
        isHealthBarActive = false;
    }

    //return true if the attack dealt fatal damage
    public bool dealDamage(int damage){
        showHealthBar();
        currentHealth = currentHealth - damage;
        healthBar.fillAmount = ((float)currentHealth / (float)maxHealth); //why do you make me cast things C#, why do you hurt me so
        if(currentHealth <= 0){
            GetComponent<Die>()?.killUnit();
            return true;
        }
        return false;
    }

    //wrap this in a boolean so we don't make needless calls on healthBarCanvas.SetActive(false);
    private void showHealthBar(){
        if(!isHealthBarActive){
            healthBarCanvas.SetActive(true);
            isHealthBarActive = true;
        }
    }
}

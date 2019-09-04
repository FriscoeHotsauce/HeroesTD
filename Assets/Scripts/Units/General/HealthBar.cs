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
        renderHealthDisplay();
        if(currentHealth <= 0){
            GetComponent<Die>()?.killUnit();
            return true;
        }
        return false;
    }

    public int getMaxHealth(){
        return maxHealth;
    }

    public int getMaxHealth(int overheal){
        return maxHealth + overheal;
    }

    public bool isMaxHealth(int overheal){
        return (maxHealth + overheal) == (currentHealth+overheal);
    }

    public int getCurrentHealth(){
        return currentHealth;
    }

    //call this after making adjustments to current health
    public void renderHealthDisplay(){
        healthBar.fillAmount = ((float)currentHealth / (float)maxHealth); //why do you make me cast things C#, why do you hurt me so
    }

    public void heal(int magic, int overheal){
        int health = magic/4;
        currentHealth = (health + currentHealth) > getMaxHealth(overheal) ? getMaxHealth(overheal) : health + currentHealth;
        renderHealthDisplay();
    }

    //wrap this in a boolean so we don't make needless calls on healthBarCanvas.SetActive(false);
    private void showHealthBar(){
        if(!isHealthBarActive){
            healthBarCanvas.SetActive(true);
            isHealthBarActive = true;
        }
    }
}

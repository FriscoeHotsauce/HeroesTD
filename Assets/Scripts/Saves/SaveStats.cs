using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
    a data represenation of HeroStats, including a key that can be used to locate prefabs to load these stats onto
 */
[System.Serializable]
public class SaveStats 
{
    public string prefabKey;
    public int health;
    public int attack;
    public int magic;
    public int defense;
    public int resistance;
    public float agility;
    public int experience;
    public int block;
    public float range;
    public int cost;
    public int level;
    public int currentXp;
    public int nextLevelXp;

    //b o i l e r p l a t e
    public SaveStats(string prefabKey, int health, int attack, int magic, int defense,
        int resistance, int agility, int experience, int block, int range, int cost,
        int level, int currentXp, int nextLevelXp){
            this.prefabKey = prefabKey;
            this.health = health;
            this.attack = attack;
            this.magic = magic;
            this.defense = defense;
            this.resistance = resistance;
            this.agility = agility;
            this.experience = experience;
            this.block = block;
            this.range = range;
            this.cost = cost;
            this.level = level;
            this.currentXp = currentXp;
            this.nextLevelXp = nextLevelXp;
    }
    

}

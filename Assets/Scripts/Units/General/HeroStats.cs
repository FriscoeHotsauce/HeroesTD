using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    @param block - the number of units this hero can block (melee only)
    @param range - the range at which this unit can attack and block units
    @param cost - The number of requisition points needed to place this unit
 */
public class HeroStats : Stats
{
    public int Block;
    public float Range;
    public int Cost;
    public Utils.UnitType UnitType;
    public int Level;
    public int currentXp;
    public int nextLevelXp;

    public float getRange(){
        return Range;
    }

    public void addRange(float range){
        Range = Range + range;
    }
    
    public int getBlock(){
        return Block;
    }

    public void addBlock(int block){
        Block = Block + block;
    }

    public int getCost(){
        return Cost;
    }

    public void addCost(int cost){
        Cost = Cost + cost;
    }

    public Utils.UnitType getUnitType(){
        return UnitType; 
    }

    public void addExperience(int xp){
       currentXp = currentXp + xp;
       if(currentXp > nextLevelXp){
           levelUp();
       }
   }

   private void levelUp(){
       Debug.Log(transform.name + " Leveled Up!");
        //adjust stats
        addHealth((int) Math.Round(Health * .10));
        addAttack((int) Math.Round(Attack * .10));
        addMagic((int) Math.Round(Magic * .10));
        addDefense((int) Math.Round(Defense * .10));
        addResistance((int) Math.Round(Resistance * .10));
        addAgility((float) Math.Round(Agility * .10, 2));
        addRange((float) Math.Round(Range * .05, 2));
        addCost(1);

        //play animation
        gameObject.GetComponentInChildren<HealthBar>().playLevelUpAnimation();

        //increment level and set next XP threshold
        Level = Level + 1;
        nextLevelXp = nextLevelXp + 100 + (Level * 10);//todo create some kind of MATH to calculate next level XP
   }

    //b o i l e r p l a t e
    public void loadStats(SaveStats saveStats){
        this.Health = saveStats.health;
        this.Attack = saveStats.attack;
        this.Magic = saveStats.magic;
        this.Defense = saveStats.defense;
        this.Resistance = saveStats.resistance;
        this.Agility = saveStats.agility;
        this.Experience = saveStats.experience;
        this.Block = saveStats.block;
        this.Range = saveStats.range;
        this.Cost = saveStats.cost;
        this.Level = saveStats.level;
        this.currentXp = saveStats.currentXp;
        this.nextLevelXp = saveStats.nextLevelXp;
   }
}

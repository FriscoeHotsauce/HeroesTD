using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    This clas provides the base stats of the unit, and it's properties are READ ONLY once in game.

    @property Health
    @property Attack
    @property Defense
    @property Resistance
    @property Agility
    @property Movement
 */
public abstract class Stats : MonoBehaviour
{
   public int Health;
   public int Attack;
   public int Magic;
   public int Defense;
   public int Resistance;
   public float Agility;

   public int getHealth(){
       return Health;
   }
   public int getAttack(){
       return Attack;
   }
   public int getMagic(){
       return Magic;
   }
   public int getDefense(){
       return Defense;
   }
   public int getResistance(){
       return Resistance;
   }

   public float getAgility(){
       return Agility;
   }
}

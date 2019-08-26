using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    This clas provides the base stats of the unit, and it's properties are READ ONLY once in game.

    @property Health
    @property Attack
    @property Defense
    @property Resistance
    @property Block
    @property Range
    @property Agility
    @property Movement
 */
public class Stats : MonoBehaviour
{
   public int Health;
   public int Attack;
   public int Defense;
   public int Resistance;
   public int Block;
   public float Range;
   public float Agility;
   public float Movement;

   public int getHealth(){
       return Health;
   }
   public int getAttack(){
       return Attack;
   }
   public int getDefense(){
       return Defense;
   }
   public int getResistance(){
       return Resistance;
   }
   public int getBlock(){
       return Block;
   }
   public float getRange(){
       return Range;
   }
   public float getAgility(){
       return Agility;
   }
   public float getMovement(){
       return Movement;
   }
}

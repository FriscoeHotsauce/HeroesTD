using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
    This class provides the base stats of the unit

    @property Health
    @property Attack
    @property Defense
    @property Resistance
    @property Agility
    @property Movement
    @Property Experience
 */
public abstract class Stats : MonoBehaviour
{
  public int Health;
  public int Attack;
  public int Magic;
  public int Defense;
  public int Resistance;
  public float Agility;
  public int Experience;
  public Utils.UnitType UnitType;


  public int getHealth()
  {
    return Health;
  }

  public void addHealth(int health)
  {
    Health = Health + health;
  }

  public int getAttack()
  {
    return Attack;
  }

  public void addAttack(int attack)
  {
    Attack = Attack + attack;
  }

  public void subtractAttack(int attack)
  {
    Attack = Attack - attack;
  }

  public int getMagic()
  {
    return Magic;
  }

  public void addMagic(int magic)
  {
    Magic = Magic + magic;
  }

  public int getDefense()
  {
    return Defense;
  }

  public void addDefense(int defense)
  {
    Defense = Defense + defense;
  }

  public int getResistance()
  {
    return Resistance;
  }

  public void addResistance(int resistance)
  {
    Resistance = Resistance + resistance;
  }

  public float getAgility()
  {
    return Agility;
  }

  public void addAgility(float agility)
  {
    Agility = Agility + agility;
  }

  public void subtractAgility(float agility)
  {
    Agility = Agility - agility;
  }

  public int getExperience()
  {
    return Experience;
  }

  public Utils.UnitType getUnitType()
  {
    return UnitType;
  }
}

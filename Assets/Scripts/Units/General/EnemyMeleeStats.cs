using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeStats : Stats
{
    public float movement;

    public float getMovement(){
        return movement;
    }

    public Utils.UnitType GetUnitType(){
        return Utils.UnitType.Melee;
    }
}

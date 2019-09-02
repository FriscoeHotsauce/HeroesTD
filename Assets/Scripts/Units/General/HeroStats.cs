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
    public int block;
    public float range;
    public int cost;
    public Utils.UnitType unitType;

    public float getRange(){
        return range;
    }
    
    public int getBlock(){
        return block;
    }

    public int getCost(){
        return cost;
    }

    public Utils.UnitType getUnitType(){
        return unitType; 
    }
}

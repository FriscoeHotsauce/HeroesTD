using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    //a higher agility should mean a faster attack rate, but we want the number *increasing* because it feels better. 
    //An agility of 50 = 1 attack per second. Maybe try and find a better formula for this.
    public static float calculateAttackRate(float agility){
        return (50 / agility);
    }
}

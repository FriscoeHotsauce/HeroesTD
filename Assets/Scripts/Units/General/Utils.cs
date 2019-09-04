using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
   public enum UnitType { Melee, Ranged }
    //a higher agility should mean a faster attack rate, but we want the number *increasing* because it feels better. 
    //An agility of 50 = 1 attack per second. Maybe try and find a better formula for this.
    public static float calculateAttackRate(UnitType unitType, float agility){
        int modifier = unitType == UnitType.Melee ? 50 : 40; //50 for melee, 40 for ranged
        return (modifier / agility);
    }

    public static int calculateDamageDealt(Stats defenderStats, int attackerAtk, int attackerMag){
        int magicDamageTaken = (attackerMag - defenderStats.getResistance()) > 0 ? attackerMag - defenderStats.getResistance() : 0;
        int physicalDamageTaken = (attackerAtk - defenderStats.getDefense()) > 0 ? attackerAtk - defenderStats.getDefense() : 0;
        return magicDamageTaken + physicalDamageTaken;
    }

    public static List<GameObject> getEnemiesInRange(Vector3 heroPosition, float range){
        return getTaggedObjectsInRange(heroPosition, range, "Enemy");
    }

    public static List<GameObject> getAlliesInRange(Vector3 heroPosition, float range){
        return getTaggedObjectsInRange(heroPosition, range, "Hero");
    }

    private static List<GameObject> getTaggedObjectsInRange(Vector3 position, float range, string tag){
        List<GameObject> objectsInRange = new List<GameObject>();
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag(tag);
        foreach(GameObject gameObject in allObjects){
            if(Vector3.Distance(gameObject.transform.position, position) < range){
                objectsInRange.Add(gameObject);
            }
        }
        return objectsInRange;
    }
}

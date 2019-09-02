using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hardpoint : MonoBehaviour
{
   public bool hasUnit = false;
   public Utils.UnitType type;

   public bool getHasUnit(){
       return hasUnit;
   }

   public void placeUnit(){
       hasUnit = true;
   }

   public void removeUnit(){
       hasUnit = false;
   }

   public Utils.UnitType getHardpointType(){
       return type;
   }
}

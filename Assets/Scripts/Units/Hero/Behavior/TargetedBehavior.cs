using System.Collections;
using System.Collections.Generic;

using UnityEngine;


/*
  11/7/2019 Don't necessarily need this, but it might be nice to have semantic destinctions between different 
  types of behaviors, kinda depends how I want to organize that in the future
 */
interface TimedBehavior : Behavior
{
  void resetCooldown();
}

interface PassiveBehavior : Behavior
{
}

interface Behavior
{

  bool shouldExecute();
  /*
    Perform the action
   */
  void executeBehavior();
}
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

interface TargetedBehavior
{
  /*
    Acquire the target
  */
  bool acquireTarget();
}

interface Behavior
{

  bool shouldExecute();
  /*
    Perform the action
   */
  void executeBehavior();
}
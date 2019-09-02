using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeHardpoint : MonoBehaviour
{
    public Hardpoint hardpoint;

    void OnDestroy(){
        hardpoint.removeUnit();
    }

    public void setHardpoint(Hardpoint hardpoint){
        this.hardpoint = hardpoint;
    }
}

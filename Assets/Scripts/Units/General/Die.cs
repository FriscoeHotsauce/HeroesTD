using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public string deathAnimationName;
    private Animator animator;

    void Start(){
        animator = GetComponent<Animator>();
    }

    public void killUnit(){
        if(deathAnimationName != null){
            animator.Play(deathAnimationName);
        }
        Destroy(gameObject, 0.2f);
    }
    
}

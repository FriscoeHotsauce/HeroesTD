using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{

    public Transform targetTransform;
    public Vector3 lastKnownPosition;

    public float smooth;
    public float speed;
    public int missileDamage;

    void Update()
    {
        if(targetTransform != null){
            transform.position = Vector3.Lerp(transform.position, targetTransform.position, Time.deltaTime * speed);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetTransform.rotation, Time.deltaTime * speed);
            lastKnownPosition = targetTransform.position;
            if(isHit()){
                dealDamage();
            }
        } else {
            transform.position = Vector3.Lerp(transform.position, lastKnownPosition, Time.deltaTime * speed);
            if(Vector3.Distance(lastKnownPosition, transform.position) < .5f){
                Destroy(gameObject);
            }
        }
    }

    private bool isHit(){
        return Vector3.Distance(targetTransform.position, transform.position) < 1f;
    }

    private void dealDamage(){
        targetTransform.GetComponent<HealthBar>().dealDamage(missileDamage);
        Destroy(gameObject);
    }

    public void setTarget(Transform target){
        //Debug.Log("SETTING TARGET - "+target.name);
        targetTransform = target;
        //Debug.Log(targetTransform != null);
    }

    public void setDamage(int damage){
        missileDamage = damage;
    }
}

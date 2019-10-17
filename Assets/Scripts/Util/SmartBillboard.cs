using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmartBillboard : MonoBehaviour
{
  public float smooth = 100.0f;
  public enum direction { left, right }
  private Vector3 previousPosition;

  void Update()
  {
    Quaternion target;
    if (determineDirectionOfMotion() == direction.left)
    {
      target = Quaternion.Euler(0, 180, 0);
    }
    else
    {
      target = Quaternion.Euler(0, 0, 0);
    }
    transform.rotation = Quaternion.Lerp(transform.rotation, target, Time.deltaTime * smooth);
  }

  private direction determineDirectionOfMotion()
  {
    if ((transform.position - previousPosition).x >= 0)
    {
      previousPosition = transform.position;
      return direction.right;
    }
    else
    {
      previousPosition = transform.position;
      return direction.left;
    }
  }
}

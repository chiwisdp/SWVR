using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieMovement : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject Player;
  public float movementSpeed = 4;
  void Update()
  {
    transform.LookAt(Player.transform);
    transform.position += transform.forward * movementSpeed * Time.deltaTime;
  }
}

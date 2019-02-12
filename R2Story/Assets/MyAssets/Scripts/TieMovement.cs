using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieMovement : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject Player;
  public float movementSpeed = .0125f;

  Vector2 offsetZRange = new Vector2(-25f, -85f);
  float randomizedZOffset = 0f;
  Vector3 offset;
  Vector3 lookPos;
  Quaternion rotation;
  public float damping = 5f;

  //bobbing
  private Vector3 startPos;

  public float speed = 1;
  public float xScale = 1;
  public float yScale = 1;
  private bool hasReachedDestination = false;
  void Start()
  {
    randomizedZOffset = Random.RandomRange(offsetZRange.x, offsetZRange.y);
    offset = new Vector3(offset.x, offset.y, randomizedZOffset);
    startPos = transform.position;
  }
  void FixedUpdate()
  {
    LookAtPlayer();
    if (!hasReachedDestination)
    {
      MoveToPlayer();
    }
    else
    {
      Bobbing();
    }
  }
  void LookAtPlayer()
  {
    lookPos = Player.transform.position - transform.position;
    rotation = Quaternion.LookRotation(lookPos);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
  }
  void MoveToPlayer()
  {
    Vector3 desiredPos = Player.transform.position + offset;
    startPos = desiredPos;
    Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, movementSpeed / 20f);
    transform.position = smoothedPos;
    if (transform.position.z <= randomizedZOffset)
    {
      hasReachedDestination = true;
    }
  }

  void Bobbing()
  {
    transform.position = startPos + (Vector3.right * Mathf.Sin(Time.timeSinceLevelLoad / 2 * speed) * xScale - Vector3.up * Mathf.Sin(Time.timeSinceLevelLoad * speed) * yScale);
  }
}

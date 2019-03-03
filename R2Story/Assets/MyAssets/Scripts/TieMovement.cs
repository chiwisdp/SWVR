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
  public LaserSpawner spawn0;
  public LaserSpawner spawn1;
  private float bobTimer;
  GameObject target;
  public float spawnRNGTimeRange;
  float spawnRNGTime;
  float shootTimer;
  public float laserRngRange;
  void Start()
  {
    randomizedZOffset = Random.RandomRange(offsetZRange.x, offsetZRange.y);
    offset = new Vector3(offset.x, offset.y, randomizedZOffset);
    target = GameObject.Find("X-Fighter");
    spawnRNGTime = Random.Range(.5f, spawnRNGTimeRange);
    //startPos = transform.position;
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
      shootTimer += Time.deltaTime;
      if (shootTimer > spawnRNGTime)
      {
        SpawnLaser();
      }
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
    Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, movementSpeed / 10f);
    transform.position = smoothedPos;
    if ((transform.position.z >= randomizedZOffset))
    {
      hasReachedDestination = true;
      startPos = desiredPos;
    }
    Debug.Log("MoveToPlayer");
  }

  void Bobbing()
  {
    bobTimer += Time.deltaTime;
    transform.position = startPos + (Vector3.right * Mathf.Sin(bobTimer / 2 * speed) * xScale - Vector3.up * Mathf.Sin(bobTimer * speed) * yScale);
    Debug.Log("Bobbing");

  }
  void SpawnLaser()
  {
    float rng = Random.Range(-laserRngRange, laserRngRange);
    spawn0.SpawnLaser(rng);
    spawn1.SpawnLaser(rng);
    shootTimer = 0;
    spawnRNGTime = Random.Range(.5f, spawnRNGTimeRange);
  }
}

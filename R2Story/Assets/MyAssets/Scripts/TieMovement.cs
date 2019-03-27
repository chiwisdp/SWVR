﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieMovement : MonoBehaviour
{
  // Start is called before the first frame update
  public GameObject Player;
  public float movementSpeed = .0125f;

  Vector2 offsetZRange = new Vector2(-35f, -130f);
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
  private float bobTimer;
  GameObject target;
  public float spawnRNGTimeRange;
  float spawnRNGTime;
  float shootTimer;
  public float laserRngRange;
  float wiggleRoom;
  WarpTunnel _startController;

  void Start()
  {
    _startController = GameObject.Find("WarpTunnel").GetComponent<WarpTunnel>();
    randomizedZOffset = Random.RandomRange(offsetZRange.x, offsetZRange.y);
    offset = new Vector3(offset.x, offset.y, randomizedZOffset);
    target = GameObject.Find("X-Fighter");
    spawnRNGTime = Random.Range(.5f, spawnRNGTimeRange);
    //startPos = transform.position;
  }
  void FixedUpdate()
  {
    LookAtPlayer();
    if (_startController._hasGameStarted)
    {
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
        BobbingAtDesiredPos();
      }
    }
  }
  void LookAtPlayer()
  {
    lookPos = target.transform.position - transform.position;
    rotation = Quaternion.LookRotation(lookPos);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
  }
  void MoveToPlayer()
  {
    Vector3 desiredPos = target.transform.position + offset;
    wiggleRoom = desiredPos.z - 5f;
    startPos = desiredPos;
    Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, movementSpeed / 10f);
    transform.position = smoothedPos;
    BobbingBeforeDesiredPos();
    if (transform.position.z >= wiggleRoom)
    {
      hasReachedDestination = true;
      startPos = transform.position;
      bobTimer = 0f;
    }

  }
  void BobbingBeforeDesiredPos()
  {
    if (transform.position.z <= (wiggleRoom * 2f))
    {
      bobTimer += Time.deltaTime;
      Vector3 bobPos = transform.position + (Vector3.right * Mathf.Sin(bobTimer / 2 * speed) * (xScale / 20f));
      transform.position = new Vector3(bobPos.x, transform.position.y, transform.position.z);
    }
  }

  void BobbingAtDesiredPos()
  {
    bobTimer += Time.deltaTime;
    //Vector3 bobPos =startPos + (Vector3.right * Mathf.Sin(bobTimer / 2 * speed) * xScale - Vector3.up * Mathf.Sin(bobTimer * speed) * yScale);
    transform.position = startPos + (Vector3.right * Mathf.Sin(bobTimer / 2 * speed) * xScale - Vector3.up * Mathf.Sin(bobTimer * speed) * yScale);
  }
  void SpawnLaser()
  {
    float rng = Random.Range(-laserRngRange, laserRngRange);
    spawn0.SpawnLaserTie(rng);
    shootTimer = 0;
    spawnRNGTime = Random.Range(.5f, spawnRNGTimeRange);
  }
}

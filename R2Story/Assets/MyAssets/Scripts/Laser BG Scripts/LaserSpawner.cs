using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSpawner : MonoBehaviour
{
  public GameObject target;
  public GameObject[] bgTarget;
  public GameObject laser_obj;
  public ParticleSystem muzzleFlash;
  public GameObject[] bg_laser_obj;
  float timer = 0f;
  float randomSpawnOffset;
  public bool isTurret = false;
  public bool isBGSpawner = false;
  public bool isFiringAtPlayer = false;
  public Vector2 spawnRate;
  private int rng_laser_fire;
  private float rng_fire_rate;

  private float laserRngRange = .25f;

  public int poolAmount = 2;
  WarpTunnel _startController;
  List<GameObject> bullets;
  // Use this for initialization
  void Awake()
  {
    _startController = GameObject.Find("WarpTunnel").GetComponent<WarpTunnel>();
    bullets = new List<GameObject>();
    for (int i = 0; i < poolAmount; i++)
    {
      GameObject newBullet;
      if (i % 2 == 1)
      {
        newBullet = Instantiate(bg_laser_obj[0]);
      }
      else
      {
        if (!isTurret)
        {
          newBullet = Instantiate(bg_laser_obj[1]);
        }
        else
        {
          newBullet = Instantiate(bg_laser_obj[0]);
        }
      }
      newBullet.SetActive(false);
      bullets.Add(newBullet);
    }

    if (!isBGSpawner)
    {
      target = GameObject.Find("X-Fighter");
    }
    rng_fire_rate = Random.Range(spawnRate.x, spawnRate.y);
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (_startController._hasGameStarted)
    {
      if (!isTurret)
      {
        timer += Time.deltaTime;
        if (timer > rng_fire_rate)
        {
          if (!isFiringAtPlayer)
          {
            SpawnLaser();
          }
          else
          {
            float rng = Random.Range(-laserRngRange, laserRngRange);
            SpawnLaser(rng);
          }
        }
      }
    }
  }

  void SpawnLaser()
  {
    for (int i = 0; i < bullets.Count; i++)
    {
      if (!bullets[i].activeInHierarchy)
      {
        target = bgTarget[Random.Range(0, bgTarget.Length)];
        bullets[i].transform.position = transform.position + (target.transform.position - transform.position).normalized;
        bullets[i].transform.rotation = Quaternion.LookRotation(target.transform.position - transform.position);
        bullets[i].SetActive(true);
        //GameObject spawnedLaser =  Instantiate(bg_laser_obj[rng_laser_fire], transform.position + (target.transform.position - transform.position).normalized,
        //	Quaternion.LookRotation(target.transform.position - transform.position));
        //spawnedLaser.GetComponent<LaserBullet> ().AddYVelocity (10000f);
        // Debug.Log(this.name + " :" + target.name);
        rng_fire_rate = Random.Range(spawnRate.x, spawnRate.y);
        timer = 0;
        break;
      }
    }
  }

  public void SpawnLaser(float randomLaserOffset)
  {
    //Debug.Log("SPAWN LASER");
    randomSpawnOffset = randomLaserOffset;
    for (int i = 0; i < bullets.Count; i++)
    {
      if (!bullets[i].activeInHierarchy)
      {
        Vector3 offsetTargetPos = new Vector3(target.transform.position.x * randomSpawnOffset, target.transform.position.y * randomSpawnOffset, target.transform.position.z * 1f);
        bullets[i].transform.position = transform.position + (target.transform.position - transform.position).normalized;
        bullets[i].transform.rotation = Quaternion.LookRotation(offsetTargetPos - transform.position);
        bullets[i].SetActive(true);
        bullets[i].name = "TIE" + i.ToString();
        //GameObject spawnedLaser =  Instantiate(bg_laser_obj[rng_laser_fire], transform.position + (target.transform.position - transform.position).normalized,
        //	Quaternion.LookRotation(target.transform.position - transform.position));
        //spawnedLaser.GetComponent<LaserBullet> ().AddYVelocity (10000f);
        //Debug.Log(this.name + " :" + target.name + " bullet name : " + bullets[i].name + bullets[i].transform.position + bullets[i].activeSelf);
        if (isTurret)
        {
          if (this.gameObject.GetComponent<AudioSource>().isPlaying)
          {
            this.gameObject.GetComponent<AudioSource>().Stop();
          }
          this.gameObject.GetComponent<AudioSource>().Play();
          muzzleFlash.Play();
        }
        timer = 0;
        rng_fire_rate = Random.Range(spawnRate.x, spawnRate.y);
        break;
      }
    }
  }
  public void SpawnLaserTie(float randomLaserOffset)
  {
    //Debug.Log("SPAWN LASER");
    randomSpawnOffset = randomLaserOffset;
    for (int i = 0; i < bullets.Count; i++)
    {
      if (!bullets[i].activeInHierarchy)
      {
        Vector3 offsetTargetPos = new Vector3(target.transform.position.x * randomSpawnOffset, target.transform.position.y * randomSpawnOffset, target.transform.position.z * 1f);
        bullets[i].transform.position = transform.position + (target.transform.position - transform.position).normalized;
        bullets[i].transform.rotation = Quaternion.LookRotation(offsetTargetPos - transform.position);
        bullets[i].SetActive(true);
        bullets[i].name = "TIE" + i.ToString();
        bullets[i].GetComponent<TieBullet>().SetTarget(target);
        //GameObject spawnedLaser =  Instantiate(bg_laser_obj[rng_laser_fire], transform.position + (target.transform.position - transform.position).normalized,
        //	Quaternion.LookRotation(target.transform.position - transform.position));
        //spawnedLaser.GetComponent<LaserBullet> ().AddYVelocity (10000f);
        //Debug.Log(this.name + " :" + target.name + " bullet name : " + bullets[i].name + bullets[i].transform.position + bullets[i].activeSelf);
        if (isTurret)
        {
          if (this.gameObject.GetComponent<AudioSource>().isPlaying)
          {
            this.gameObject.GetComponent<AudioSource>().Stop();
          }
          this.gameObject.GetComponent<AudioSource>().Play();
          muzzleFlash.Play();
        }
        timer = 0;
        rng_fire_rate = Random.Range(spawnRate.x, spawnRate.y);
        break;
      }
    }
  }
}

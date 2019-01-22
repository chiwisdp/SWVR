using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFire : MonoBehaviour
{
    GameObject target;
    Vector3 lookPos;
    Quaternion rotation;
    public float damping;
    public LaserSpawner spawn0;
    public LaserSpawner spawn1;
    float timer = 0f;
    public float laserRngRange;
    public float spawnRNGTimeRange;
    float spawnRNGTime;
    WarpTunnel _startController;
    // Use this for initialization
    void Awake()
    {
        _startController = GameObject.Find("WarpTunnel").GetComponent<WarpTunnel>();
        target = GameObject.Find("X-Fighter");
        spawnRNGTime = Random.Range(.5f, spawnRNGTimeRange);
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        timer += Time.deltaTime;
        if (timer > spawnRNGTime)
        {
            SpawnLaser();
        }
    }

    void LookAtPlayer()
    {
        lookPos = target.transform.position - transform.position;
        lookPos.y = 0;
        rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * damping);
    }

    void SpawnLaser()
    {
        if(_startController._hasGameStarted){
            float rng = Random.Range(-laserRngRange, laserRngRange);
            spawn0.SpawnLaser(rng);
            spawn1.SpawnLaser(rng);
            Debug.Log("Fire");
            timer = 0;
            spawnRNGTime = Random.Range(.5f, spawnRNGTimeRange);
        }
    }
}

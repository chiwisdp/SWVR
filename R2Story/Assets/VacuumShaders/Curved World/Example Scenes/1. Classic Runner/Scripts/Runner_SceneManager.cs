//VacuumShaders 2014
// https://www.facebook.com/VacuumShaders

using UnityEngine;
using System.Collections.Generic;
using System.Linq;


namespace VacuumShaders
{
  namespace CurvedWorld
  {
    [AddComponentMenu("VacuumShaders/Curved World/Example/Runner/Scene Manager")]
    public class Runner_SceneManager : MonoBehaviour
    {
      //////////////////////////////////////////////////////////////////////////////
      //                                                                          //
      //Variables                                                                 //
      //                                                                          //
      //////////////////////////////////////////////////////////////////////////////
      static public Runner_SceneManager get;

      public float speed = 1;
      public GameObject[] chunks;
      public GameObject endWall;
      public float totalPlayTime;
      private float currentPlaytime;
      public GameObject[] cars;

      static public float chunkSize = 249;
      static public Vector3 moveVector = new Vector3(0, 0, -1);
      static public GameObject lastChunk;

      List<Material> listMaterials;
      public float zChunkOffset;
      public WarpTunnel _startController;
      private bool canSpawn;
      private bool hasSpawnedWall;
      //////////////////////////////////////////////////////////////////////////////
      //                                                                          //
      //Unity Functions                                                           //
      //                                                                          //
      //////////////////////////////////////////////////////////////////////////////
      void Awake()
      {
        get = this;
        canSpawn = true;
        _startController = GameObject.Find("WarpTunnel").GetComponent<WarpTunnel>();

        zChunkOffset = chunks.Length / 2;
        //Instantiate chunks
        for (int i = 0; i < chunks.Length; i++)
        {
          GameObject obj = (GameObject)Instantiate(chunks[i]);

          obj.transform.position = new Vector3(0, 0, (i * chunkSize) - (zChunkOffset * chunkSize));

          lastChunk = obj;
        }

        //Instantiate cars
        for (int i = 0; i < cars.Length; i++)
        {
          Instantiate(cars[i]);
        }
      }

      // Use this for initialization
      void Start()
      {
        Renderer[] renderers = FindObjectsOfType(typeof(Renderer)) as Renderer[];

        listMaterials = new List<Material>();
        foreach (Renderer _renderer in renderers)
        {
          listMaterials.AddRange(_renderer.sharedMaterials);
        }

        listMaterials = listMaterials.Distinct().ToList();
      }

      //////////////////////////////////////////////////////////////////////////////
      //                                                                          //
      //Custom Functions                                                          //
      //                                                                          //
      //////////////////////////////////////////////////////////////////////////////
      void Update()
      {
        if (_startController._hasGameStarted)
        {
          currentPlaytime += Time.deltaTime;
          Debug.Log("current Playtime : " + currentPlaytime);

        }
        if (currentPlaytime >= totalPlayTime)
        {
          if (!hasSpawnedWall)
          {
            SpawnEndWall(endWall.GetComponent<Runner_Chunk>());
            Time.timeScale = .3f;
            canSpawn = false;
            speed = 100;
          }
        }
      }
      void SpawnEndWall(Runner_Chunk moveElement)
      {
        Vector3 newPos = lastChunk.transform.position;
        newPos.z += 0;
        GameObject obj = (GameObject)Instantiate(endWall);

        obj.transform.position = newPos;

        lastChunk = obj;
        lastChunk.transform.position = newPos;
        hasSpawnedWall = true;
      }
      public bool GetCanSpawn()
      {
        return canSpawn;
      }
      public void DestroyChunk(Runner_Chunk moveElement)
      {
        if (canSpawn)
        {
          Vector3 newPos = lastChunk.transform.position;
          newPos.z += chunkSize;


          lastChunk = moveElement.gameObject;
          lastChunk.transform.position = newPos;
        }
      }

      public void DestroyCar(Runner_Car car)
      {
        GameObject.Destroy(car.gameObject);

        Instantiate(cars[Random.Range(0, cars.Length)]);
      }
    }
  }
}
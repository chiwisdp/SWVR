﻿//VacuumShaders 2014
// https://www.facebook.com/VacuumShaders

using UnityEngine;
using System.Collections;


namespace VacuumShaders
{
  namespace CurvedWorld
  {
    [AddComponentMenu("VacuumShaders/Curved World/Example/Runner/Move Element")]
    public class Runner_Chunk : MonoBehaviour
    {
      //////////////////////////////////////////////////////////////////////////////
      //                                                                          //
      //Unity Functions                                                           //
      //                                                                          //
      //////////////////////////////////////////////////////////////////////////////
      Runner_SceneManager controller;
      void Awake()
      {
        controller = GameObject.FindObjectOfType<Runner_SceneManager>();
      }
      void Update()
      {
        transform.Translate(Runner_SceneManager.moveVector * Runner_SceneManager.get.speed * Time.deltaTime);
      }

      void FixedUpdate()
      {

        if (transform.position.z < -1500 && controller.GetCanSpawn())
          Runner_SceneManager.get.DestroyChunk(this);
      }
    }
  }
}
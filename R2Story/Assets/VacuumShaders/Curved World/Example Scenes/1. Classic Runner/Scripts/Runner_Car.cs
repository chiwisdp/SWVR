using UnityEngine;
using System.Collections;

namespace VacuumShaders
{
  namespace CurvedWorld
  {
    [AddComponentMenu("VacuumShaders/Curved World/Example/Runner/Move Element")]
    public class Runner_Car : MonoBehaviour
    {
      //////////////////////////////////////////////////////////////////////////////
      //                                                                          // 
      //Variables                                                                 //                
      //                                                                          //               
      //////////////////////////////////////////////////////////////////////////////

      Rigidbody rigidBody;
      public float speed = 1;

      //////////////////////////////////////////////////////////////////////////////
      //                                                                          // 
      //Unity Functions                                                           //                
      //                                                                          //               
      //////////////////////////////////////////////////////////////////////////////
      void Start()
      {
        rigidBody = GetComponent<Rigidbody>();

        transform.position = new Vector3(Random.Range(-3.5f, 3.5f), 1, Random.Range(140, 240));
        speed = Random.Range(2f, 6f);

      }
      void Update()
      {
        transform.Translate(Runner_SceneManager.moveVector * Runner_SceneManager.get.speed * Time.deltaTime);
      }
      void FixedUpdate()
      {

        if (transform.position.y < -1500)
        {
          Runner_SceneManager.get.DestroyCar(this);
        }
      }

      //////////////////////////////////////////////////////////////////////////////
      //                                                                          // 
      //Custom Functions                                                          //
      //                                                                          //               
      //////////////////////////////////////////////////////////////////////////////
    }
  }
}
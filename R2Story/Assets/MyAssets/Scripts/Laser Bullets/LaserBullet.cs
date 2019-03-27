using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{

  float bulletForce = 350;
  float yVelOffset = 0;
  public bool isTieLaser = false;
  public void AddYVelocity(float yOffset)
  {
    yVelOffset = yOffset;
    GetComponent<Rigidbody>().AddForce(transform.up * yVelOffset);

  }

  void OnEnable()
  {
    if (isTieLaser)
    {
      bulletForce = 1f; //10
    }
    Invoke("Kill", 7f);
  }
  void Update()
  {
    transform.Translate(Vector3.forward * bulletForce * Time.deltaTime);
    Debug.Log(this.name + " : " + GetComponent<Rigidbody>().velocity);
  }
  void Kill()
  {
    gameObject.SetActive(false);
    GetComponent<Rigidbody>().velocity = Vector3.zero;
  }
  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.name == "x-wing")
    {
      Kill();
      Debug.Log(collision.gameObject.name);
    }
  }

  void OnBecameInvisible()
  {
    Kill();
    Debug.Log(this.name + " Became invisible");
  }
}

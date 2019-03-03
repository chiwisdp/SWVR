using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{

  float bulletForce = 350;
  float yVelOffset = 0;
  public bool isTieLAser = false;
  public void AddYVelocity(float yOffset)
  {
    yVelOffset = yOffset;
    GetComponent<Rigidbody>().AddForce(transform.up * yVelOffset);

  }

  void OnEnable()
  {
    if (isTieLAser)
    {
      bulletForce = 10f;
    }
    Invoke("Kill", 7f);
  }
  void Update()
  {
    transform.Translate(Vector3.forward * bulletForce * Time.deltaTime);
    Debug.Log("LASER MOVE");
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

  void OnDisable()
  {
    CancelInvoke();
  }
}

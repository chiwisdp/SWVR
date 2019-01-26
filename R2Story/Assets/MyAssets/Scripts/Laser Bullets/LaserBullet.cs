using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBullet : MonoBehaviour
{

  float bulletForce = 350;
  float yVelOffset = 0;
  public void AddYVelocity(float yOffset)
  {
    yVelOffset = yOffset;
    GetComponent<Rigidbody>().AddForce(transform.up * yVelOffset);

  }

  void OnEnable()
  {
    Invoke("Kill", 7f);
  }
  void Update()
  {
    transform.Translate(Vector3.forward * bulletForce * Time.deltaTime);
  }
  void Kill()
  {
    gameObject.SetActive(false);
    GetComponent<Rigidbody>().velocity = Vector3.zero;
  }
  void OnCollisionEnter(Collision collision)
  {
    Kill();
  }

  void OnDisable()
  {
    CancelInvoke();
  }
}

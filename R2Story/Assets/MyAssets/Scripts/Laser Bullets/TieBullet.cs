using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TieBullet : MonoBehaviour
{
  // Start is called before the first frame update
  private float speed = 250f;
  public GameObject target;
  float yVelOffset = 0;
  void OnEnable()
  {
    Invoke("Kill", 7f);
    yVelOffset = Random.Range(-1, 3f);
  }

  // Update is called once per frame
  void Update()
  {
    if (target != null &&
    (Vector3.Distance(transform.position, target.transform.position) > 5f))
    {
      float step = speed * Time.deltaTime;
      this.transform.position = Vector3.MoveTowards(this.transform.position,
        target.transform.position + new Vector3(0, yVelOffset, 0), step);
    }
    else
    {
      Kill();
    }
  }

  void OnCollisionEnter(Collision collision)
  {
    if (collision.gameObject.name == "x-wing")
    {
      Kill();
      Debug.Log(collision.gameObject.name);
    }
  }
  void Kill()
  {
    gameObject.SetActive(false);
    GetComponent<Rigidbody>().velocity = Vector3.zero;
  }
  public void AddYVelocity(float yOffset)
  {
    yVelOffset = yOffset;
    GetComponent<Rigidbody>().AddForce(transform.up * yVelOffset);

  }
  public void SetTarget(GameObject targetToSet)
  {
    target = targetToSet;
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FXV
{
  public class FXVBullet : MonoBehaviour
  {

    public float speed = 50.0f;
    public float lifetime = 2.0f;

    public float bulletHitSize = 1.0f;

    public GameObject hitParticles;

    private Vector3 moveDir = Vector3.zero;
    private Vector3 postMoveDir = Vector3.zero;
    private Ray ray;

    private float currentTime = 0.0f;
    private Vector2 rngOffset;
    private float rngXRange = 6f;
    private float rngYRange = 6f;
    void Start()
    {

    }

    void Update()
    {
      Vector3 newPos = transform.position + moveDir * speed * Time.deltaTime;

      ray.origin = transform.position;
      RaycastHit rhi;

      Vector3 offset = newPos - transform.position;

      currentTime += Time.deltaTime;

      //transform.position = newPos;
      float dist = Vector3.Distance(moveDir, transform.position);
      float step = speed * Time.deltaTime;
      Debug.Log(dist + "  this: " + transform.position.z + "  MOVE: " + moveDir.z);
      if ((dist > 5) && (transform.position.z <= moveDir.z))
      {
        transform.position = Vector3.MoveTowards(this.transform.position,
        moveDir, step);
      }
      else
      {
        transform.position = Vector3.MoveTowards(this.transform.position,
        postMoveDir, step);
      }

      bool needDestroy = false;

      if (currentTime > lifetime)
      {
        needDestroy = true;
      }

      if (Physics.Raycast(ray, out rhi, offset.magnitude))
      {
        needDestroy = true;

        FXVShield shield = rhi.collider.gameObject.GetComponentInParent<FXVShield>();

        if (shield && !shield.GetIsDuringActivationAnim())
        {
          shield.OnHit(rhi.point, bulletHitSize);

          if (hitParticles)
          {
            GameObject ps = Instantiate(hitParticles, transform.position, Quaternion.identity);
            ps.transform.position = transform.position;
            ps.GetComponent<ParticleSystem>().Emit(25);
            Destroy(ps, 3.0f);
          }
        }
      }
      if (needDestroy)
      {
        DestroyObject(gameObject);
      }
    }

    public void Shoot(Vector3 dir)
    {
      currentTime = 0.0f;
      rngOffset = new Vector2(Random.Range(-rngXRange, rngXRange), Random.Range(-rngYRange, rngYRange));
      moveDir = new Vector3(dir.x + rngOffset.x, dir.y + rngOffset.y, dir.z);
      postMoveDir = new Vector3(dir.x + rngOffset.x, dir.y + rngOffset.y, dir.z + 1000f);
      ray = new Ray(transform.position, moveDir);

    }
  }

}
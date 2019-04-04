using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace FXV
{
  public class FXVShooter : MonoBehaviour
  {
    public GameObject[] guns;
    public GameObject bulletPrefab;

    public bool autoShoot = false;
    public float autoShootRate = 1.0f;
    public float autoShootDispersion = 15.0f;

    private int currentGun = 0;

    private Vector3 baseShootDir;
    private float timeToNextShoot = 0.0f;
    public GameObject target;
    void Start()
    {
      baseShootDir = transform.forward;
    }
    public void SetAutoShoot()
    {
      autoShoot = true;
    }

    void Update()
    {
      if (target != null)
      {
        Debug.DrawRay(this.transform.position, target.transform.position, Color.red);
        Debug.Log(target.transform.position);
      }
      if (autoShoot)
      {
        timeToNextShoot -= Time.deltaTime;
        if (timeToNextShoot <= 0.0f)
        {
          Vector3 dir = Quaternion.Euler(Random.Range(-autoShootDispersion, autoShootDispersion), Random.Range(-autoShootDispersion, autoShootDispersion), Random.Range(-autoShootDispersion, autoShootDispersion)) * baseShootDir;
          //gameObject.transform.rotation = Quaternion.LookRotation(target.gameObject.transform.eulerAngles, Vector3.up);
          Shoot(dir);

          timeToNextShoot = 1.0f / autoShootRate;
        }
      }
      else
      {
        Ray ray = new Ray(transform.position, target.transform.position);
        RaycastHit rhi;

        if (Physics.Raycast(ray, out rhi))
        {
          Vector3 dirShip = rhi.point - target.transform.position;
          dirShip.Normalize();
          gameObject.transform.rotation = Quaternion.LookRotation(dirShip, Vector3.up);

          if (!EventSystem.current.IsPointerOverGameObject())
          {
            if (Input.GetMouseButtonDown(0))
            {
              Vector3 dirBullet = rhi.point - guns[currentGun].transform.position;
              dirBullet.Normalize();

              Shoot(dirBullet);
            }
          }
        }
      }
    }

    public void Shoot(Vector3 dir)
    {
      GameObject bullet = GameObject.Instantiate(bulletPrefab, guns[currentGun].transform.position, guns[currentGun].transform.rotation);

      bullet.GetComponent<FXVBullet>().Shoot(target.transform.position);

      currentGun++;
      if (currentGun >= guns.Length)
      {
        currentGun = 0;
      }
    }
  }

}
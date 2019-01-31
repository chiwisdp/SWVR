using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagetWallUI : MonoBehaviour
{
  // Start is called before the first frame update
  float maxSize = 0.00011f;
  float currentSize;
  float timer;
  void Start()
  {
    currentSize = transform.localScale.x;
  }

  // Update is called once per frame
  void Update()
  {
    timer += Time.deltaTime * .0000015f;
    // Widen the object by 0.1
    transform.localScale += new Vector3(timer, timer, timer);
    if (transform.localScale.x >= maxSize)
    {
      transform.localScale = Vector3.zero;
      timer = 0f;
    }
  }
}

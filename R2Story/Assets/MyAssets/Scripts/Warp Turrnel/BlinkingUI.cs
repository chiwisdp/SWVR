using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingUI : MonoBehaviour
{
  // Start is called before the first frame update
  float timer;
  float shownTime = .5f;
  float unshowTime = .25f;
  SpriteRenderer thisRend;
  void OnEnable()
  {
    thisRend = gameObject.GetComponent<SpriteRenderer>();
    timer = shownTime;
  }

  // Update is called once per frame
  void Update()
  {
    timer -= Time.deltaTime;
    if (thisRend.enabled && (timer <= 0f))
    {
      thisRend.enabled = false;
      timer = unshowTime;
    }
    else if (!thisRend.enabled && (timer <= 0f))
    {
      thisRend.enabled = true;
      timer = shownTime;
    }
  }
}

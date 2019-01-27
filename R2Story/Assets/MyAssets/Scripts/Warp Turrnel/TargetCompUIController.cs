using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCompUIController : MonoBehaviour
{
  // Start is called before the first frame update
  TextMesh targetText;
  float currentDistance = 100000f;
  GameObject targetAquiredUI;
  void Awake()
  {
    targetAquiredUI = GameObject.Find("TargetUI");
    targetText = GameObject.Find("DistanceUIText").GetComponent<TextMesh>();
    targetAquiredUI.SetActive(false);
  }

  // Update is called once per frame
  void Update()
  {
    currentDistance -= (Time.deltaTime * 10f);
    targetText.text = Mathf.FloorToInt(currentDistance).ToString();
  }
  public void SetIsAtTargetArea()
  {
    targetAquiredUI.SetActive(true);
  }
}

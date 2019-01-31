using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCompUIController : MonoBehaviour
{
  // Start is called before the first frame update
  TextMesh targetText;
  float currentDistance = 100000f;
  GameObject targetAquiredUI;
  Animation rightLine;
  Animation leftLine;
  float linetimer;
  void Awake()
  {
    targetAquiredUI = GameObject.Find("TargetUI");
    rightLine = GameObject.Find("TargetRightLine").GetComponent<Animation>();
    leftLine = GameObject.Find("TargetLeftLine").GetComponent<Animation>();
    targetText = GameObject.Find("DistanceUIText").GetComponent<TextMesh>();
    targetAquiredUI.SetActive(false);
  }
  void OnEnable()
  {
    rightLine.Play();
    leftLine.Play();
    rightLine["RightLine"].speed = .051f;
    leftLine["LeftLine"].speed = .051f;
  }
  // Update is called once per frame
  void Update()
  {
    currentDistance -= (Time.deltaTime * 10f);
    linetimer += Time.deltaTime * 1;
    // Widen the object by 0.1
    if (rightLine.transform.localPosition.x >= 0f)
    {
      //rightLine.transform.localPosition += new Vector3(linetimer, rightLine.transform.localPosition.y, rightLine.transform.localPosition.z);
    }
    targetText.text = Mathf.FloorToInt(currentDistance).ToString();
  }
  public void SetIsAtTargetArea()
  {
    targetAquiredUI.SetActive(true);
  }
}

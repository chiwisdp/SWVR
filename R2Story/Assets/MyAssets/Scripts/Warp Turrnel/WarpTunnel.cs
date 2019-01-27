﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTunnel : MonoBehaviour
{

  // Use this for initialization
  Material tunnelMat;

  public ParticleSystem frontLight;
  public ParticleSystem backLight;
  public ParticleSystem stars;
  public ParticleSystem rings;
  public ParticleSystem fog;
  public AudioClip warpLoop;
  public AudioClip warpClose;
  AudioSource audio;
  SpriteRenderer logo;
  Light mainLight;
  public Color warpColor;
  public Color normalColor;
  SpriteRenderer whiteOverlay;
  public bool _hasGameStarted = false;
  private bool hasCalledFadeBlack;
  private GameObject targetingUI;
  void Awake()
  {
    targetingUI = GameObject.Find("TargetCompUI");
    tunnelMat = this.GetComponent<Renderer>().material;
    audio = this.GetComponent<AudioSource>();
    whiteOverlay = GameObject.Find("WhiteOverlay").GetComponent<SpriteRenderer>();
    logo = GameObject.Find("Logo").GetComponent<SpriteRenderer>();
    whiteOverlay.color = new Color(1, 1, 1, 0);
    mainLight = GameObject.Find("MainLight").GetComponent<Light>();
    mainLight.color = warpColor;
    audio.clip = warpLoop;
    audio.loop = true;
    audio.Play();
  }

  // Update is called once per frame
  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space) && !_hasGameStarted)
    {
      StartCoroutine("FadeOutTunnel");
      StartCoroutine(FadeWhiteOverlay());
    }
  }
  IEnumerator FadeWhiteOverlay()
  {
    yield return new WaitForSeconds(.3f);
    for (float i = 0; i <= 1; i += (Time.deltaTime * 1f))
    {
      whiteOverlay.color = new Color(1, 1, 1, i);
      mainLight.color = Color.Lerp(warpColor, normalColor, i);
      yield return null;
    }
    targetingUI.SetActive(true);
    for (float i = 1; i >= 0; i -= (Time.deltaTime * 2f))
    {
      whiteOverlay.color = new Color(1, 1, 1, i);
      yield return null;
    }
  }

  public void FadeInBlackOverlay()
  {
    if (!hasCalledFadeBlack)
    { StartCoroutine(FadeBlackOverlay()); }
  }
  IEnumerator FadeBlackOverlay()
  {
    hasCalledFadeBlack = true;
    yield return new WaitForSeconds(3f);
    for (float i = 0; i <= 1; i += (Time.deltaTime * 1f))
    {
      // set color with i as alpha
      whiteOverlay.color = new Color(0, 0, 0, i);
      yield return null;
    }
  }
  IEnumerator FadeOutTunnel()
  {
    for (float f = 2f; f >= 0; f -= 0.15f)
    {
      Color c = tunnelMat.color;
      Color lc = Color.white;
      lc.a = f;
      c.a = f;
      if (c.a > 1)
      {
        c.a = 1;
      }
      tunnelMat.color = c;
      logo.color = lc;

      if (f <= .5f)
      {
        TurnOffBigLights();
      }
      if (f <= .35f)
      {
        TurnOffRest();
        turnOffAudioLoop();
      }


      yield return new WaitForSeconds(.1f);
    }
    tunnelMat.color = Color.clear;
    logo.color = Color.clear;
    _hasGameStarted = true;
  }
  void TurnOffBigLights()
  {
    frontLight.Stop();
    backLight.Stop();
  }

  void TurnOffRest()
  {
    stars.Stop();
    rings.Stop();
    fog.Stop();
  }

  void turnOffAudioLoop()
  {
    if (audio.clip != warpClose)
    {
      audio.clip = warpClose;
      audio.loop = false;
      audio.volume = 2;
      audio.Play();
    }
  }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpTunnel : MonoBehaviour {

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

	void Awake () {
		tunnelMat = this.GetComponent<Renderer> ().material;
		audio = this.GetComponent<AudioSource> ();
		whiteOverlay = GameObject.Find ("WhiteOverlay").GetComponent<SpriteRenderer> ();
		logo = GameObject.Find ("Logo").GetComponent<SpriteRenderer> ();
		whiteOverlay.color = new Color(1,1,1,0);
		mainLight = GameObject.Find ("MainLight").GetComponent<Light> ();
		mainLight.color = warpColor;
		audio.clip = warpLoop;
		audio.loop = true;
		audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			StartCoroutine("LightAnimation");
			StartCoroutine("FadeOutTunnel");
		}

	
	}

	IEnumerator FadeOutTunnel() {
		for (float f = 2f; f >= 0; f -= 0.15f) {
			Color c = tunnelMat.color;
			Color lc = Color.white;
			lc.a = f;
			c.a = f;
			if (c.a > 1) {
				c.a = 1;
			}
			tunnelMat.color = c;
			logo.color = lc;
			 
			if (f <= .5f) {
				TurnOffBigLights ();
			}
			if (f <= .35f) {
				TurnOffRest ();
				turnOffAudioLoop ();
			}


			yield return new WaitForSeconds(.1f);
		}
		tunnelMat.color = Color.clear;
		logo.color = Color.clear;
		_hasGameStarted = true;
	}

	IEnumerator LightAnimation() {
		for (float f = 0f; f <= 2; f += 0.15f) {
			mainLight.color = Color.Lerp (warpColor,normalColor, f/2 );

			//mainLight.intensity = f; 
			if (f >= .5f && f <= .75f) {
				
				whiteOverlay.color = new Color(1,1,1,f/2f);
			}
			if (f >= .75f && f <= .77f) {
				whiteOverlay.color = new Color(1,1,1,f*2);
			}

			yield return new WaitForSeconds(.1f);
		}

		whiteOverlay.color = Color.clear;
		whiteOverlay.gameObject.SetActive(false);
	}


	void TurnOffBigLights(){
		frontLight.Stop ();
		backLight.Stop ();
	}

	void TurnOffRest(){
		stars.Stop ();
		rings.Stop ();
		fog.Stop ();
	}

	void turnOffAudioLoop(){
		if (audio.clip != warpClose) {
			audio.clip = warpClose;
			audio.loop = false;
			audio.volume = 2;
			audio.Play ();
		}

	}
}

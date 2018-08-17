using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGShipSpawner : MonoBehaviour {
	BGFighterObjectPool bgPooler;
	// Use this for initialization
	void Start () {
		bgPooler = BGFighterObjectPool.Instance;
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		bgPooler.SpawnFromPool("X", transform.position, Quaternion.identity);
	}
}

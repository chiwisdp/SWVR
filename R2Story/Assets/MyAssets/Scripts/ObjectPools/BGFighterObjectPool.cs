using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGFighterObjectPool : MonoBehaviour {
	[System.Serializable]
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int size;
	}
	public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;

	#region Singleton
	public static BGFighterObjectPool Instance;
		private void Awake()
		{
			Instance = this;
		}
	#endregion
	// Use this for initialization
	void Start () {
		poolDictionary = new Dictionary<string, Queue<GameObject>>();
		foreach(Pool pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();

			for (int i=0; i<pool.size; i++)
			{
				GameObject obj = Instantiate(pool.prefab);
				obj.SetActive(false);
				objectPool.Enqueue(obj);
			}
			poolDictionary.Add(pool.tag, objectPool);
		}
	}

	public GameObject SpawnFromPool (string tag, Vector3 pos, Quaternion rot)
	{
		if(!poolDictionary.ContainsKey(tag))
		{
			Debug.Log("pool with tag "+tag +" doesn't exist");
			return null;
		}
		GameObject objToSpawn = poolDictionary[tag].Dequeue();

		objToSpawn.SetActive(true);
		objToSpawn.transform.position = pos;
		objToSpawn.transform.rotation = rot;

		poolDictionary[tag].Enqueue(objToSpawn);
		return objToSpawn;
	}
}

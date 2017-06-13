using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour {

	//"cannot save it as part of the prefab, so it must be non-serializable"
	//i don't understand this
	[System.NonSerialized]
	ObjectPool poolInstanceForPrefab;

	public ObjectPool Pool { get; set; }

	public void ReturnToPool(){

		//if a pool exists, add the object to it
		//else, just destroy the object
		if (Pool) {
			Pool.AddObject (this);
		} else {
			Destroy (gameObject);
		}

	}

	//generic method for returning the right prefab for the pool?
	public T GetPooledInstance<T> () where T : PooledObject{
		if (!poolInstanceForPrefab) {
			poolInstanceForPrefab = ObjectPool.GetPool (this);
		}

		return (T)poolInstanceForPrefab.GetObject ();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	//which prefab is being pooled
	PooledObject prefab;

	//"array" of pooled objects
	List<PooledObject> availibleObjects = new List<PooledObject>();

	//get a pooled object
	public PooledObject GetObject (){

		PooledObject obj;

		//get the most recent object in the array to prevent shifting
		int lastAvailibleIndex = availibleObjects.Count - 1;

		//if there's at least one object in the pool, reactivate it
		//else, make a new object and pretend it was in the pool
		if (lastAvailibleIndex >= 0) {
			
			obj = availibleObjects [lastAvailibleIndex];
			availibleObjects.RemoveAt (lastAvailibleIndex);
			obj.gameObject.SetActive (true);

		} else {

			obj = Instantiate<PooledObject> (prefab);
			obj.transform.SetParent (transform, false);
			obj.Pool = this;

		}

		return obj;
	}

	//deactivate an object entering the pool and add it to the list
	public void AddObject(PooledObject obj){
		obj.gameObject.SetActive (false);
		availibleObjects.Add (obj);
	}

	//not sure why this had to be static
	public static ObjectPool GetPool (PooledObject prefab){

		GameObject obj;  
		ObjectPool pool; 

		//prevent editor from doing something wrong
		//(it was making duplicate pools i think)
		if(Application.isEditor){
			obj = GameObject.Find (prefab.name + " Pool");

			if (obj) {
				pool = obj.GetComponent<ObjectPool> ();

				if (pool) {
					return pool;
				}
			}
		}

		//make a new pool and return it
		obj = new GameObject (prefab.name + " Pool");
		pool = obj.AddComponent<ObjectPool> ();
		pool.prefab = prefab;
		return pool;
	}
}

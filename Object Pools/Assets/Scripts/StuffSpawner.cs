using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffSpawner : MonoBehaviour {

	//floatrange struct to make choosing random values easier
	[System.Serializable]
	public struct FloatRange {

		public float min, max;

		public float RandomInRange{ get { return Random.Range (min, max); } }
	}

	public FloatRange timeBetweenSpawns, scale, randomVelocity, angularVelocity;
	float currentSpawnDelay;

	public float velocity;

	//does this mean an array of gameObjects that have a Stuff Component?
	//or is at an array of the components?
	public Stuff[] stuffPrefabs;

	float timeSinceLastSpawn;

	public Material stuffMaterial;

	//-------------------------------------------------------------------------------------------------------------

	void FixedUpdate() {

		//spawn the objects based on pseudo-random times
		timeSinceLastSpawn = timeSinceLastSpawn + Time.deltaTime;

		if (timeSinceLastSpawn >= currentSpawnDelay) {

			timeSinceLastSpawn = timeSinceLastSpawn - currentSpawnDelay;
			currentSpawnDelay = timeBetweenSpawns.RandomInRange;
			SpawnStuff ();
		}
	}

	void SpawnStuff(){

		//get a random prefab, and then find a copy from the pool
		Stuff prefab = stuffPrefabs[Random.Range(0, stuffPrefabs.Length)];
		Stuff spawn = prefab.GetPooledInstance<Stuff> ();

		//object is reset to look like a new object, when in reality it's just reactivated
		spawn.transform.localPosition = transform.position;
		spawn.transform.localScale = Vector3.one * scale.RandomInRange;
		spawn.transform.localRotation = Random.rotation;

		spawn.Body.velocity = transform.up * velocity + Random.onUnitSphere * randomVelocity.RandomInRange;
		spawn.Body.angularVelocity = Random.onUnitSphere * angularVelocity.RandomInRange;

		spawn.SetMaterial (stuffMaterial);
	}
}

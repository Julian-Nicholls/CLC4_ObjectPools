using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//stuff is a terrible name for a script
[RequireComponent(typeof(Rigidbody))]
public class Stuff : PooledObject {

	//changed Body from a field to a property (???)
	public Rigidbody Body { get; private set; }

	//list of mesh renderers in object and it's children
	MeshRenderer[] meshRenderers;

	void Awake(){
		Body = GetComponent<Rigidbody> ();
		meshRenderers = GetComponentsInChildren<MeshRenderer> ();
	}

	//when object has fallen too far, put it back in the pool
	void OnTriggerEnter(Collider enteredCollider){
		if (enteredCollider.CompareTag ("Kill Zone")) {
			ReturnToPool();
		}
	}

	//set the material for each mesh renderer in children
	public void SetMaterial (Material m){
		for (int i = 0; i < meshRenderers.Length; i++) {
			meshRenderers [i].material = m;
		}
	}
}

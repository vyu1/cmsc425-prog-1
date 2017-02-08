using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticles : MonoBehaviour {

	public float windForce;

	void OnParticleCollision (GameObject other) {
		Rigidbody body = other.GetComponent<Rigidbody> ();
		if (other.gameObject.CompareTag ("Ball")) 
		{
			body.AddForce(Vector3.right * windForce);
		}
	}
}

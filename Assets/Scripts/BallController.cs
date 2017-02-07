using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	public float floatSpeed;
	public float moveSpeed;
	public Rigidbody rb;

	private bool goalEntered;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		goalEntered = false;
	}

	void FixedUpdate () 
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * moveSpeed);
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Goal")) 
		{
			this.rb.isKinematic = true;
			goalEntered = true;
		}
	}

	void Update () 
	{
		if (goalEntered) {
			this.gameObject.transform.Translate (Vector3.up * floatSpeed * Time.deltaTime, Space.World);
		}

		// restarts the game
		if (Input.GetKeyDown (KeyCode.R)) 
		{
			Application.LoadLevel(0);
		}

		// quits the game
		if (Input.GetKey (KeyCode.Escape) || Input.GetKey (KeyCode.Q))
		{
			Application.Quit();
		}
	}
}

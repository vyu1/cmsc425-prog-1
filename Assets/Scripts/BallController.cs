using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour {

	public float floatSpeed;
	public float moveSpeed;
	public float thrustSpeed;
	public float gridZ;
	public float gridX;

	public Rigidbody rb;
	public GameObject hingeCenterPrefab;
	public GameObject innerWallPrefab;
	public Transform plane;

	private List<GameObject> wallParentPrefabs = new List<GameObject> ();
	private List<GameObject> currentlyRotatingPrefabs = new List<GameObject> ();
	private bool goalEntered;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		goalEntered = false;

		for (float z = -4; z <= gridZ; z+=2) {
			for (float x = -4; x <= gridX; x+=2) {
				Vector3 pos = new Vector3 (x, 0.1f, z);
				GameObject obj = Instantiate (hingeCenterPrefab, pos, Quaternion.identity, plane) as GameObject;
				Vector3 pos2 = new Vector3 (x-0.4f, 0.1f, z);
				GameObject obj2 = Instantiate (innerWallPrefab, pos2, Quaternion.identity) as GameObject;
				obj2.transform.parent = obj.transform;
				wallParentPrefabs.Add (obj);
				float randomDirection = Random.Range (0, 4);
				obj.transform.rotation = Quaternion.AngleAxis (90 * randomDirection, Vector3.up);
			}
		}
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

		// if the user hits the space bar, the ball jumps.
		if (Input.GetKeyDown (KeyCode.Space)) 
		{
			rb.AddForce(0, thrustSpeed, 0, ForceMode.Impulse);
		}

		foreach (GameObject wallParent in wallParentPrefabs) {
			float randomNumber = Random.Range (0.0f, 1.0f);
			int rotationDirection = Random.Range (0, 2);
			if (randomNumber < (Time.deltaTime / 10) && !currentlyRotatingPrefabs.Contains(wallParent)) {
				wallParent.transform.rotation = Quaternion.AngleAxis (90, Vector3.up);
//				currentlyRotatingPrefabs.Add (wallParent);
//				if (rotationDirection == 0) {
//					StartCoroutine(RotateMe(wallParent, Vector3.up * 90, 1));
//				} else {
//					StartCoroutine(RotateMe(wallParent, Vector3.up * 270, 1));
//				}
			}
		}
	}

	IEnumerator RotateMe(GameObject wallParent, Vector3 byAngles, float inTime) {
		var fromAngle = wallParent.transform.rotation;
		var toAngle = Quaternion.Euler(wallParent.transform.eulerAngles + byAngles);
		for(var t = 0f; t < 1; t += Time.deltaTime/inTime) {
			wallParent.transform.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
			yield return null;
		}
		currentlyRotatingPrefabs.Remove (wallParent);
	}
}

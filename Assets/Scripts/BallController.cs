using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

	public float floatSpeed = 10.0f;
	public float moveSpeed = 1.0f;
	public float thrustSpeed = 5.0f;
	public float gridZ = 4.0f;
	public float gridX = 4.0f;

	public Rigidbody rb;
	public GameObject hingeCenterPrefab;
	public GameObject innerWallPrefab;
	public Transform plane;
	public GameObject pickUpPrefab;
	public Transform pickUps;
	public Text countText;
	public Text goalText;

	private bool goalEntered;
	private int count = 2;

	void Start () 
	{
		rb = GetComponent<Rigidbody> ();
		goalEntered = false;

		// instantiate the inner walls and their random rotation orientation
		for (float z = -4; z <= gridZ; z++) {
			for (float x = -4; x <= gridX; x++) {
				if ((z != 0 || x != 0) && ((z + x) % 2 == 0)) {
					Vector3 pos = new Vector3 (x, 0f, z);
					GameObject obj = Instantiate (hingeCenterPrefab, pos, Quaternion.identity, plane) as GameObject;
					Vector3 pos2 = new Vector3 (x-0.4f, 0.1f, z);
					GameObject obj2 = Instantiate (innerWallPrefab, pos2, innerWallPrefab.transform.rotation) as GameObject;
					obj2.transform.parent = obj.transform;
					float randomDirection = Random.Range (0, 4);
					obj.transform.rotation = Quaternion.AngleAxis (90 * randomDirection, Vector3.up);
				}
			}
		}

		// instantiate the 6 pick up objects
		for (int i = 0; i < 6; i++) {
			float pickUpLocationX = Random.Range (-5, 5) + 0.5f;
			float pickUpLocationZ = Random.Range (-5, 5) + 0.5f;
			while (pickUpLocationX == 4.5 && pickUpLocationZ == 4.5) {
				pickUpLocationX = Random.Range (-5, 5) + 0.5f;
				pickUpLocationZ = Random.Range (-5, 5) + 0.5f;
			}
			Vector3 pos = new Vector3 (pickUpLocationX, 0.25f, pickUpLocationZ);
			Instantiate (pickUpPrefab, pos, Quaternion.identity, pickUps);
		}

		// this is the starting count of the game.
		count = 2;
		SetCountText ();
	}

	void SetCountText() {
		if (count <= 0) {
			countText.text = "Done! Go to the goal!";
		} else {
			countText.text = "Pick-ups remaining: " + count.ToString ();
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
		if (other.gameObject.CompareTag ("Goal") && count <= 0) 
		{
			countText.text = "";
			goalText.text = "Congratulations. You win!";
			this.rb.isKinematic = true;
			goalEntered = true;
		} else if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count -= 1;
			SetCountText ();
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

		if (this.gameObject.transform.position.y <= -10) {
			countText.text = "";
			goalText.text = "Sorry. You lose!";
		}
	}
}

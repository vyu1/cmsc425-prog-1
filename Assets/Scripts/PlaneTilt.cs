using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneTilt : MonoBehaviour {

	public float tiltAngle = 10.0f;
	public Vector3 currentRot;
	public float tiltSensitivity = 20.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		currentRot = NormalizeAngles (GetComponent<Transform> ().eulerAngles);

		// press right, go clockwise
		if ((Input.GetAxis ("Horizontal") > 0.2) && currentRot.z >= -tiltAngle)
		{
			this.transform.Rotate (0, 0, tiltSensitivity * Time.deltaTime * -Input.GetAxis("Horizontal"));
		}

		// press left, go counter-clockwise
		if ((Input.GetAxis ("Horizontal") < -0.2) && currentRot.z <= tiltAngle)
		{
			this.transform.Rotate (0, 0, tiltSensitivity * Time.deltaTime * -Input.GetAxis("Horizontal"));
		}

		// press up, rotate away from game view
		if (Input.GetAxis ("Vertical") > 0.2 && currentRot.x <= tiltAngle) 
		{
			this.transform.Rotate (tiltSensitivity * Time.deltaTime * Input.GetAxis("Vertical"), 0, 0);
		}

		// press downwards, rotate towards game view
		if (Input.GetAxis ("Vertical") < -0.2 && currentRot.x >= -tiltAngle) 
		{
			this.transform.Rotate (tiltSensitivity * Time.deltaTime * Input.GetAxis("Vertical"), 0, 0);
		}

	}

	Vector3 NormalizeAngles (Vector3 currentRot) {
		if (currentRot.x >= 180) 
		{
			currentRot.x -= 360;
		}
		if (currentRot.y >= 180) 
		{
			currentRot.y -= 360;
		}
		if (currentRot.z >= 180) 
		{
			currentRot.z -= 360;
		}
		return currentRot;
	}
}

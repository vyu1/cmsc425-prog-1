using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRotator : MonoBehaviour {

	private bool isRotating;
	// the total number of degrees that the wall has rotated so far. resets to 0 every time it rotates a full 90 degrees.
	private float rotationDegrees;
	// determines whether the wall will rotate clockwise/counterclockwise. resets every time the wall rotates a full 90 degrees.
	private int rotationDirection;

	// Use this for initialization
	void Start () {
		isRotating = false;	
		rotationDegrees = 0;
	}
	
	// Update is called once per frame
	void Update () {
		// this random number determines whether the wall should start to rotate or not
		float randomNumber = Random.Range (0.0f, 1.0f);
		if (randomNumber < (Time.deltaTime / 10) && !isRotating) {
			isRotating = true;
			rotationDegrees = 0;
			rotationDirection = Random.Range (0, 2);
		} else if (isRotating) {
			float rotationAngle;
			if (rotationDirection == 0) {
				// rotating clockwise
				rotationAngle = 90 * Time.deltaTime;
			} else {
				// rotating counter-clockwise
				rotationAngle = 270 * Time.deltaTime;
			}

			// rotate the wall such that it is aligned with the x and z axis.
			if (rotationDegrees + rotationAngle >= 90) {
				transform.parent.transform.Rotate (Vector3.up * (90 - rotationDegrees));
			} else {
				transform.parent.transform.Rotate (Vector3.up * rotationAngle);
			}
			rotationDegrees += rotationAngle;

			if (rotationDegrees >= 90) {
				isRotating = false;
				rotationDegrees = 0;
			}
		}
	}
}

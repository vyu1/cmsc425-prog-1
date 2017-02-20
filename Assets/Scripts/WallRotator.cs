using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRotator : MonoBehaviour {

	private bool isRotating;
	private float rotationDegrees;

	// Use this for initialization
	void Start () {
		isRotating = false;	
		rotationDegrees = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float randomNumber = Random.Range (0.0f, 1.0f);
		if (randomNumber < (Time.deltaTime / 10) && !isRotating) {
			isRotating = true;
			rotationDegrees = 0;
		} else if (isRotating) {
			int rotationDirection = Random.Range (0, 2);
			float rotationAngle;
			if (rotationDirection == 0) {
				rotationAngle = 90 * Time.deltaTime;
			} else {
				rotationAngle = 270 * Time.deltaTime;
			}
			transform.parent.transform.Rotate (Vector3.up * rotationAngle);
			rotationDegrees += rotationAngle;
			if (rotationDegrees >= 90) {
				isRotating = false;
				rotationDegrees = 0;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{

	GameObject ThePlayer;
	Lookat TheLookat;
	float TurnAmount = 0;

	void Start ()
	{
		ThePlayer = GameObject.FindGameObjectWithTag("Player");
		TheLookat = FindObjectOfType<Lookat>();
	}

	void LateUpdate ()
	{
		bool cameraRotated = false;

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			cameraRotated = true;
			TurnAmount += 50 * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			cameraRotated = true;
			TurnAmount -= 50 * Time.deltaTime;
		}

		if (cameraRotated)
		{
			transform.rotation = Quaternion.Euler(0, TurnAmount, 0);
			TheLookat.UpdateLookat(ThePlayer.transform.position);
		}

		transform.position = ThePlayer.transform.position;
	}
}

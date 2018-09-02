using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{

	void Start ()
	{

	}

	void LateUpdate ()
	{

    }

    public void UpdateLookat(Vector3 lookat)
    {
        transform.LookAt(lookat);
    }
}

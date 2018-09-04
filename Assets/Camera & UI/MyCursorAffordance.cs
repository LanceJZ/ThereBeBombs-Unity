﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MyCameraRaycaster))]
public class MyCursorAffordance : MonoBehaviour {

	[SerializeField] Texture2D WalkCursor;
	[SerializeField] Texture2D TargetCursor;
	[SerializeField] Texture2D UnknownCursor;
	[SerializeField] Vector2 CursorHotspot = new Vector2(96, 96);

	MyCameraRaycaster Raycaster;

	// Use this for initialization
	void Start ()
	{
		Raycaster = GetComponent<MyCameraRaycaster>();
//		Raycaster.LayerChange += OnLayerChanged; // registering
	}

	//void OnLayerChanged(Layer newLayer)
	//{
	//	switch (newLayer)
	//	{
	//		case Layer.Walkable:
	//			Cursor.SetCursor(WalkCursor, CursorHotspot, CursorMode.Auto);
	//			break;
	//		case Layer.Enemy:
	//			Cursor.SetCursor(TargetCursor, CursorHotspot, CursorMode.Auto);
	//			break;
	//		case Layer.Wall:
	//			Cursor.SetCursor(UnknownCursor, CursorHotspot, CursorMode.Auto);
	//			break;
	//		case Layer.RaycastEndStop:
	//			Cursor.SetCursor(UnknownCursor, CursorHotspot, CursorMode.Auto);
	//			break;
	//		default:
	//			Debug.LogError("Don't know what cursor to show");
	//			return;
	//	}
	//}

	// TODO consider de-registering OnLayerChanged on leaving all game scenes
}

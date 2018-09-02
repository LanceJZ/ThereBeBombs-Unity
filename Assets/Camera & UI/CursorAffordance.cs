using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CameraRaycaster))]

public class CursorAffordance : MonoBehaviour
{

	[SerializeField] Texture2D WalkCursor;
	[SerializeField] Texture2D TargetCursor;
	[SerializeField] Texture2D UnknownCursor;
	[SerializeField] Vector2 CursorHotspot = new Vector2(0, 0);

	public const int WalkableLayerNumber = 8;
	public const int EnemyLayerNumber = 9;
    public const int WallLayer = 10;
    public const int ObjectLayer = 11;
    public const int CorpseLayer = 12;

    CameraRaycaster cameraRaycaster;

	// Use this for initialization
	void Start ()
	{
		cameraRaycaster = GetComponent<CameraRaycaster>();
		cameraRaycaster.NotifyLayerChangeObservers += OnLayerChanged; // registering
	}

	void OnLayerChanged(int newLayer)
	{
		switch (newLayer)
		{
			case WalkableLayerNumber:
				Cursor.SetCursor(WalkCursor, CursorHotspot, CursorMode.Auto);
				break;
			case EnemyLayerNumber:
				Cursor.SetCursor(TargetCursor, CursorHotspot, CursorMode.Auto);
				break;
			default:
				Cursor.SetCursor(UnknownCursor, CursorHotspot, CursorMode.Auto);
				return;
		}
	}
}

using UnityEngine;

// Add a UI Socket transform to your enemy
// Attach this script to the socket
// Link to a canvas prefab
public class EnemyUI : MonoBehaviour
{
    // Work around for Unity 2018's lack of nested prefabs
    [Tooltip("The UI canvas prefab")]
    [SerializeField] GameObject EnemyCanvasPrefab;

    Camera CameraToLookAt;

    void Start()
    {
        CameraToLookAt = Camera.main;
        Instantiate(EnemyCanvasPrefab, transform.position, transform.rotation, transform);
    }

    void LateUpdate()
    {
        transform.LookAt(CameraToLookAt.transform);
    }
}
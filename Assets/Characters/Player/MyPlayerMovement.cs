using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.AI;

[RequireComponent(typeof (ThirdPersonCharacter))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]

public class MyPlayerMovement : MonoBehaviour
{
//    [SerializeField] float WalkMoveStopRadius = 0.2f;
    [SerializeField] float MeleeAttackStopRadius = 0.75f;
    [SerializeField] float RangeAttackStopRadius = 5.1f;

    //Vector3 CurrentClickTarget, ClickPoint;
    ThirdPersonCharacter Character;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster Raycaster;
    AICharacterControl AICharacterControl;
    Transform Target;

    //float DistanceToStop = 0;
    bool IsInDirectMode = false;

    void Start()
    {
        Raycaster = Camera.main.GetComponent<CameraRaycaster>();
        Character = GetComponent<ThirdPersonCharacter>();
        AICharacterControl = GetComponent<AICharacterControl>();
        Target = new GameObject("walkTarget").transform;
        //CurrentClickTarget = transform.position;

        Raycaster.NotifyMouseClickObservers += ProcessMouseClick;
    }

    void Update()
    {
        if (Input.GetJoystickNames().Length > 0) // Auto switch if gamepad is activated. Deactivate joystick detection in menu.
        {
            if (Input.GetJoystickNames()[0].Length > 0)
            {
                if (!IsInDirectMode)
                {
                    IsInDirectMode = true; // toggle mode
                    //CurrentClickTarget = transform.position; // clear the click target
                }
            }
            else
            {
                if (IsInDirectMode)
                {
                    IsInDirectMode = false;
                }
            }
        }
    }

    // Fixed update is called in sync with physics
    void FixedUpdate()
    {
        if (IsInDirectMode)
        {
            ProcessDirectMovement();
        }
        else
        {
            //ProcessMouseMovement();
        }
    }

    void ProcessMouseClick(RaycastHit raycastHit, int layerHit)
    {
        switch (layerHit)
        {
            case CursorAffordance.EnemyLayerNumber:
                // navigate to the enemy
                //GameObject enemy = raycastHit.collider.gameObject;
                AICharacterControl.SetTarget(raycastHit.collider.gameObject.transform);
                break;
            case CursorAffordance.WalkableLayerNumber:
                // navigate to point on the ground
                Target.position = raycastHit.point;
                AICharacterControl.SetTarget(Target);
                break;
            default:
                Debug.LogWarning("Don't know how to handle mouse click for player movement");
                return;
        }
    }

    void ProcessDirectMovement()
    {
        float horz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        // calculate camera relative direction to move:
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 movement = vert * cameraForward + horz * Camera.main.transform.right;

        Character.Move(movement, false, false);
    }

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.grey;
    //    Gizmos.DrawLine(transform.position, CurrentClickTarget);
    //    Gizmos.DrawSphere(CurrentClickTarget, 0.1f);
    //    Gizmos.DrawSphere(ClickPoint, 0.15f);

    //    //Draw Melee attack sphere.
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, MeleeAttackStopRadius);

    //    //Draw Range attack sphere.
    //    Gizmos.color = Color.cyan;
    //    Gizmos.DrawWireSphere(transform.position, RangeAttackStopRadius);
    //}

    //void ProcessMouseMovement()
    //{
    //if (Input.GetMouseButton(0))
    //{
    //ClickPoint = Raycaster.Hit.point;

    //switch (Raycaster.CurrentLayerHit)
    //{
    //    case Layer.Walkable:
    //        CurrentClickTarget = ShortDestion(ClickPoint, WalkMoveStopRadius);
    //        break;
    //    case Layer.Enemy:
    //        CurrentClickTarget = ShortDestion(ClickPoint, MeleeAttackStopRadius);
    //        break;
    //    case Layer.Wall:
    //        print("That is a wall, not moving.");
    //        break;
    //    default:
    //        print("Unexpected layer clicked.");
    //        return;
    //}
    //}

    //WalkToDestination();
    //}

    //void WalkToDestination()
    //{
    //    Vector3 playerToClickPoint = CurrentClickTarget - transform.position;

    //    if (playerToClickPoint.magnitude >= DistanceToStop)
    //    {
    //        Character.Move(playerToClickPoint, false, false);
    //    }
    //    else
    //    {
    //        Character.Move(Vector3.zero, false, false);
    //    }
    //}

    //Vector3 ShortDestion(Vector3 destination, float shortening)
    //{
    //    Vector3 reductionVector = (destination - transform.position).normalized * shortening;
    //    DistanceToStop = reductionVector.magnitude;
    //    return destination - reductionVector;
    //}
}


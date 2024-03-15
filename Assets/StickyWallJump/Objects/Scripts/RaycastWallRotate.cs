using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWallRotate : MonoBehaviour
{
    public GameObject objectPlayer;

    public float rayLength = 0.1f;
    public Vector3 leftRotation;
    public Vector3 rightRotation;
    public Vector3 upRotation;
    public Vector3 downRotation;

    [SerializeField] private LayerMask arcLayer;

    public bool isActive;

    void Update()
    {
        transform.position = objectPlayer.transform.position;

        RaycastHit hitLeft;
        RaycastHit hitRight;
        RaycastHit hitUp;
        RaycastHit hitDown;

        if (Physics.Raycast(transform.position, -transform.right, out hitLeft, rayLength, arcLayer))
        {
            if (isActive)
            {
                objectPlayer.transform.rotation = Quaternion.Euler(leftRotation);
                objectPlayer.transform.position = hitLeft.point;
                isActive = false;
            }
        }
        else if (Physics.Raycast(transform.position, transform.right, out hitRight, rayLength, arcLayer))
        {
            if (isActive)
            {
                objectPlayer.transform.rotation = Quaternion.Euler(rightRotation);
                objectPlayer.transform.position = hitRight.point;
                isActive = false;
            }
        }
        else if (Physics.Raycast(transform.position, transform.up, out hitUp, rayLength, arcLayer))
        {
            if (isActive)
            {
                objectPlayer.transform.rotation = Quaternion.Euler(upRotation);
                objectPlayer.transform.position = hitUp.point;
                isActive = false;
            }
        }
        else if (Physics.Raycast(transform.position, -transform.up, out hitDown, rayLength, arcLayer))
        {
            if (isActive)
            {
                objectPlayer.transform.rotation = Quaternion.Euler(downRotation);
                objectPlayer.transform.position = hitDown.point;
                isActive = false;
            }
        }

        Debug.DrawRay(transform.position, -transform.right * rayLength, Color.red);
        Debug.DrawRay(transform.position, transform.right * rayLength, Color.green);
        Debug.DrawRay(transform.position, transform.up * rayLength, Color.blue);
        Debug.DrawRay(transform.position, -transform.up * rayLength, Color.yellow);
    }
}
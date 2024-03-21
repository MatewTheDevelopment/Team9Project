using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastWallRotate : MonoBehaviour
{
    public GameObject objectPlayer;

    public float rayLengthLeft = 0.1f;
    public float rayLengthRight = 0.1f;
    public float rayLengthUp = 0.1f;
    public float rayLengthDown = 0.1f;

    public GameObject leftRotationObject;
    public GameObject rightRotationObject;
    public GameObject upRotationObject;

    public Vector3 leftRotation;
    public Vector3 rightRotation;
    public Vector3 upRotation;
    public Vector3 downRotation;

    [SerializeField] private LayerMask arcLayer;

    public bool isActive;

    private void Start()
    {
        RaycastUpdater();
    }

    public void RaycastUpdater()
    {
        Debug.Log("Hello"); 

        RaycastHit hitLeft;
        RaycastHit hitRight;
        RaycastHit hitUp;
        RaycastHit hitDown;

        if (Physics.Raycast(leftRotationObject.transform.position, -leftRotationObject.transform.right, out hitLeft, rayLengthLeft, arcLayer, QueryTriggerInteraction.Ignore))
        {
            Attract(leftRotation, hitLeft);
        }
        else if (Physics.Raycast(rightRotationObject.transform.position, rightRotationObject.transform.right, out hitRight, rayLengthRight, arcLayer, QueryTriggerInteraction.Ignore))
        {
            Attract(rightRotation, hitRight);
        }
        else if (Physics.Raycast(upRotationObject.transform.position, upRotationObject.transform.up, out hitUp, rayLengthUp, arcLayer, QueryTriggerInteraction.Ignore))
        {
            Attract(upRotation, hitUp);
        }
        else if (Physics.Raycast(transform.position, -transform.up, out hitDown, rayLengthDown, arcLayer, QueryTriggerInteraction.Ignore))
        {
            Attract(downRotation, hitDown);
        }
    }

    private void Update()
    {
        Debug.DrawRay(leftRotationObject.transform.position, -leftRotationObject.transform.right * rayLengthLeft, Color.red);
        Debug.DrawRay(rightRotationObject.transform.position, rightRotationObject.transform.right * rayLengthRight, Color.green);
        Debug.DrawRay(upRotationObject.transform.position, upRotationObject.transform.up * rayLengthUp, Color.blue);
        Debug.DrawRay(transform.position, -transform.up * rayLengthDown, Color.yellow);
    }

    public void Attract(Vector3 rotate, RaycastHit hit)
    {
        objectPlayer.transform.rotation = Quaternion.Euler(rotate);
        objectPlayer.transform.position = hit.point;
        objectPlayer.GetComponent<Rigidbody>().isKinematic = true;
    }
}
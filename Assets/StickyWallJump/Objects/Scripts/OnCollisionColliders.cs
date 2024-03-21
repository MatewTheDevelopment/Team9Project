using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionColliders : MonoBehaviour
{
    public RaycastWallRotate RayActive;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RotateNull();
        }
    }

    public void RotateNull()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}

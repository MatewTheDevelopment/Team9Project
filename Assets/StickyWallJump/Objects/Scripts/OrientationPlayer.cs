using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrientationPlayer : MonoBehaviour
{
    public Quaternion LeftPosition;
    public Quaternion RightPosition;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            transform.localRotation = LeftPosition;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.localRotation = RightPosition;
        }
    }
}

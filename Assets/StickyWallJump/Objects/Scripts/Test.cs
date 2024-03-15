using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public float impulseForce = 10f; // Сила импульса

    public Rigidbody rb;
    private Vector3 mouseDownPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 targetPosition = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
            Vector3 direction = targetPosition - transform.position;
            rb.AddForce(direction.normalized * impulseForce, ForceMode.Impulse);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    public Rigidbody rb;

    public float impulseForce = 10f;

    void Update()
    {
        CheckMouse();
    }

    public void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10f));

            Vector3 direction = worldPosition - transform.position;
            direction.z = 0f;

            gameObject.GetComponent<Rigidbody>().isKinematic = false;

            rb.AddForce(direction.normalized * impulseForce, ForceMode.Impulse);
        }
    }
}
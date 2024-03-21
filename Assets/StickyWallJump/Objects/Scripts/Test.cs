using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Quaternion leftRotation;
    public Quaternion rightRotation;
    public Quaternion upRotation;
    public Quaternion downRotation;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            Vector3 rightSide = collision.transform.position + Vector3.right * collision.transform.localScale.x / 2;
            Vector3 leftSide = collision.transform.position - Vector3.right * collision.transform.localScale.x / 2;
            Vector3 topSide = collision.transform.position + Vector3.up * collision.transform.localScale.y / 2;
            Vector3 bottomSide = collision.transform.position - Vector3.up * collision.transform.localScale.y / 2;

            Vector3 characterPosition = transform.position;

            if (characterPosition.x > rightSide.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, -90);
                // Притягивание персонажа к правой
                Vector3 newPosition = new Vector3(rightSide.x, characterPosition.y, characterPosition.z);
                transform.position = newPosition;
                rb.isKinematic = true;
            }
            else if (characterPosition.x < leftSide.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, 90);
                // Притягивание персонажа к левой
                Vector3 newPosition = new Vector3(leftSide.x, characterPosition.y, characterPosition.z);
                transform.position = newPosition;
                rb.isKinematic = true;
            }
            else if (characterPosition.y > topSide.y)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                // Притягивание персонажа к верхней
                Vector3 newPosition = new Vector3(characterPosition.x, topSide.y, characterPosition.z);
                transform.position = newPosition;
                rb.isKinematic = true;
            }
            else if (characterPosition.y < bottomSide.y)
            {
                transform.rotation = Quaternion.Euler(0, 0, -180);
                // Притягивание персонажа к нижней
                Vector3 newPosition = new Vector3(characterPosition.x, bottomSide.y, characterPosition.z);
                transform.position = newPosition;
                rb.isKinematic = true;
            }
        }
    }
}
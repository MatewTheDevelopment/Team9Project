using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    private Vector3 mouseDownPosition;
    private Rigidbody rb;
    public float impulseForce = 10f;

    [SerializeField] private LayerMask arcLayer;

    public RaycastWallRotate wallRotate;

    public bool IsActive;

    public bool IsGround = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        CheckMouse();

        if (IsActive)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }  
    }

    public void CheckMouse()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseDownPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            OnEnablePhysicsBoxCollider(false);
            OnEnablePhysics(false);
            
            Vector3 mouseUpPosition = Input.mousePosition;
            Vector3 direction = (mouseUpPosition - mouseDownPosition).normalized;
            rb.AddForce(direction * impulseForce, ForceMode.Impulse);

            StartCoroutine(OnEnablePhysicsBoxCollider(0.1f, true));

            IsActive = true;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if ((arcLayer & (1 << collision.gameObject.layer)) != 0)
        {
            if (IsGround)
            {
                OnEnablePhysics(true);
                IsActive = false;
                IsGround = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        wallRotate.isActive = true;
        IsActive = false;
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void OnEnablePhysics(bool isActive)
    {
        rb.isKinematic = isActive;
    }

    public IEnumerator OnEnablePhysicsBoxCollider(float delay, bool isActive)
    {
        yield return new WaitForSeconds(delay);
        OnEnablePhysicsBoxCollider(isActive);
    }

    public void OnEnablePhysicsBoxCollider(bool isActive)
    {
        IsGround = isActive;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeathAmountAdd : MonoBehaviour
{
    public int healtAdd;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<HealthPlayer>().Health += healtAdd;

            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}

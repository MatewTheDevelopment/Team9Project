using UnityEngine;

public class FireDamage : MonoBehaviour
{
    [SerializeField] private float maxTime;

    private float currentTime;

    private void Update()
    {
        currentTime -= Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerDamage") && currentTime <= 0)
        {
            currentTime = maxTime;

            other.GetComponent<HealthPlayer>().TakeDamage(20);
        }
    }
}

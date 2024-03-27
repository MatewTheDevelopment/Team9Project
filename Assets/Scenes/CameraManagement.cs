using UnityEngine;

public class CameraManagement : MonoBehaviour
{
    [SerializeField] private Vector3 offset;

    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != false)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position - offset, Time.deltaTime * 50);
        }
        else
        {
            return;
        }
    }
}

using UnityEngine;

public class ProjectTileManagement : MonoBehaviour
{
    [SerializeField] private float speed, lifeTime;

    [SerializeField] private LayerMask solid;

    private Vector3 direction;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        direction = player.transform.position - transform.position;

        Invoke("DestroyIt", lifeTime);
    }

    private void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, 1f, solid))
        {
            DestroyIt();
        }
    }

    private void DestroyIt()
    {
        Destroy(this.gameObject);
    }
}

using System.Collections;
using UnityEngine;

public class AIManagementVooDoo : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform[] points;

    [SerializeField] private Transform hitPoint;

    [SerializeField] private GameObject projectTile;

    [SerializeField] private float speed, maxTime, maxHitTime, maxUltraHitTime, minTime, hitDistance, ultraHitDistance;

    [SerializeField] private Animator animator;

    [SerializeField] private int maxHealth;

    private bool isDead;

    private int currentHealth;

    private Vector3 currentTarget;

    private float currentTime, currentHitTime, currentUltraHitTime;

    [HideInInspector] public bool canSeePlayer;

    public float radius;

    [Range(0, 360)] public float angle;

    public LayerMask targetMask, obstructionMask;

    public GameObject player;

    private void Awake()
    {
        currentTarget = transform.position;

        player = GameObject.FindGameObjectWithTag("Player");

        currentHealth = maxHealth;

        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        if (!isDead)
        {
            AxisCheck();

            ViewOfField();

            currentTime -= Time.deltaTime; currentHitTime -= Time.deltaTime; currentUltraHitTime -= Time.deltaTime;

            animator.SetBool("Ready?", canSeePlayer);
        }
    }

    private void AxisCheck()
    {
        if (currentHitTime <= 0 && currentUltraHitTime <= 0)
        {
            transform.LookAt(currentTarget);

            int lastLenght = points.Length - 1;

            int index = Random.Range(0, points.Length);

            if (transform.position.z > points[lastLenght].position.z)
            {
                transform.position = new Vector3(0, transform.position.y, points[lastLenght].position.z);

                currentTarget = new Vector3(0, transform.position.y, points[index].position.z);
            }
            else if (transform.position.z < points[0].position.z)
            {
                transform.position = new Vector3(0, transform.position.y, points[0].position.z);

                currentTarget = new Vector3(0, transform.position.y, points[index].position.z);
            }

            if (currentTime <= 0 && canSeePlayer == false)
            {
                transform.position = Vector3.MoveTowards(transform.position, currentTarget, Time.deltaTime * speed);

                animator.SetBool("Move?", true);

                if (currentTarget == transform.position)
                {
                    animator.SetBool("Move?", false);

                    index = Random.Range(0, points.Length);

                    currentTarget = new Vector3(0, transform.position.y, points[index].position.z);

                    currentTime = Random.Range(minTime, maxTime);
                }
            }
            else if (canSeePlayer == true)
            {
                Collider[] hit = Physics.OverlapSphere(hitPoint.position, hitDistance, targetMask);

                Collider[] ultraHit = Physics.OverlapSphere(hitPoint.position, ultraHitDistance, targetMask);

                if (ultraHit.Length != 0)
                {
                    animator.SetTrigger("UltraHit");

                    currentUltraHitTime = maxUltraHitTime;

                    Invoke("UltraHit", maxUltraHitTime / 2);
                }
                else if (hit.Length != 0)
                {
                    animator.SetTrigger("Hit");

                    currentHitTime = maxHitTime;

                    Invoke("Hit", maxHitTime / 2);
                }
            }
        }
    }

    private void Hit()
    {
        Debug.Log("Hit!!!");

        Instantiate(projectTile, hitPoint.position, Quaternion.identity);
    }

    private void UltraHit()
    {
        Debug.Log("UltraHit!!!");

        Collider[] ultraHit = Physics.OverlapSphere(hitPoint.position, ultraHitDistance, targetMask);

        if(ultraHit.Length != 0)
        {
            player.GetComponent<Rigidbody>().AddForce((player.transform.position - hitPoint.position) * 10, ForceMode.Impulse);
        }
    }

    private void ViewOfField()
    {
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, radius, targetMask);

        if (rangeChecks.Length != 0)
        {
            Transform target = rangeChecks[0].transform;

            Vector3 directionToTarget = (target.position - transform.position).normalized;

            if (Vector3.Angle(transform.forward, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionMask)) {
                    canSeePlayer = true;
                }
                else {
                    canSeePlayer = false;
                }
            }
            else {
                canSeePlayer = false;
            }
        }
        else if (canSeePlayer) {
            canSeePlayer = false;
        }
            
    }

    private IEnumerator FOVRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;

            ViewOfField();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if(currentHealth <= 0)
        {
            animator.SetTrigger("Death");

            isDead = true;

            //this.GetComponent<>().
        }
    }
}

using System.Collections;
using UnityEngine;

public class AIManagementLittleWarrior : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform hitPoint, spawnPoint;

    [SerializeField] private GameObject littleWarrior;

    [SerializeField] private float speed, maxTime, maxHitTime, maxUltraHitTime, maxKickTime, minTime, hitDistance, ultraHitDistance;

    [SerializeField] private float hitCoolDownMaxTime, ultraHitCoolDownMaxTime, powerUpCoolDownMaxTime, kickCoolDownMaxTime;

    [SerializeField] private Animator animator;

    [SerializeField] private int maxHealth;

    [SerializeField] private Vector3 offset;

    private bool isDead;

    private int currentHealth;

    private Vector3 currentTarget;

    private Vector3[] points;

    private float currentTime, currentHitTime, currentUltraHitTime, currentKickTime;

    private float hitCoolDownTime, ultraHitCoolDownTime, powerUpCoolDownTime, kickCoolDownTime;

    [HideInInspector] public bool canSeePlayer;

    public float radius;

    [Range(0, 360)] public float angle;

    public LayerMask targetMask, obstructionMask;

    public GameObject player;

    private void Awake()
    {
        points = new Vector3[2];

        currentTarget = transform.position;

        points[1] = transform.position;

        points[0] = transform.position + offset;

        player = GameObject.FindGameObjectWithTag("Player");

        if(player == null) { Destroy(this.gameObject); }

        StartCoroutine(FOVRoutine());
    }

    private void Update()
    {
        if (!isDead)
        {
            AxisCheck();

            ViewOfField();

            currentTime -= Time.deltaTime; currentHitTime -= Time.deltaTime; currentUltraHitTime -= Time.deltaTime;

            hitCoolDownTime -= Time.deltaTime; ultraHitCoolDownTime -= Time.deltaTime; powerUpCoolDownTime -= Time.deltaTime; kickCoolDownTime -= Time.deltaTime;

            animator.SetBool("Move?", canSeePlayer);
        }
    }

    private void AxisCheck()
    {
        if (currentHitTime <= 0 && currentUltraHitTime <= 0 && powerUpCoolDownTime <= 0)
        {
            int lastLenght = points.Length - 1;

            if (transform.position.z > points[lastLenght].z)
            {
                transform.position = new Vector3(0, transform.position.y, points[lastLenght].z);
            }
            else if (transform.position.z < points[0].z)
            {
                transform.position = new Vector3(0, transform.position.y, points[0].z);
            }

            if (currentTime <= 0 && canSeePlayer == false)
            {
                Invoke("CheckBack", 3);
            }
            else if (canSeePlayer == true)
            {
                currentTarget = new Vector3(0, transform.position.y, player.transform.position.z);

                currentTime = 0;

                transform.position = Vector3.MoveTowards(transform.position, currentTarget, Time.deltaTime * speed * 1.25f);

                animator.SetBool("Move?", true);

                Collider[] hit = Physics.OverlapSphere(hitPoint.position, hitDistance, targetMask);

                Collider[] ultraHit = Physics.OverlapSphere(hitPoint.position, ultraHitDistance, targetMask);

                if (hit.Length != 0 && hitCoolDownTime <= 0)
                {
                    if (Random.Range(0, 10) > 7.5f)
                    {
                        animator.SetTrigger("Kick");

                        currentHitTime = maxKickTime;

                        Invoke("Kick", maxKickTime / 2);

                        kickCoolDownTime = kickCoolDownMaxTime;
                    }
                    else
                    {
                        animator.SetTrigger("Hit");

                        currentHitTime = maxHitTime;

                        Invoke("Hit", maxHitTime / 2);

                        hitCoolDownTime = hitCoolDownMaxTime;
                    }
                }
                else if (ultraHit.Length != 0 && ultraHitCoolDownTime <= 0)
                {
                    animator.SetTrigger("UltraHit");

                    currentUltraHitTime = maxUltraHitTime;

                    Invoke("UltraHit", maxUltraHitTime / 2 + 0.5f);

                    ultraHitCoolDownTime = ultraHitCoolDownMaxTime;
                }
            }
        }
    }

    private void Hit()
    {
        Collider[] hit = Physics.OverlapSphere(hitPoint.position, hitDistance, targetMask);

        if (hit.Length != 0)
        {
            player.GetComponent<Rigidbody>().AddForce((player.transform.position - hitPoint.position) * 2, ForceMode.Impulse);
        }
    }

    private void UltraHit()
    {
        Collider[] hit = Physics.OverlapSphere(hitPoint.position, ultraHitDistance, targetMask);

        if (hit.Length != 0)
        {
            
        }
    }

    private void Kick()
    {
        Collider[] hit = Physics.OverlapSphere(hitPoint.position, hitDistance, targetMask);

        if (hit.Length != 0)
        {
            player.GetComponent<Rigidbody>().AddForce((player.transform.position - hitPoint.position) * 10, ForceMode.Impulse);
        }
    }

    private void CheckBack()
    {
        if(canSeePlayer == false)
        {
            transform.LookAt(new Vector3(0, transform.position.y, player.transform.position.z));
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

        if (currentHealth <= 0)
        {
            animator.SetTrigger("Death");

            isDead = true;
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Ninja : MonoBehaviour
{
    public Transform target;
    public float followDistance = 10f;
    public float runDistance = 5f;
    public float shootDistance = 7f;
    public float runSpeed = 5f;
    public float walkSpeed = 1f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private NavMeshAgent agent;
    private Animator animator;
    private bool isFollowing = false;
    private bool canShoot = true;
    public float fireRate = 1f;

    public Transform pointA;
    public Transform pointB;
    private bool isWalkingToB = true;

    public AudioClip walkSound;
    public AudioClip runSound;
    public AudioClip shootSound;
    public AudioClip gruntSound;

    private AudioSource audioSource;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = walkSpeed;

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (agent != null)
        {
            agent.enabled = true;
        }
        else
        {
            Debug.LogError("NavMeshAgent is not attached!");
        }

        SetNextDestination();
    }

    void Update()
    {
        if (agent.enabled)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);

            var distance = Vector3.Distance(transform.position, target.position);

            if (distance <= followDistance && !isFollowing)
            {
                isFollowing = true;
            }
            else if (distance > followDistance && isFollowing)
            {
                isFollowing = false;
                agent.speed = walkSpeed;
                animator.SetBool("run", false);
            }

            if (isFollowing)
            {
                if (distance <= runDistance)
                {
                    agent.speed = runSpeed;
                    animator.SetBool("run", true);
                    PlayRunSound();
                }
                else
                {
                    agent.speed = walkSpeed;
                    animator.SetBool("run", false);
                    PlayWalkSound();
                }

                if (agent.isOnNavMesh)
                {
                    agent.SetDestination(target.position);
                }
                else
                {
                    Debug.LogWarning("Agent is not on the NavMesh!");
                }

                if (distance <= shootDistance && canShoot)
                {
                    StartCoroutine(ShootAtPlayer());
                }
            }

            if (!isFollowing)
            {
                if (Vector3.Distance(transform.position, pointA.position) < 1f && !isWalkingToB)
                {
                    isWalkingToB = true;
                    SetNextDestination();
                }
                else if (Vector3.Distance(transform.position, pointB.position) < 1f && isWalkingToB)
                {
                    isWalkingToB = false;
                    SetNextDestination();
                }
            }
        }
        else
        {
            Debug.LogWarning("NavMeshAgent is disabled.");
        }
    }

    public void GetHurt()
    {
        animator.Play("Head Hit 1");
        StartCoroutine(StopAndWait());
        PlayGruntSound();
    }

    IEnumerator StopAndWait()
    {
        agent.enabled = false;
        yield return new WaitForSeconds(2);
        agent.enabled = true;
    }

    IEnumerator ShootAtPlayer()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(firePoint.forward * 20f, ForceMode.Impulse);
            }

            canShoot = false;
            PlayShootSound();
            yield return new WaitForSeconds(fireRate);
            canShoot = true;
        }
    }

    private void SetNextDestination()
    {
        if (isWalkingToB)
        {
            agent.SetDestination(pointB.position);
        }
        else
        {
            agent.SetDestination(pointA.position);
        }
    }

    private void PlayWalkSound()
    {
        if (walkSound != null && !audioSource.isPlaying)
        {
            audioSource.clip = walkSound;
            audioSource.Play();
        }
    }

    private void PlayRunSound()
    {
        if (runSound != null && !audioSource.isPlaying)
        {
            audioSource.clip = runSound;
            audioSource.Play();
        }
    }

    private void PlayShootSound()
    {
        if (shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }
    }

    private void PlayGruntSound()
    {
        if (gruntSound != null)
        {
            audioSource.PlayOneShot(gruntSound);
        }
    }
}

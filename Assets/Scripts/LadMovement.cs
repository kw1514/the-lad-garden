using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// script adapted from a script on github by  @Templar2020 https://gist.github.com/Templar2020/8e4f5296de96d8ccf03263bf1a9f277f

public class LadMovement : MonoBehaviour
{
    public float wanderRadius;
    public float wanderTimer;

    Transform target;
    NavMeshAgent agent;
    LadGenes ladGenes;
    Animator animator;
    bool wandering = false;

    private float timer;

    private void Start()
    {
        ladGenes = GetComponent<LadGenes>();
        wanderTimer = ladGenes.GetSpeed();
    }

    // Use this for initialization
    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        timer = wanderTimer;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (ladGenes.farmer == true)
        {
            //move
            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, 3, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
        else
        {
            //move
            if (timer >= wanderTimer)
            {
                Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
                agent.SetDestination(newPos);
                timer = 0;
            }
        }
        if (agent.velocity.magnitude > 0)
        {
            wandering = true;

        }
        else
        {
            wandering = false;
        }

        if (animator)
        {
            animator.SetBool("Wandering", wandering);
        }
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;

        randDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}

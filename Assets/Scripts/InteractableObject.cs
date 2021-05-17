using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class InteractableObject : MonoBehaviour
{
    [HideInInspector]

    public float stoppingDistance = 0.5f;
    public bool isActive = false;
    public bool isInteracting = false;

    public NavMeshAgent playerAgent;
    public GameObject Player;
    public virtual void MoveToInteract(GameObject player)
    {
        playerAgent = player.GetComponent<NavMeshAgent>();
        Player = player;
        playerAgent.destination = transform.position;
        playerAgent.stoppingDistance = stoppingDistance;
        isActive = true;
    }

    private void Update()
    {
        if (isActive && playerAgent && !isInteracting && !playerAgent.pathPending)
        {
            if (playerAgent.remainingDistance <= stoppingDistance)
            { 
                Interact();
                isInteracting = true;
            }
        } 
    }

    public virtual void Interact()
    {
    }

    public virtual void AfterInteract()
    {
        isActive = false;
    }
}

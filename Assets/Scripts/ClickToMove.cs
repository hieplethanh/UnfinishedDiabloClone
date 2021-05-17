using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickToMove : MonoBehaviour
{
    NavMeshAgent NMAgent;
    GameObject currentEnemy;
    GameObject CurrentInteractableObject;
    void Start()
    {
        NMAgent = gameObject.GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(0)) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            NMAgent.isStopped = false;
            GetCurrentObject();
        }
    }

    void GetCurrentObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit interactionInfo;

        if (Physics.Raycast(ray, out interactionInfo, Mathf.Infinity))
        {
            GameObject currentObject = interactionInfo.collider.gameObject;
            if (!currentObject)
            {
                return;
            }

            if (currentObject.tag == "Enemy")
            {
                currentEnemy = currentObject;
                if (CurrentInteractableObject && CurrentInteractableObject.tag == "NPC")
                {
                    CurrentInteractableObject.GetComponent<InteractableObject>().isActive = false;
                }
                CurrentInteractableObject = null;
            }

            if (currentObject.tag == "Item" || currentObject.tag == "NPC")
            {
                currentEnemy = null;
                CurrentInteractableObject = currentObject;
                currentObject.GetComponent<InteractableObject>().MoveToInteract(gameObject);
            }
            else
            {
                NMAgent.stoppingDistance = 0;
                NMAgent.destination = interactionInfo.point;
                if (CurrentInteractableObject && CurrentInteractableObject.tag == "NPC")
                {
                    CurrentInteractableObject.GetComponent<InteractableObject>().isActive = false;
                }
                CurrentInteractableObject = null;
            }
        }
    }
}

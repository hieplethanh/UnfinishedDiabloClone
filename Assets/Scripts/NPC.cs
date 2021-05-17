using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : InteractableObject
{
    public string[] dialogs;
    public string name;
    public override void Interact()
    {
        playerAgent.isStopped = true; //stop before too near. Social distancing.
        transform.LookAt(new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z));
        DialogSystem.Instance.AddNewDialog(dialogs, name);
    }
}

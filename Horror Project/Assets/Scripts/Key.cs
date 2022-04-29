using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        Inventory.instance.AddKey(this);
        gameObject.SetActive(false);
    }
}

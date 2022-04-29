using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour,IInteractable
{
    public void Interact()
    {
        Inventory.instance.AddItem(this);
        gameObject.SetActive(false);
    }


}

using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour,IInteractable
{
    [SerializeField] Key key;

    Animator anim;
    [SerializeField] Animator anim2;

    bool isOpen = false;
    public bool isLocked = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (Inventory.instance.keyChain.Contains(key))
        {
            isLocked = false;
        }

        if (isLocked)
        {
            Debug.Log("Locked");
            return; 
        }

        Function();
    }

    void Function()
    {
        isOpen = !isOpen;
        anim.SetBool("IsOpen",isOpen);

        if (!isOpen) return;
        anim2.SetTrigger("Handle");
    }

}

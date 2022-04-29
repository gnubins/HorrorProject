using UnityEngine;
using UnityEngine.UI;

public class HandleInteraction : MonoBehaviour
{
    public LayerMask whatIsInteractable;
    [SerializeField] Text _text;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (TryInteract(out IInteractable interactable))
                interactable.Interact();
                //Debug.Log(interactable.ToString());
        }

        if (TryInteract(out IInteractable _interactable))
        {
            _text.text = _interactable.ToString();
        }
        else
        {
            _text.text = " ";
        }


    }

    bool TryInteract(out IInteractable interactable)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out RaycastHit hit, 2f, whatIsInteractable))
        {
            return hit.transform.TryGetComponent<IInteractable>(out interactable);
        }

        interactable = null;
        return false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    private IInteractible interactible = null;
    private bool canInteract = true;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            if (interactible != null)
            {
                interactible.Interact();
            }
        }
    }

    public void EnableInteraction()
    {
        canInteract = true;
    }

    public void DisableInteraction()
    {
        canInteract = false;
    }

    public void SetInteractible(IInteractible interactibleObject)
    {
        interactible = interactibleObject;
    }
}

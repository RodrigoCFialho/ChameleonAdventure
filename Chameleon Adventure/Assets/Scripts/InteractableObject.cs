using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour, IInteractible
{
    private Color initialSpriteTransparency;

    private Color transparentSprite;

    private SpriteRenderer mySpriteRenderer = null;

    [SerializeField]
    private bool turnedOn = false;

    [SerializeField]
    private GameObject activableGameobject = null;
    private IActivable activable = null; //cannot be serialized

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        initialSpriteTransparency = mySpriteRenderer.color;
        transparentSprite = new Color(mySpriteRenderer.color.r, mySpriteRenderer.color.g, mySpriteRenderer.color.b, 0.5f);
        if (turnedOn)
        {
            mySpriteRenderer.color = transparentSprite;
        }
        else
        {
            mySpriteRenderer.color = initialSpriteTransparency;
        }
        activable = activableGameobject.GetComponent<IActivable>();
        if (activable == null)
        {
            Debug.LogWarning("Associated GameObject does not implement" +
                             " IActivable interface" + activableGameobject.name);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<InteractionSystem>().SetInteractible(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<InteractionSystem>().SetInteractible(null);
        }
    }

    public void Interact()
    {
        turnedOn = !turnedOn;
        if (turnedOn == false)
        {
            mySpriteRenderer.color = initialSpriteTransparency;
            activable.Deactivate();
        }
        else
        {
            mySpriteRenderer.color = transparentSprite;
            activable.Activate();
        }
    }
}

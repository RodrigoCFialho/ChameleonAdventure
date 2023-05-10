using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour, IActivable, IInteractible
{
    private SpriteRenderer mySpriteRenderer = null;

    private Color initialColor;

    private Color initialSpriteTransparency;

    private Color transparentSprite;

    [SerializeField]
    private bool turnedOn = false;

    private bool canCamouflage = false;

    private void Awake()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = mySpriteRenderer.color;
        initialSpriteTransparency = mySpriteRenderer.color;
        transparentSprite = new Color(mySpriteRenderer.color.r, mySpriteRenderer.color.g, 1f, 0.5f);
        if (turnedOn)
        {
            mySpriteRenderer.color = transparentSprite;
        }
        else
        {
            mySpriteRenderer.color = initialSpriteTransparency;
        }
    }

    private void ColorBlue()
    {
        GetComponent<SpriteRenderer>().color = Color.blue;
    }

    private void DisableColorBlue()
    {
        GetComponent<SpriteRenderer>().color = initialColor;
    }

    public void Activate()
    {
        ColorBlue();
        canCamouflage = true;
    }

    public void Deactivate()
    {
        DisableColorBlue();
        canCamouflage = false;
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
        if (canCamouflage)
        {
            turnedOn = !turnedOn;

            Locomotion locomotion = FindObjectOfType<Locomotion>();

            Camouflage camouflage = FindObjectOfType<Camouflage>();

            if (turnedOn == false)
            {
                mySpriteRenderer.color = initialSpriteTransparency;
                locomotion.EnableLocomotion();
            }
            else
            {
                mySpriteRenderer.color = transparentSprite;
                locomotion.DisableLocomotion();
                camouflage.EnableCamouflage(transform);
            }
        }
    }
}

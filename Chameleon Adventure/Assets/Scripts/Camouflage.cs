using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camouflage : MonoBehaviour
{
    private SpriteRenderer mySpriteRenderer;

    [SerializeField]
    private SpriteRenderer boxSprite;

    public void EnableCamouflage(Transform box)
    {
        transform.position = box.position;
        mySpriteRenderer = boxSprite;
    }
}

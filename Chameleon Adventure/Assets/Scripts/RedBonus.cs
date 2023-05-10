using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ColorRed();
            Dismiss();
        }
    }

    public void Dismiss()
    {
        Destroy(gameObject);
    }
}

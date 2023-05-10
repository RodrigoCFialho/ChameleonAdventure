using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueBonus : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerController>().ColorBlue();
            Dismiss();
        }
    }

    public void Dismiss()
    {
        Destroy(gameObject);
    }
}

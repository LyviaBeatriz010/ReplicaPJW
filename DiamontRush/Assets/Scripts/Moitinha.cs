using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moitinha : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

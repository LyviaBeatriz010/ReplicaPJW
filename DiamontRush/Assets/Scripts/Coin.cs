using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int Valordamoeda;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
          GameController.instance.UpdateScore(Valordamoeda);
          Destroy(gameObject);
        }
    }
}

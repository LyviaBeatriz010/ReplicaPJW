using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;

public class Enemy : MonoBehaviour
{
  public float velocidadeSubida = 2f;
    public float velocidadeDescida = 2f;
    public float alturaMaxima = 5f; 
    public Vector3 posicaoInicial;

    private bool subindo = true;

    void Start()
    {
        posicaoInicial = transform.position; 
    }

    void Update()
    {
        if (subindo)
        {
            transform.Translate(Vector3.up * velocidadeSubida * Time.deltaTime);

            
            if (transform.position.y >= posicaoInicial.y + alturaMaxima)
            {
                subindo = false;
            }
        }
        else
        {
            transform.Translate(Vector3.down * velocidadeDescida * Time.deltaTime);

            
            if (transform.position.y <= posicaoInicial.y)
            {
                subindo = true;
            }
        }
    }
}

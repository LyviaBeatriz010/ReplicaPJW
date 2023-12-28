using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] healthImages;

    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < healthImages.Length; i++)
        {
            if (i < currentHealth)
            {
                healthImages[i].gameObject.SetActive(true); 
            }
            else
            {
                healthImages[i].gameObject.SetActive(false); 
            }
        }
    }
}

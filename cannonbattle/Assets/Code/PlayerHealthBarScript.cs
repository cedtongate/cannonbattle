using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Unity.Mathematics;

public class PlayerHealthBarScript : MonoBehaviour
{
    // Start is called before the first frame update
    public Image healthBarSprite;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void updateHealthBar(float maxHealth, float currentHealth){
        healthBarSprite.fillAmount = currentHealth / maxHealth;
    }
}

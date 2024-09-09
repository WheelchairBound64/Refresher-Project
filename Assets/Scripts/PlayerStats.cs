using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] Stat maxHealth;
    [SerializeField] Stat currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth.amount = maxHealth.amount;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth.amount <= 0) 
        {
            currentHealth.amount = maxHealth.amount;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Respawn")
        {
            currentHealth.amount = maxHealth.amount;
        }
    }
}

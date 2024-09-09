using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGas : MonoBehaviour
{
    [SerializeField] Stat health;

    private bool isTriggered;

    public void OnTriggerEnter(Collider collision)
    {
        isTriggered = true;
        StartCoroutine(DrainHealth());
    }

    public void OnTriggerExit(Collider collision)
    {
        isTriggered = false;
        StopCoroutine(DrainHealth());
    }

    IEnumerator DrainHealth()
    {
        yield return new WaitForSeconds(2.0f);
        while (health.amount > 0 && isTriggered == true)
        {
            health.amount -= 33;

            yield return new WaitForSeconds(2.0f);
        }
    }
}

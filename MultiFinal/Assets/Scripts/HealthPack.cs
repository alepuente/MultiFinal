using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealthPack : MonoBehaviour
{
    public float healthAmount;
    void OnTriggerEnter(Collider collision)
    {
        HealthController hit = collision.gameObject.GetComponent<HealthController>();
        if (hit != null)
        {
            hit.GetDamage(-healthAmount);
            gameObject.SetActive(false);
        }
    }
}

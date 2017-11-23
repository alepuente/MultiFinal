using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        HealthController hitLocal = collision.gameObject.GetComponent<HealthController>();
        if (hitLocal != null)
        {
            hitLocal.GetDamage(100f, "Death Zone");
        }
    }
}

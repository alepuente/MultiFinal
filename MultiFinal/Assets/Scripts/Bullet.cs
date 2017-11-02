using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float _speed;
    public float _damage;
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 3f);
        GetComponent<Rigidbody>().velocity = transform.forward * _speed;
    }
	
	// Update is called once per frame
	void Update () {
	}

    private void OnCollisionEnter(Collision collision)
    {
        HealthController aux = collision.gameObject.GetComponent<HealthController>();
        if (aux != null)
        {
            aux.GetDamage(_damage);
        }
        Destroy(gameObject);
    }

}

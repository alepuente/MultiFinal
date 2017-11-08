using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float _speed;
    public float _damage;
    public float _radius;
    public float _power;
    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 3f);
        GetComponent<Rigidbody>().velocity = transform.forward * _speed;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 explosionPos = collision.contacts[0].point;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, _radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(_power, explosionPos, _radius, 3.0F,ForceMode.Impulse);
            HealthController aux = hit.gameObject.GetComponent<HealthController>();
            if (aux != null)
            {
                aux.GetDamage(_damage);
            }
        }
        Destroy(gameObject);


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour
{

    public float _speed;
    public float _damage;
    public float _radius;
    public float _power;
    public GameObject _explotionPrefab;
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
        HealthController hitLocal = collision.gameObject.GetComponent<HealthController>();
        if (hitLocal != null)
        {
            hitLocal.GetDamage(_damage); 
        }
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(_power, explosionPos, _radius, 3.0F,ForceMode.Impulse);
            HealthController areaHit = hit.gameObject.GetComponent<HealthController>();
            if (areaHit != null && areaHit != hitLocal)
            {
                areaHit.GetDamage(_damage);
            }
        }
        GameObject aux2 = Instantiate(_explotionPrefab, explosionPos, Quaternion.identity);
        Destroy(aux2, 1.1f);
        Destroy(gameObject);


    }

}

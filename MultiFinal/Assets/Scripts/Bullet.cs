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
    public string _name = "";

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

   /* [ClientRpc]
    void RpcExplotion(Vector3 explosionPos, GameObject hitObject)
    {
        HealthController hitLocal = null;
        if (hitObject != null)
        {
             hitLocal = hitObject.GetComponent<HealthController>();
        }
        if (hitLocal != null)
        {
            hitLocal.GetDamage(_damage);
        }
        Collider[] colliders = Physics.OverlapSphere(explosionPos, _radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(_power, explosionPos, _radius, 3.0F, ForceMode.Impulse);
            HealthController areaHit = hit.gameObject.GetComponent<HealthController>();
            if (areaHit != null && areaHit != hitLocal)
            {
                areaHit.GetDamage(_damage);
            }
        }
        GameObject aux2 = Instantiate(_explotionPrefab, explosionPos, Quaternion.identity);
        Destroy(aux2, 1.1f);
        Destroy(gameObject);
    }*/

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 explosionPos = collision.contacts[0].point;
        HealthController hitLocal = collision.gameObject.GetComponent<HealthController>();
        if (hitLocal != null)
        {
            hitLocal.GetDamage(_damage, name);
        }
        Collider[] colliders = Physics.OverlapSphere(explosionPos, _radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(_power, explosionPos, _radius, 3.0F, ForceMode.Impulse);
            HealthController areaHit = hit.gameObject.GetComponent<HealthController>();
            if (areaHit != null && areaHit != hitLocal)
            {
                areaHit.GetDamage(_damage, _name);
            }
        }
        GameObject aux2 = Instantiate(_explotionPrefab, explosionPos, Quaternion.identity);
        Destroy(aux2, 1.1f);
        Destroy(gameObject);
       // RpcExplotion(explosionPos, collision.gameObject);
    }

}

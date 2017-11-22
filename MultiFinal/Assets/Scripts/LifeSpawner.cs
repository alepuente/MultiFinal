using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LifeSpawner : NetworkBehaviour
{
    public float spawnTime;
    private float timer;
    public GameObject healthPack;
    public float healthAmount;
    public HealthPack _healthpack;

    void Start()
    {
        _healthpack.healthAmount = healthAmount;
    }

    void Update()
    {
        if (!isServer)
        {
            return;
        }
        if (!_healthpack.gameObject.activeInHierarchy)
        {
            timer += Time.deltaTime;
            if (timer >= spawnTime)
            {
                RpcSpawnHealth();
                timer = 0;
            }
        }
    }

    [ClientRpc]
    private void RpcSpawnHealth()
    {
        _healthpack.gameObject.SetActive(true);
    }


}

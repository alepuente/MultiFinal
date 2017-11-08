
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HealthController : NetworkBehaviour
{

    public const float _maxHP = 100;
    [SyncVar(hook = "UpdateHealthBar")]
    private float _hp = _maxHP;

    //public GameObject healthPrefab;
    //private Canvas canvas;
    private float healthPanelOffset = 0.35f;
    public Slider healthSlider;
    private GameObject healthPanel;
    public Slider fuelSlider;

    void Start()
    {
       // SpawnHealthBar();
    }

    // Update is called once per frame
    void Update()
    {
        updateUI();
    }

    void updateUI()
    {
        healthSlider.value = _hp / _maxHP;
    }

    void UpdateHealthBar(float hp)
    {
        _hp = hp;
    }

    public void SpawnHealthBar()
    {
        //healthPanel = Instantiate(healthPrefab) as GameObject;
        //healthSlider = healthPanel.GetComponentInChildren<Slider>();
    }

    public void GetDamage(float damage)
    {
        if (!isServer)
        {
            return;
        }
        
            _hp -= damage;
            if (_hp <= 0)
            {
                RpcRespawn();
            }

        
    }

    [ClientRpc]
    private void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            _hp = _maxHP;
            Vector3 aux = NetworkManager.singleton.startPositions[Random.Range(0, NetworkManager.singleton.startPositions.Count)].transform.position;
            Vector2 aux2 = Random.insideUnitCircle * 5;
            transform.position = aux + new Vector3(aux2.x, 0, aux2.y);
        }
    }
}

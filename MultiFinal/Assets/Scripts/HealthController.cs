
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HealthController : NetworkBehaviour {

    public const float _maxHP = 100;
    [SyncVar(hook = "UpdateHealthBar")]
    private float _hp = _maxHP;

    public GameObject healthPrefab;
    private Canvas canvas;
    private float healthPanelOffset = 0.35f;
    private Slider healthSlider;
    private GameObject healthPanel;

    void Start() {
        SpawnHealthBar();
    }

    // Update is called once per frame
    void Update() {
        updateUI();
    }

    void updateUI()
    {
        healthSlider.value = _hp / _maxHP;
    }
   
    void UpdateHealthBar(float hp)
    {
        _hp = hp;
        /* Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
         Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
         healthPanel.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);*/
    }
    
    public void SpawnHealthBar()
    {
        // canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        healthPanel = Instantiate(healthPrefab) as GameObject;
        //healthPanel.transform.SetParent(canvas.transform, false);
        healthSlider = healthPanel.GetComponentInChildren<Slider>();
        /*Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        healthPanel.transform.position = new Vector3(Screen.width / 2, Screen.height - healthPanel.GetComponent<RectTransform>().rect.height, 0);*/
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
            transform.position = aux + new Vector3(aux2.x,0,aux2.y);
        }
    }
}

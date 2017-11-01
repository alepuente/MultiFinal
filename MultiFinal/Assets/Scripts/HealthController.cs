using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HealthController : NetworkBehaviour {

    [SyncVar]
    private float _hp;
    public float _maxHP;

    public GameObject healthPrefab;
    private Canvas canvas;
    private float healthPanelOffset = 0.35f;
    private Slider healthSlider;
    private GameObject healthPanel;

    void Start () {
        CmdSpawnHealthBar();
    }
	
	// Update is called once per frame
	void Update () {

        CmdUpdateHealthBar();
    }

    [Command]
    void CmdUpdateHealthBar()
    {
        healthSlider.value = _hp / _maxHP;
        /* Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
         Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
         healthPanel.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);*/
    }

    [Command]
    public void CmdSpawnHealthBar()
    {
        _hp = _maxHP;
        // canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        healthPanel = Instantiate(healthPrefab) as GameObject;
        //healthPanel.transform.SetParent(canvas.transform, false);
        healthSlider = healthPanel.GetComponentInChildren<Slider>();
        /*Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        healthPanel.transform.position = new Vector3(Screen.width / 2, Screen.height - healthPanel.GetComponent<RectTransform>().rect.height, 0);*/
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            if (!isServer)
                return;
            _hp -= 10;
            if (_hp <= 0)
            {
                RpcRespawn();
            }
        }
    }

    [ClientRpc]
    void RpcRespawn()
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

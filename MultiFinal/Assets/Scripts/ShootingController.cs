using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ShootingController : NetworkBehaviour {

    public GameObject _bullet;
    private float _hp;
    public float _maxHP;
    
    public Canvas canvas;
    public GameObject healthPrefab;
    public float healthPanelOffset = 0.35f;
    private Slider healthSlider;
    public GameObject healthPanel;
    // Use this for initialization
    void Start () {
        _hp = _maxHP;
        if (!isLocalPlayer)
        {
            return;
        }
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        healthPanel = Instantiate(healthPrefab) as GameObject;
        healthPanel.transform.SetParent(canvas.transform, false);
        healthSlider = healthPanel.GetComponentInChildren<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            CmdFire();
        }

        healthSlider.value = _hp / _maxHP;
        Vector3 worldPos = new Vector3(transform.position.x, transform.position.y + healthPanelOffset, transform.position.z);
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);
        healthPanel.transform.position = new Vector3(screenPos.x, screenPos.y, screenPos.z);

    }

    [Command]
    void CmdFire()
    {      
        NetworkServer.Spawn(Instantiate(_bullet, transform.position + new Vector3(0f, 0f, 1f), transform.rotation));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            Destroy(collision.gameObject);
            _hp -= 10;
            if (_hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

}

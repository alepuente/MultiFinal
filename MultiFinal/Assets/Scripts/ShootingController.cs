using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ShootingController : NetworkBehaviour
{
    public GameObject _bullet;
    public GameObject _gun;
    public float _bulletDistance;
    [SyncVar]
    public string _playerName = "";

    void Start()
    {
        if (isLocalPlayer)
        {
            if (FindObjectOfType<InputField>().text != string.Empty)
            {
            CmdChangeName(FindObjectOfType<InputField>().text);
            }
            else
            {
            CmdChangeName("Unknown");
            }
                FindObjectOfType<InputField>().gameObject.SetActive(false);
        }
        
    }

    [Command]
    public void CmdChangeName(string newName)
    {
        RpcChangeName(newName);
    }
    [ClientRpc]
    public void RpcChangeName(string newName)
    {
        _playerName = newName;
    }

    void Update()
    {
        this.GetComponentInChildren<TextMesh>().text = _playerName;
        if (Input.GetMouseButtonDown(0))
        {
            CmdFire();
        }
    }

    [Command]
    void CmdFire()
    {
        Bullet aux = Instantiate(_bullet, _gun.transform.position + (_gun.transform.forward * _bulletDistance), _gun.transform.rotation).GetComponent<Bullet>();
        aux._name = _playerName;
        NetworkServer.Spawn(aux.gameObject);
    }

}

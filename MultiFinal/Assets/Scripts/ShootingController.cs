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
    [SyncVar(hook = "CmdChangeName")]
    public string _playerName = "";
    public float _shootingSpeed;
    private float timer;
    private int bullets;
    public int _maxBullets;
    public Text bulletsText;
    public float reloadTime;
    public TextMesh nameTag;

    void Start()
    {
        /*if (FindObjectOfType<InputField>().text != string.Empty)
        {
            GetComponent<ShootingController>().CmdChangeName(FindObjectOfType<InputField>().text);
        }
        else
        {
            GetComponent<ShootingController>().CmdChangeName("Unknown");
        }*/
        timer = _shootingSpeed;
        bullets = _maxBullets;
    }



    void Reload()
    {
        bullets = _maxBullets;
        timer = -reloadTime;
    }


    //[Command]
    public void CmdChangeName(string newName)
    {
        //RpcChangeName(newName);
        _playerName = newName;
    }
    [ClientRpc]
    public void RpcChangeName(string newName)
    {
        _playerName = newName;
    }

    void Update()
    {
        timer += Time.deltaTime;
        bulletsText.text = bullets + "/" + _maxBullets;
        nameTag.text = _playerName;
        if (Input.GetMouseButton(0) && timer >= _shootingSpeed)
        {
            CmdFire();
            bullets--;
            timer = 0f;
            if (bullets <= 0)
            {
                Reload();
            }
        }
        if (Input.GetKey(KeyCode.R))
        {
            Reload();
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

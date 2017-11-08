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
    public string _playerName;


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CmdFire();
        }
    } 

    [Command]
    void CmdFire()
    {
        Bullet aux = Instantiate(_bullet, _gun.transform.position + (_gun.transform.forward * _bulletDistance), _gun.transform.rotation).GetComponent<Bullet>();
        NetworkServer.Spawn(aux.gameObject);
    }

}

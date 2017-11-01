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
        NetworkServer.Spawn(Instantiate(_bullet, _gun.transform.position + (_gun.transform.forward * _bulletDistance), _gun.transform.rotation));
    }

}

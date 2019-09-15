using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Shooting : MonoBehaviourPunCallbacks
{
    public Scriptable[] WeaponInformations;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            photonView.RPC("CreateBullets", RpcTarget.All, gameObject.GetComponent<Rigidbody>().rotation);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            photonView.RPC("ChangeTransform", RpcTarget.All, 3f);
        }
    }

    [PunRPC]
    public void CreateBullets(Quaternion quaternion)
    {
        GameObject go = Instantiate(WeaponInformations[1].bulletType, transform.position, Quaternion.identity);
        Vector3 bulletDirection = quaternion * Vector3.forward;
        go.transform.forward = bulletDirection;
        go.GetComponent<Rigidbody>().velocity = bulletDirection * 20f;
        
        
        Destroy(go, 5f);
    }

    [PunRPC]
    public void ChangeTransform(float x)
    {
        gameObject.transform.position = new Vector3(0f, 1f, 0f);
    }
}

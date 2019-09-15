using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PickUpObjects : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        photonView.RPC("equalName", RpcTarget.AllBufferedViaServer, this.name, this.tag);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [PunRPC]
    public void OnPickUps(string inventory)
    {
        transform.parent = GameObject.Find(inventory).transform.GetChild(1).transform;

        gameObject.GetComponent<MeshRenderer>().enabled = false;

        //gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }

    [PunRPC]
    public void realesePickUps(Vector3 place)
    {
        this.gameObject.transform.position = place;
        
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;

        this.gameObject.GetComponent<CapsuleCollider>().enabled = true;

        transform.parent = null;
    }

    [PunRPC]
    public void equalName(string name,string tag)
    {
        transform.name = name;
        transform.tag = tag;
    }
}

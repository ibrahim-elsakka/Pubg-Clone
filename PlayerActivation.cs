using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerActivation : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (!photonView.IsMine)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Find("Main Camera").SetActive(false);
            Debug.Log(photonView.Owner.NickName);

            photonView.RPC("TakeYourOwnCharacter", RpcTarget.AllBufferedViaServer, photonView.Owner.NickName);


            

        }
    }

    [PunRPC]
    public void TakeYourOwnCharacter(string playerName)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        GameObject go2 = GameObject.Find(photonView.Owner.NickName);
        //sahipliğini alıyor
        go2.GetComponent<PhotonView>().RequestOwnership();
        //diğer kamerayı kapattıktan sonra bunu açıyor
        go2.transform.GetChild(0).gameObject.SetActive(true);
        //kontrolleri etkinleştiriyor
        go2.GetComponent<MovementController>().enabled = true;

        go2.GetComponent<PickUpMechanism>().enabled = true;

        go2.GetComponent<Shooting>().enabled = true;
    }

    
}

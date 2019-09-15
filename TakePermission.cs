using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class TakePermission : MonoBehaviourPunCallbacks
{
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //başlangıçta RPC kullanılmasının sebebi sceneobjectler üretildikten sonra isim değiştiğinde sadece masterda gözüküyor
        //sceneobject üretmemizin sebebi ise oyuncu disconnect olduğunda karakterinin silinmesini istemiyoruz hızlı dc and rc den sonra karakterine istediği gibi devam edebilmeli
        photonView.RPC("changeName_Tag",RpcTarget.AllBufferedViaServer,this.name,this.tag);
    }

    private void OnMouseDown()
    {
        base.photonView.RequestOwnership();
    }


    // Update is called once per frame
    void Update()
    {
        if (base.photonView.Owner == PhotonNetwork.LocalPlayer)
        {
            
        }
    }
    [PunRPC]
    public void changeName_Tag(string name,string tag)
    {
        transform.name = name;
        transform.tag = tag;
    }
}

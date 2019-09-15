using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManagerPUBG : MonoBehaviourPunCallbacks
{
    public GameObject Sphere;
    public GameObject Cube;
    public GameObject capsule;
    public GameObject RealPlayer;

    GameObject[] players;
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.Instantiate(RealPlayer.name, Vector3.zero, Quaternion.identity);

        // PhotonNetwork.InstantiateSceneObject(Sphere.name, new Vector3(0f, 4f, 0f), Quaternion.identity);
        if (PhotonNetwork.IsMasterClient)
        {
            for (int i = 0; i < 5; i++)
            {
                GameObject go = PhotonNetwork.InstantiateSceneObject(capsule.name, new Vector3(Random.Range(-10f, 10f), 1f, Random.Range(-10f, 10f)), Quaternion.identity);
                go.name = capsule.name + i;
            }
        }

        
       

        if (PhotonNetwork.IsMasterClient)
        {
            foreach (Player player in PhotonNetwork.PlayerList)
            {
                GameObject go = PhotonNetwork.InstantiateSceneObject(Cube.name, new Vector3(Random.Range(-5f, 5f), 2f, Random.Range(-5f, 5f)), Quaternion.identity);
                go.transform.name = player.NickName;
            }
        }

        foreach (Player player in PhotonNetwork.PlayerList)
        {

            

            //go.name = player.NickName;
        }




    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    
}

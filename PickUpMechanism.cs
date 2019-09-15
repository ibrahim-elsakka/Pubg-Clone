using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickUpMechanism : MonoBehaviourPunCallbacks,IDropHandler
{
    public static PickUpMechanism handle;

    public GameObject pickUpCanvas;
    //public Transform inventory;

    public GameObject pickUpButton;

    public GameObject inventory;

    public GameObject groundInventory;
    public GameObject playerInventory;

    public string characterName;

    public bool changeButtonPlace;

    public GameObject self;

    public void OnDrop(PointerEventData eventData)
    {
        RectTransform groundInventoryRect = groundInventory.gameObject.transform as RectTransform;
        RectTransform playerInventoryRect = playerInventory.gameObject.transform as RectTransform;

        if (RectTransformUtility.RectangleContainsScreenPoint(playerInventoryRect,Input.mousePosition))
        {
            Debug.Log("işe yaradı");
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        handle = this;

        changeButtonPlace = false;

        for (int i = 0; i < transform.GetChild(1).childCount; i++)
        {
            GameObject go = Instantiate(pickUpButton, Vector3.zero, Quaternion.identity) as GameObject;

            go.transform.SetParent(playerInventory.transform, false);

            go.name = transform.GetChild(1).GetChild(i).transform.name;

            go.tag = "FromPlayer";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine)
        {
            return;
        }
        

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (pickUpCanvas.gameObject.active == true)
            {
                Cursor.visible = false;
                pickUpCanvas.SetActive(false);
            }
            else
            {
                Cursor.visible = true;
                pickUpCanvas.SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            inventoryItemsListed("gg");
        }

        if (changeButtonPlace)
        {
            inventoryItemsListed(characterName);
            changeButtonPlace = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType() == typeof(SphereCollider))
        {
            
        }
        if (photonView.IsMine)
        {
            if (other.gameObject.tag == "PickUp")
            {
                    groundInventory.name = this.gameObject.name;

                    GameObject go = Instantiate(pickUpButton, Vector3.zero, Quaternion.identity);

                    go.name = other.name;

                    go.transform.SetParent(groundInventory.transform, false);

                    go.tag = "FromGround";
                
                if (other.gameObject.GetComponent<CapsuleCollider>().enabled == false)
                {
                    Destroy(groundInventory.transform.Find(other.name).gameObject);
                }
            }
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (other.gameObject.tag == "PickUp")
                {
                    other.gameObject.GetComponent<PhotonView>().RequestOwnership();

                    other.gameObject.GetComponent<PhotonView>().RPC("OnPickUps", RpcTarget.AllBufferedViaServer, this.gameObject.name);
                    
                }

            }
            else if (Input.GetKeyDown(KeyCode.G))
            {
                if (true)
                {
                   // other.gameObject.GetComponent<PhotonView>().TransferOwnership(1);

                    GameObject.Find("Capsule(Clone)").gameObject.GetComponent<PhotonView>().RPC("realesePickUps", RpcTarget.AllBufferedViaServer, transform.position);

                }
            }
        }
    }

    //bunu yazmamamızın sebebi unitynin kendi içindeki bir bugundan dolayı
    //bu bug nedir ?
    //2 kişi aynı şeyi lootlamaya çalışıyor diyelim bir tanesi diğerinden daha önce çektiği vakit..
    //ontriggerExit methodu çalışmıyor bu methodun çalışması için yürüyerek gitmesi lazım gibi bir şey
    //diğerinin colliderını kapatsak dahi yürüyerek çıkmadığı ya da uzaklaşmadığı için exit çalışmıyor bu yüzden
    //loot ekranındaki buton kaybolmuoyr...
    // bu yüzden aşağıdaki ontriggerStay sayesinde biz bunu hallediyoruz
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "PickUp")
        {
                if (other.gameObject.GetComponent<MeshRenderer>().enabled == false)
                {
                    if (groundInventory.transform.Find(other.name).gameObject != null)
                    {
                         Destroy(groundInventory.transform.Find(other.name).gameObject);
                    }
                }
        }
    }
    private void OnTriggerExit(Collider other)
    {
                if (groundInventory.transform.Find(other.name).gameObject != null)
                {
                    if (other.gameObject.tag == "PickUp")
                    {
                        Destroy(groundInventory.transform.Find(other.name).gameObject);
                    }
                }
    }

    public void pickUpButtonPressed(Button button)
    {
        if (button.gameObject.tag == "FromGround")
        {
            Debug.Log(button.gameObject.transform.parent.name);

            GameObject.Find(button.name).gameObject.GetComponent<PhotonView>().RequestOwnership();

            GameObject.Find(button.name).gameObject.GetComponent<PhotonView>().RPC("OnPickUps", RpcTarget.AllBufferedViaServer, button.gameObject.transform.parent.name);

            PickUpMechanism.handle.characterName = button.name;

            Destroy(button.gameObject);

            PickUpMechanism.handle.changeButtonPlace = true;
        }
        else if (button.gameObject.tag == "FromPlayer")
        {
            Debug.Log("transform adı==" + button.name);

            PickUpMechanism.handle.self.transform.GetChild(1).transform.Find(button.name).gameObject.GetComponent<PhotonView>().RPC("realesePickUps", RpcTarget.AllBufferedViaServer, PickUpMechanism.handle.self.transform.position);

            Destroy(button.gameObject);
        }
        
        //Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }

    public void inventoryItemsListed(string name)
    {
            GameObject go = Instantiate(pickUpButton, Vector3.zero, Quaternion.identity) as GameObject;

            go.transform.SetParent(playerInventory.transform, false);

            go.name = name;

            go.tag = "FromPlayer";

            Debug.Log("go nun ismi" + name);
    }

    
}

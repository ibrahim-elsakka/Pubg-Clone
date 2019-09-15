using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Weapons")]
public class Scriptable : ScriptableObject
{
    public string weaponName;
    public float weaponDamage;
    public GameObject bulletType;
    public float bulletSpeed;
    
}

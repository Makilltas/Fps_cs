using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance;

    public GameObject damageText;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void DisplayDamage(int damage, Vector3 position)
    {
        var obj = Instantiate(damageText,position,Quaternion.identity);

        obj.GetComponent<DamageText>().ChangeText(damage);
    }
   
}

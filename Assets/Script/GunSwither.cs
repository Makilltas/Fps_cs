using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwither : MonoBehaviour
{
    public GameObject[] guns;

    private int index = 0;
    
    void Start()
    {
        SwitchGun(0);
    }

    
    void Update()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");
        if (scroll > 0)
        {
            SwitchGun((index + 1 ) % guns.Length);
        }
        else if (scroll < 0)
        {
            SwitchGun((index - 1 + guns.Length) % guns.Length);
        }
    }

    void SwitchGun(int newIndex)
    {
        guns[index].SetActive(false);
        index = newIndex;
        guns[index].SetActive(true);
    }
}

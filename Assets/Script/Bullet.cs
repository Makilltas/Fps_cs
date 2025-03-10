using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 40;
    public float LifeTime = 3;
    public Vector2 damageRange = new Vector2 (10, 20);

    void Start()
    {
        Destroy(gameObject, LifeTime);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision other)
    {
        //print($"Hit: {other.gameObject.name} for {Random.Range(damageRange.x, damageRange.y)}");
        var damage = Random.Range(damageRange.x, damageRange.y);
        DamageManager.instance.DisplayDamage((int)damage,transform.position);

        var ninja = other.gameObject.GetComponent<Ninja>();
        if (ninja != null) ninja.GetHurt();

        Destroy(gameObject);
    }
}

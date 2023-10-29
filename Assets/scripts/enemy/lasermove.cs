using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lasermove : MonoBehaviour
{
    [SerializeField] float Speed;
    [SerializeField] float Damage;
    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * Speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "destroylaser")
        {
            Destroy(this.gameObject);
        }

        if (other.gameObject.tag == "player")
        {
            screenmovement player = other.gameObject.GetComponent<screenmovement>();
            player.TakeDamage(Damage);
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyai : MonoBehaviour
{
    [SerializeField] float ShotCooldown;
    [SerializeField] GameObject playertransform;
    [SerializeField] float firerange;
    bool IsAggro = false;
    float distancetoplayer;
    [SerializeField] GameObject LASER;

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, firerange);
    }

    // Start is called before the first frame update
    void Start()
    {
        playertransform = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        distancetoplayer = Vector2.Distance(playertransform.transform.position, transform.position);
        if (distancetoplayer <= firerange && !IsAggro)
        {
            StartCoroutine(ShootTarget());
        }
        else if (distancetoplayer > firerange)
        {
            IsAggro = false;
            StopCoroutine(ShootTarget());
        }
    }

    IEnumerator ShootTarget()
    {
        IsAggro = true;
        while (distancetoplayer <= firerange)
        {
            GameObject laser = Instantiate(LASER, transform.position, Quaternion.identity);
            laser.transform.LookAt(playertransform.transform.position);
            yield return new WaitForSeconds(ShotCooldown);
        }
    }
}

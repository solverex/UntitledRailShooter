using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class weapon : MonoBehaviour
{
    [SerializeField] float Damage;
    [SerializeField] float Cooldown;
    [SerializeField] ParticleSystem MuzzleFlash;
    [SerializeField] ParticleSystem Impact;
    [SerializeField] LayerMask layermask;

    [SerializeField] InputAction shootbutton;

    [SerializeField] bool CanShoot;

    [SerializeField] bool isExplosive;



    void OnEnable()
    {
        shootbutton.Enable();
        CanShoot = true;
    }

    void OnDisable()
    {
        shootbutton.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        CanShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        float shootinput = shootbutton.ReadValue<float>();
        Ray rotateray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(rotateray, out hit, 100, layermask))
        {
            Vector3 pos = hit.point;
            transform.LookAt(pos);

            if (shootinput > 0 && CanShoot)
            {
                StartCoroutine(Fire());
                ParticleSystem impactvfx = Instantiate(Impact, hit.point, Quaternion.identity);
                impactvfx.Play();
                if (hit.transform.tag == "enemy")
                {
                    enemyhp enemyscript = hit.transform.GetComponent<enemyhp>();
                    enemyscript.TakeDamage(Damage);
                }

                if (hit.transform.tag == "boss")
                {
                    weakpointhealth bossscript = hit.transform.GetComponent<weakpointhealth>();
                    bossscript.TakeDamage(Damage);
                }
            
            }
        }
    }

    IEnumerator Fire()
    {
        CanShoot = false;
        MuzzleFlash.Play();
        yield return new WaitForSeconds(Cooldown);
        CanShoot = true;
        StopCoroutine(Fire());
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class seeker : MonoBehaviour
{
    [SerializeField] InputAction SeekerInput;

    [SerializeField] List<GameObject> SeekerMissiles;
    [SerializeField] List<GameObject> TargetedEnemy;
    [SerializeField] GameObject TargetUI;

    [SerializeField] bool CanShoot;
    [SerializeField] bool isCharging;

    [SerializeField] float NumberOfTargets;
    [SerializeField] float SeekerChargeTimer;
    [SerializeField] float Cooldown;

    [SerializeField] LayerMask layermask;

    private float ChargeHold;
    private float nexttargettimer;
    private int MissilesFired;
    
    void OnEnable()
    {
        CanShoot = true;
        ChargeHold = 0;
        MissilesFired = 0;
        SeekerInput.Enable();
    }

    void OnDisable()
    {
        SeekerInput.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        SeekerCharge();
    }

    void SeekerCharge()
    {
        float recievedinputs = SeekerInput.ReadValue<float>();

        Ray rotateray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(rotateray, out hit, 100, layermask))
        {
            Vector3 pos = hit.point;
            transform.LookAt(pos);
        }

        if (recievedinputs > 0 && CanShoot)
        {
            isCharging = true;
        }
        else
        {
            isCharging = false;
        }

        if (isCharging)
        {
            ChargeHold += Time.deltaTime;
            Debug.Log(ChargeHold);
            if (ChargeHold > SeekerChargeTimer)
            {
                TargetsLaunch();
            }
        }

        if (!isCharging)
        {
            ChargeHold = 0;
        }

        if (NumberOfTargets > 0 && !isCharging && CanShoot)
        {
            StartCoroutine(FireMissiles());
        }
    }

    void TargetsLaunch()
    {
        nexttargettimer -= Time.deltaTime;
        Ray rotateray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(rotateray, out hit, 100, layermask))
        {
            if (hit.transform.tag == "enemy" && nexttargettimer < 0 && NumberOfTargets < 5)
            {
                TargetedEnemy.Add(hit.transform.gameObject);
                NumberOfTargets += 1f;
                nexttargettimer = 1f;
            }
        }
    }

    IEnumerator FireMissiles()
    {
        bool CanShoot = false;
        Debug.Log("hmm");
        if (MissilesFired < NumberOfTargets)
        {
            SeekerMissiles[MissilesFired].transform.position = transform.position;
            MissilesFired += 1;
            StartCoroutine(FireMissiles());
        }
        if (MissilesFired == NumberOfTargets)
        {
            CanShoot = true;
            TargetedEnemy.Clear();
            NumberOfTargets = 0;
            MissilesFired = 0;
            yield return null;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossai : MonoBehaviour
{
    [SerializeField] float ShotCooldown;
    [SerializeField] GameObject playertransform;
    [SerializeField] bool BossActive;
    bool IsAggro = false;
    float distancetoplayer;
    [SerializeField] GameObject LASER;
    [SerializeField] GameObject FASTLASER;
    bool isDestroyed;

    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        playertransform = GameObject.FindWithTag("player");
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 115f)
        {
            BossActive = true;
        }
        distancetoplayer = Vector3.Distance(playertransform.transform.position, transform.position);
        if (!IsAggro && BossActive)
        {
            StartCoroutine(ShootTarget());
        }
    }

    IEnumerator ShootTarget()
    {
        IsAggro = true;
        if (!isDestroyed)
        {
            GameObject laser = Instantiate(LASER, transform.position, Quaternion.identity);
            laser.transform.LookAt(playertransform.transform.position);
            yield return new WaitForSeconds(ShotCooldown);
            StartCoroutine(ShootTarget());
        }
        if (isDestroyed)
        {
            GameObject laser = Instantiate(FASTLASER, transform.position, Quaternion.identity);
            laser.transform.LookAt(playertransform.transform.position);
            yield return new WaitForSeconds(ShotCooldown);
            StartCoroutine(ShootTarget());
        }
    }

    public void WeakPointDown()
    {
        ShotCooldown = 0.25f;
        isDestroyed = true;
    }
}

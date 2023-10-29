using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weakpointhealth : MonoBehaviour
{
    [Header("Statistics")]
    public float health;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] float IFrames;
    [SerializeField] float defeatedscore;
    float iframeatstart;
    private bool isDead;
    [SerializeField] float explodedamage;
    [SerializeField] bossai boss;
    // Start is called before the first frame update
    void Start()
    {
        iframeatstart = IFrames;
    }

    // Update is called once per frame
    void Update()
    {
        IFrames -= Time.deltaTime;

        if (health == 0 && !isDead)
        {
            isDead = true;
            UIManager playerui = FindObjectOfType<UIManager>();
            playerui.AddScore(defeatedscore);
            ParticleSystem explode = Instantiate(explosion, transform.position, Quaternion.identity);
            explode.Play();
            boss.WeakPointDown();
            transform.localScale = new Vector3(0.1f, 0.1f, 1);
        }

        if (health < 0)
        {
            health = 0;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "destructiveparticle")
        {
            if (IFrames < 0)
            {
                health -= explodedamage;
                IFrames = 2 * iframeatstart;
            }
        }
    }

    public void TakeDamage(float Damage)
    {
        Debug.Log("hit");
        if (IFrames < 0)
        {
            health -= Damage;
            IFrames = iframeatstart;
        }
    }
}

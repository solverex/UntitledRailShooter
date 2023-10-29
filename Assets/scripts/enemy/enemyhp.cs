using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhp : MonoBehaviour
{
    [Header("Statistics")]
    [SerializeField] float health;
    [SerializeField] ParticleSystem explosion;
    [SerializeField] float IFrames;
    [SerializeField] float defeatedscore;
    float iframeatstart;
    [SerializeField] float explodedamage;
    // Start is called before the first frame update
    void Start()
    {
        iframeatstart = IFrames;
    }

    // Update is called once per frame
    void Update()
    {
        IFrames -= Time.deltaTime;

        if (health <= 0)
        {
            UIManager playerui = FindObjectOfType<UIManager>();
            playerui.AddScore(defeatedscore);
            ParticleSystem explode = Instantiate(explosion, transform.position, Quaternion.identity);
            explode.Play();
            Destroy(this.gameObject);
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

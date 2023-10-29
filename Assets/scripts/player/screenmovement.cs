using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class screenmovement : MonoBehaviour
{
    [SerializeField] InputAction playerinput;
    [SerializeField] InputAction dodgeinput;

    [Header ("Statistics")]
    [SerializeField] float playerspeed;
    [SerializeField] float dodgedistance;
    [SerializeField] float healthcurrent;
    [SerializeField] float maxhealth;
    [SerializeField] float Iframes;
    [SerializeField] float DodgeCooldown;
    [SerializeField] float DodgeTime;

    private bool isInvincible;
    private bool isDodging;
    private bool DodgeButtonHeld;
    private bool isDead;

    private float refinedinputx;
    private float refinedinputy;
    private float recievedinputd;
    private float Xpos;

    private Vector3 velocity = Vector3.zero;

    [Header ("Boundaries")]
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    [SerializeField] Animator anim;
    [SerializeField] ParticleSystem Deathexp;
    [SerializeField] GameObject needtodeactivate;
    [SerializeField] Vector3 offset;
    [SerializeField] UIManager playerui;

    void OnEnable()
    {
        playerinput.Enable();
        dodgeinput.Enable();
    }

    void OnDisable()
    {
        playerinput.Disable();
        dodgeinput.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDodging && !isDead)
        {
            playermove();
        }
        if (!isDead)
        {
            playerdodge();
        }
        ani();
        Iframes -= Time.deltaTime;
        playerui.HealthCheck(healthcurrent);
    }

    void playermove()
    {
        float recievedinputx = playerinput.ReadValue<Vector2>().x;
        float recievedinputy = playerinput.ReadValue<Vector2>().y;

        if (recievedinputx > 0)
        {
            refinedinputx += playerspeed * Time.deltaTime;
        }
        else if (recievedinputx < 0)
        {
            refinedinputx -= playerspeed * Time.deltaTime;
        }

        if (recievedinputy > 0)
        {
            refinedinputy += playerspeed * Time.deltaTime;
        }
        else if (recievedinputy < 0)
        {
            refinedinputy -= playerspeed * Time.deltaTime;
        }

        refinedinputx = Mathf.Clamp(refinedinputx, minX, maxX);
        refinedinputy = Mathf.Clamp(refinedinputy, minY, maxY);
        transform.localPosition = new Vector3(refinedinputx, refinedinputy, 0);
    }

    void playerdodge()
    {
        recievedinputd = dodgeinput.ReadValue<float>();
        if (recievedinputd != 0 && !isDodging && !DodgeButtonHeld)
        {
            Xpos = transform.position.x + (recievedinputd * dodgedistance);
            DodgeButtonHeld = true;
            isDodging = true;
        }

        if (recievedinputd == 0 && !isDodging)
        {
            DodgeButtonHeld = false;
        }

        if (isDodging)
        {
            Xpos = Mathf.Clamp(Xpos, minX, maxX);
            refinedinputx = Xpos;
            Vector3 newpos = new Vector3(Xpos, transform.position.y, transform.position.z) ;
            transform.position = Vector3.SmoothDamp(transform.position, newpos, ref velocity, DodgeTime);
            Invoke("DisableDash", DodgeCooldown);
        }
    }

    void DisableDash()
    {
        transform.rotation = Quaternion.Euler(0, 0, 0);
        anim.SetBool("LeftDash", false);
        anim.SetBool("RightDash", false);
        isDodging = false;
    }

    void ani()
    {
        if (recievedinputd < 0 && isDodging)
        {
            anim.SetBool("LeftDash", true);
        }

        if (recievedinputd > 0 && isDodging)
        {
            anim.SetBool("RightDash", true);
        }


    }

    public void TakeDamage(float Damage)
    {
        if (Iframes < 0)
        {
            anim.SetBool("IsHit", true);
            healthcurrent -= Damage;
            Iframes = 0.5f;
            Invoke("anicheck", 0.1f);
        }

        if (healthcurrent <= 0 && !isDead)
        {
            isDead = true;
            needtodeactivate.SetActive(false);
            anim.SetBool("Death", true);
            StartCoroutine(DeathExplosion());
        }
    }

    void anicheck()
    {
        anim.SetBool("IsHit", false);
    }
    
    IEnumerator DeathExplosion()
    {
        yield return new WaitForSeconds(1f);
        ParticleSystem Ded = Instantiate(Deathexp, transform.position + offset, Quaternion.identity);
        transform.position = new Vector3(0, -100, 0);
        Ded.Play();
        yield return new WaitForSeconds(5f);
        Application.Quit();
    }

}

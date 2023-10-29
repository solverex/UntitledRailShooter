using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] float savedhealth;
    [SerializeField] float maxhealth;
    [SerializeField] float score;

    [SerializeField] TMP_Text healthtext;
    [SerializeField] TMP_Text scoretext;

    [SerializeField] GameObject HealthUI;

    [SerializeField] GameObject TargetUI;

    [SerializeField] GameObject BossUI;

    [SerializeField] weakpointhealth one;
    [SerializeField] weakpointhealth two;
    [SerializeField] weakpointhealth three;
    [SerializeField] weakpointhealth four;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetUI.transform.position = Input.mousePosition;
        healthtext.text = savedhealth.ToString();
        float bossremaininghealth = Mathf.Clamp(((one.health + two.health + three.health + four.health) / 1000) * 1.28f, 0, 1.28f);
        float remaininghealth = Mathf.Clamp((savedhealth) / maxhealth, 0, 1f);
        HealthUI.transform.localScale = new Vector3(remaininghealth, 1, 1);
        BossUI.transform.localScale = new Vector3(bossremaininghealth, 1.4f, 1);
        scoretext.text = "Score: " + score;
    }

    public void HealthCheck(float healthcurrent)
    {
        savedhealth = healthcurrent;
    }

    public void AddScore(float defeatedscore)
    {
        score += defeatedscore;
    }
}

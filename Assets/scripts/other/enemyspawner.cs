using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyspawner : MonoBehaviour
{
    [SerializeField] GameObject smallenemy;
    [SerializeField] GameObject normalenemy;
    [SerializeField] GameObject bigenemy;

    [SerializeField] float enemyspawncooldown;
    [SerializeField] GameObject player;

    [SerializeField] bool end;

    private float timer = 0f;
    private bool scoreattack;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 115f && !scoreattack)
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            scoreattack = true;
            end = true;
        }

        if (end)
        {
            StartCoroutine(spawnsmallenemy());
            StartCoroutine(spawnnormalenemy());
            StartCoroutine(spawnhardenemy());
            end = false;
        }
    }

    IEnumerator spawnsmallenemy()
    {
        float Xpos = Random.Range(-9f, 9f);
        float Ypos = Random.Range(-5f, 5f);
        float Zpos = Random.Range(9f, 28f);
        Vector3 finalPos = player.transform.position + new Vector3(Xpos, Ypos, Zpos);
        Instantiate(smallenemy, finalPos, Quaternion.identity);
        yield return new WaitForSeconds(enemyspawncooldown);
        StartCoroutine(spawnsmallenemy());
    }

    IEnumerator spawnnormalenemy()
    {
        float Xpos = Random.Range(-9f, 9f);
        float Ypos = Random.Range(-5f, 5f);
        float Zpos = Random.Range(9f, 28f);
        Vector3 finalPos = player.transform.position + new Vector3(Xpos, Ypos, Zpos);
        Instantiate(normalenemy, finalPos, Quaternion.identity);
        yield return new WaitForSeconds(enemyspawncooldown*2f);
        StartCoroutine(spawnnormalenemy());
    }

    IEnumerator spawnhardenemy()
    {
        float Xpos = Random.Range(-9f, 9f);
        float Ypos = Random.Range(-5f, 5f);
        float Zpos = Random.Range(9f, 28f);
        Vector3 finalPos = player.transform.position + new Vector3(Xpos, Ypos, Zpos);
        Instantiate(bigenemy, finalPos, Quaternion.identity);
        yield return new WaitForSeconds(enemyspawncooldown*6f);
        StartCoroutine(spawnhardenemy());
    }
}

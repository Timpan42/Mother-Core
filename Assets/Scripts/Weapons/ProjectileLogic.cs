using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public Ammo ammo { get; set; }
    private Transform enemyTarget;
    private Transform player;
    private bool activeBullet;

    private void Start()
    {
        player = transform.root.GetComponent<Transform>();
    }

    void Update()
    {
        StartCoroutine(SendHomingBullet());
    }

    private IEnumerator SendHomingBullet()
    {
        while (activeBullet)
        {
            transform.position += (enemyTarget.position - transform.position).normalized * ammo.speed * Time.deltaTime;
            transform.LookAt(enemyTarget);
            yield return null;
        }
    }

    public void FollowTarget(Transform target)
    {
        enemyTarget = target;
        activeBullet = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            OnImpact();
        }
    }
    private void OnImpact()
    {
        activeBullet = false;
        transform.position = player.position;
        transform.gameObject.SetActive(false);
    }
}

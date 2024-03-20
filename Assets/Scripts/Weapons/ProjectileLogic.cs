using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class ProjectileLogic : MonoBehaviour
{
    public Ammo ammo { get; set; }
    private Transform enemyTarget;
    private Transform parent;
    private bool activeBullet;
    private float damage;

    private void Start()
    {
        parent = transform.root.GetComponent<Transform>();
        gameObject.SetActive(false);
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

    public void FollowTarget(Transform target, float totalDamage)
    {
        enemyTarget = target;
        damage = totalDamage;
        activeBullet = true;

    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Enemy") || collider.CompareTag("Player"))
        {
            OnImpact(collider);
        }
    }
    private void OnImpact(Collider collider)
    {
        activeBullet = false;
        collider.GetComponent<HealthScript>().RemoveHealth(damage);
        transform.position = parent.position;
        transform.gameObject.SetActive(false);
    }
}

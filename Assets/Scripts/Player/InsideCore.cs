using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.InputSystem;


public class InsideCore : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private int damageTimer;
    [SerializeField] CorePower corePower;
    [SerializeField] private LayerMask layerToHit;
    [SerializeField] private Vector3 boxHalfExtents;
    private int counter = 0;
    private bool activeCore = false;
    private Collider[] hitCollider = new Collider[10];
    private int numberOfColliders;

    public void ActivateTheCore()
    {
        activeCore = !activeCore;
        if (activeCore)
        {
            corePower.ActivateCore(activeCore);
            StartCoroutine(CoreOperation());
        }
        else
        {
            corePower.ActivateCore(activeCore);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        var oldMatrix = Gizmos.matrix;
        // create a matrix which translates an object by "position", rotates it by "rotation" and scales it by "halfExtends * 2"
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, boxHalfExtents * 2);
        // Then use it one a default cube which is not translated nor scaled
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        Gizmos.matrix = oldMatrix;

    }

    private IEnumerator CoreOperation()
    {
        int index = 0;
        var waitTimer = new WaitForSeconds(1);
        while (activeCore)
        {

            if (counter > 0)
            {
                yield return waitTimer;
                counter--;
            }

            numberOfColliders = Physics.OverlapBoxNonAlloc(transform.position, boxHalfExtents, hitCollider, transform.rotation, layerToHit);
            if (hitCollider[index] != null && index <= numberOfColliders - 1)
            {

                if (hitCollider[index].GetComponent<HealthScript>())
                {
                    DamageObject(hitCollider[index].GetComponent<HealthScript>());
                }
                index++;
            }
            else
            {
                counter = damageTimer;
                index = 0;
            }
            yield return activeCore;
        }
    }
    private void DamageObject(HealthScript collisionObjectHealthScript)
    {
        collisionObjectHealthScript.RemoveHealth(damage);
    }
}

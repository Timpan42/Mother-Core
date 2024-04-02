using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class CorePower : MonoBehaviour
{
    private bool activateCorePower = false;
    [SerializeField] private float pullForce;
    [SerializeField] private Transform coreHolder;
    [SerializeField] private Transform parent;
    [SerializeField] private Transform pullCenterPoint;
    [SerializeField] private Vector3 boxHalfExtents;
    [SerializeField] private LayerMask layerToHit;
    private Collider[] hitCollider = new Collider[10];
    private int numberOfColliders;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var oldMatrix = Gizmos.matrix;
        // create a matrix which translates an object by "position", rotates it by "rotation" and scales it by "halfExtends * 2"
        Gizmos.matrix = Matrix4x4.TRS(pullCenterPoint.position, pullCenterPoint.rotation, boxHalfExtents * 2);
        // Then use it one a default cube which is not translated nor scaled
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

        Gizmos.matrix = oldMatrix;

    }

    private IEnumerator CorePullObject()
    {
        int index = 0;
        while (activateCorePower)
        {
            numberOfColliders = Physics.OverlapBoxNonAlloc(pullCenterPoint.position, boxHalfExtents, hitCollider, pullCenterPoint.rotation, layerToHit);
            if (hitCollider[index] != null && index <= numberOfColliders - 1)
            {
                if (hitCollider[index].GetComponent<AffectedByCore>())
                {
                    pullObject(hitCollider[index].GetComponent<AffectedByCore>(), parent.position);
                }
                index++;
            }
            else
            {
                index = 0;
            }
            yield return activateCorePower;
        }
    }
    private void pullObject(AffectedByCore collisionObjectScript, Vector3 distans)
    {
        collisionObjectScript.pulledByCore(distans, pullForce);
    }
    public void ActivateCore()
    {
        if (activateCorePower == false)
        {
            activateCorePower = true;
            StartCoroutine(CorePullObject());
        }
        else
        {
            activateCorePower = false;
        }
    }
}

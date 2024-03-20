using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CorePower : MonoBehaviour
{
    private bool activateCorePower = false;
    [SerializeField] private float pullForce;
    [SerializeField] private Transform coreHolder;

    private void OnTriggerStay(Collider collisionObject)
    {
        if (!activateCorePower)
        {
            return;
        }
        else if (activateCorePower)
        {
            Vector3 newDistans = CalculateDistans(collisionObject.transform);
            if (collisionObject.GetComponent<AffectedByCore>())
            {
                pullObject(collisionObject.GetComponent<AffectedByCore>(), newDistans);
            }
        }
    }

    private void pullObject(AffectedByCore collisionObjectScript, Vector3 distans)
    {
        collisionObjectScript.pulledByCore(distans, pullForce);
    }

    private Vector3 CalculateDistans(Transform collisionObject)
    {
        float xDistans = coreHolder.position.x - collisionObject.position.x;
        float yDistans = coreHolder.position.x - collisionObject.position.y;
        float zDistans = coreHolder.position.x - collisionObject.position.z;


        return new Vector3(xDistans, yDistans, zDistans);
    }

    public void ActivateCore()
    {
        activateCorePower = !activateCorePower;
    }
}

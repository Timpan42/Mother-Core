using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorePower : MonoBehaviour
{
    [SerializeField] private float pullForce; 
    [SerializeField] private Transform coreHolder;
    private AffectedByCore affectScriptInCore;

    private void OnTriggerEnter(Collider collisionObject) {
        Debug.Log("object is in core range");
        affectScriptInCore = collisionObject.GetComponent<AffectedByCore>();
    }

    private void OnTriggerStay(Collider collisionObject) {
        Vector3 newDistans = CalculateDistans(collisionObject.transform);
        affectScriptInCore.pulledByCore(newDistans, pullForce);
    }

    private Vector3 CalculateDistans(Transform collisionObject){
        float xDistans = coreHolder.position.x - collisionObject.position.x;
        float yDistans = coreHolder.position.x - collisionObject.position.y;
        float zDistans = coreHolder.position.x - collisionObject.position.z;

        
        return new Vector3(xDistans, yDistans, zDistans);
    }

}

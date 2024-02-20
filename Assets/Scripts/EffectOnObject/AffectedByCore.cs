using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class AffectedByCore : MonoBehaviour
{
    private Rigidbody objectRigidbody;

    private void Start() {
        objectRigidbody = GetComponent<Rigidbody>();
    }
    public void pulledByCore(Vector3 forceDirection,float pullForce){
        objectRigidbody.AddForce(forceDirection * pullForce);
    }
}

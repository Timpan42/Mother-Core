using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class AffectedByCore : MonoBehaviour
{
    public void pulledByCore(Vector3 forceDirection, float pullForce)
    {
        float pullSpeed = pullForce * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, forceDirection, pullSpeed);
    }
}

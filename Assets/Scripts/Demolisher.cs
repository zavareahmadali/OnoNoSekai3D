using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Demolisher : MonoBehaviour, IBreakable
{
    private Rigidbody[] parts;
    void Start()
    {
        parts = transform.GetComponentsInChildren<Rigidbody>();
    }

    public void Demolish(Vector3 hitPosition)
    {
        foreach (var p in parts)
        {
            //p.AddForceAtPosition((transform.position - hitPosition).normalized * 200, p.transform.position);
            p.AddForceAtPosition(Vector3.up * 200, p.transform.position);
            p.GetComponent<Collider>().enabled = false;
        }
        Destroy(gameObject, 2f);
    }
}

public interface IBreakable
{
    void Demolish(Vector3 hitPosition);
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Transform originalParent;
    private Rigidbody rb;
    public Manusia[] manusia;
    public string[] jokes;
    public Tuple<Manusia, string>[] jokesTuple;

    void Start()
    {
        originalParent = transform.parent;
        rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // Menonaktifkan physics ketika item dipegang
        }

        jokesTuple = new Tuple<Manusia, string>[manusia.Length];    // setting jawaban dgn jenis joke yg sesuai
        for (int i = 0; i < manusia.Length; i++)
        {
            jokesTuple[i] = Tuple.Create(manusia[i], jokes[i]);
        }
    }

    public void Pickup(Transform newParent)
    {
        transform.SetParent(newParent);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    public void Drop()
    {
        transform.SetParent(originalParent);
        if (rb != null)
        {
            rb.isKinematic = false;
        }
    }
}

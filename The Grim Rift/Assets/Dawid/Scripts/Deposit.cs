using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Deposit : MonoBehaviour
{
    public bool dropCrystal = false;
    bool isEmpty = false;
    public GameObject crystalPrefab;

    void Update()
    {
        if (!isEmpty && dropCrystal)
        {
            GameObject crystal;
            crystal = Instantiate(crystalPrefab, transform.position, transform.rotation);
            crystal.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 2);
            
            isEmpty = true;
            dropCrystal = false;
        }
        else if (isEmpty)
        {
            dropCrystal = false;
        }
    }
}

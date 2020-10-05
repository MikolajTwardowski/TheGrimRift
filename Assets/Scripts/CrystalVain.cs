﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalVain : Interactable
{

    public bool dropCrystal = false;
    bool isEmpty = false;
    public GameObject crystalPrefab;
    private Vector3 moveCrystal;
    
    protected override void InteractionWithPickaxe(GameObject item)
    {
        if (!isEmpty && dropCrystal)
        {
            GameObject crystal;
            moveCrystal = new Vector3(transform.position.x +1, transform.position.y -0.1f, transform.position.z);
            crystal = Instantiate(crystalPrefab, moveCrystal, transform.rotation);
            crystal.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 2);

            SoundManager.current.PlaySound(SoundManager.Sound.Mining);
            
            isEmpty = true;
            dropCrystal = false;
        }
        else if (isEmpty)
        {
            dropCrystal = false;
            Debug.Log("Nothing left");
        }
    }

    protected override void InteractionWithCrystalShard(GameObject item)
    {
        Debug.Log("Use pickaxe, not crystalshard!");
    }

    protected override void InteractionWithOther(GameObject item)
    {
        Debug.Log("Use pickaxe, not this!");
    }

    protected override void InteractionWithNull()
    {
        Debug.Log("Use pickaxe, not your hand!");;
    }
}

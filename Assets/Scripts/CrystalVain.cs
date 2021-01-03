using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalVain : Interactable
{

    public bool dropCrystal = false;
    bool isEmpty = false;
    public GameObject crystalPrefab;
    private Vector3 moveCrystal;
   // public GameObject BrokenPickaxeMessage;
    [SerializeField] float time = 5f;
    
    protected override void InteractionWithPickaxe(GameObject item)
    {
        if (!isEmpty && dropCrystal)
        {
            GameObject crystal;
            moveCrystal = new Vector3(transform.position.x + 1.5f, transform.position.y, transform.position.z);
            crystal = Instantiate(crystalPrefab, moveCrystal, transform.rotation);
            crystal.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 2);

            SoundManager.current.PlaySound(SoundManager.Sound.Mining);
            
            isEmpty = true;
            //dropCrystal = false;
            Destroy(item);
            //BrokenPickaxeMessage.SetActive(true);
        }
        else if (isEmpty)
        {
            dropCrystal = false;
            Debug.Log("Nothing left");
        }
    }
    
    /*void HideMessagePickaxe(GameObject item)
    {
        for (float i = time; i > 0; i--)
        {
            BrokenPickaxeMessage.SetActive(false);
        }
    }*/
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

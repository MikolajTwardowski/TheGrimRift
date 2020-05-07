using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{

    public Transform target;
    private Vector3 Portal;
    public GameObject Arrow;
    public Transform ArrowPos;
    void Start()
    {
        
    }

    
    void Update()
    {
        Portal = new Vector3(target.position.x, target.position.y, target.position.z);
        transform.LookAt(Portal);

    }

}
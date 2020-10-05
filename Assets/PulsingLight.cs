using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulsingLight : MonoBehaviour
{
    
     Light CrystalLight;
     float lightPercentage;
     //public float PulseSpeed = 3;
    
        void Start()
        {
            CrystalLight = GetComponent<Light>();
        }
    
        void Update()
        {
            CrystalLight.intensity = Mathf.Lerp(75.0f, 150.0f, Mathf.PingPong(Mathf.Sin(Time.time), 8));
        }
}

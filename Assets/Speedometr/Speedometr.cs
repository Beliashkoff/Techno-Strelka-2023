using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometr : MonoBehaviour
{
    public float maxspeed;
    public Rigidbody car;
    public Image im;
    public float scale;
    void Update()
    {
        float speed = car.velocity.magnitude;
        Debug.Log(speed);
        if (speed >= 0f) 
        {
            im.fillAmount = speed / maxspeed * scale; 
        }
    }
}

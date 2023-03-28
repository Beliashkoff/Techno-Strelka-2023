using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystic : MonoBehaviour
{
    [SerializeField] public float maxDistance;
    private float startPosX;
    private float startPosZ;
    public float curentJoysticPosX { get { return (transform.position.x - startPosX)/maxDistance; } }
    public float curentJoysticPosZ { get { return (transform.position.z - startPosZ)/maxDistance; } }
    void Awake()
    {
        startPosX = transform.position.x;
        startPosZ = transform.position.z;
        
    }
}

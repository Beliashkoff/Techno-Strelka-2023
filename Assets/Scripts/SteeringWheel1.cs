using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringWheel1 : MonoBehaviour
{
    public Transform attach;
    private void LateUpdate()
    {
        transform.position = attach.position;
        transform.rotation = new Quaternion(attach.rotation.x, transform.rotation.y, attach.rotation.z, attach.rotation.w);
    }
}

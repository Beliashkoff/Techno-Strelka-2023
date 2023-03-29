using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometr : MonoBehaviour
{
    public float speed;
    public GameObject speedometer;
    public float scale; 
    public float maxSpeed;


    void Update()
    {
        speed = gameObject.GetComponent<Rigidbody>().velocity.magnitude * 3.6f;

        //Debug.Log(speed);

        float speedPercent = speed / maxSpeed; // ���������� �������� ���������� ����������
        speedometer.GetComponent<Image>().fillAmount = speedPercent * scale; // ��������� �������� �� ����������
    }
}

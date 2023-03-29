using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using DG.Tweening;

public class GameController : MonoBehaviour
{
    public GameObject[] rayInteractors;
    public GameObject[] directInteractors;
    public GameObject[] objectsToDisable;
    public GameObject attachCarPoint;
    public GameObject movingSurface;
    public Transform moveAtach;
    public Material disolveMat;
    public float disolveSpeed;
    private ScreenFade screenFade;
    private bool isDisolving;
    private CarController carController;

    private void Start()
    {
        disolveMat.SetFloat("_CutoffHeight", 9f);
        screenFade = FindObjectOfType<ScreenFade>();
        carController = FindObjectOfType<CarController>();
    }
    private void FixedUpdate()
    {
        if (isDisolving)
        {
            float value = disolveMat.GetFloat("_CutoffHeight") - Time.fixedDeltaTime * disolveSpeed;
            if (value >= -7f)
            {
                disolveMat.SetFloat("_CutoffHeight", value);
            }
            else
            {
                disolveMat.SetFloat("_CutoffHeight", -9f);
                isDisolving = false;
            }
        }
    }
    public void ChangeInteractMetod()
    {
        foreach (GameObject interactor in rayInteractors)
            interactor.SetActive(!interactor.activeSelf);
        foreach (GameObject interactor in directInteractors)
            interactor.SetActive(!interactor.activeSelf);
    }
    public void SetupCar()
    {
        StartCoroutine(SetupCarTimer());
    }
    private IEnumerator SetupCarTimer()
    {
        screenFade.FadeOut();
        yield return new WaitForSeconds(screenFade.fadeTime+1);
        transform.position = attachCarPoint.transform.position;
        transform.rotation = attachCarPoint.transform.rotation;
        transform.parent = attachCarPoint.transform;
        foreach (GameObject obj in objectsToDisable)
            obj.SetActive(false);
        screenFade.FadeIn();
        yield return new WaitForSeconds(1);
        isDisolving = true;
        StartCoroutine(MoveTimer());
    }
    private IEnumerator MoveTimer()
    {
        yield return new WaitForSeconds(15);
        movingSurface.transform.DOMove(moveAtach.position, 20);
        movingSurface.transform.DORotateQuaternion(moveAtach.rotation, 20);
        yield return new WaitForSeconds(20);
        carController.carRb.isKinematic = false;
    }
}

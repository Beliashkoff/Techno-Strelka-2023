using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class OpenMenu : MonoBehaviour
{
    public InputActionReference toggleReference = null;
    public GameObject tablet;
    private GameObject walls;
    private GameObject furniture;
    
    private void Awake()
    {
        toggleReference.action.canceled += Toggle;
        furniture = GameObject.Find("Furniture");
        walls = GameObject.Find("Walls");
    }


    private void OnDestroy()
    {
        toggleReference.action.canceled -= Toggle;
    }
    public void Toggle(InputAction.CallbackContext context)
    {
        bool isActive = !gameObject.activeSelf;
        gameObject.SetActive(isActive);
        tablet.SetActive(isActive);
        
    }
    
    public void FurnitToogle()
    {
        bool isActiveS = !furniture.activeSelf;
        furniture.SetActive(isActiveS);
    }
    public void WallToogle()
    {
        bool isActiveS = !walls.activeSelf;
        walls.SetActive(isActiveS);
    }
    public void LoadScene(int number)
    {
        SceneManager.LoadScene(number);
    }
}

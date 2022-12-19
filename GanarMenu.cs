using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GanarMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState=CursorLockMode.Confined;
    }
    public void menu()
    {
        Debug.Log("Juego");
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{
  public void jugar()
    {
        Debug.Log("Juego");
        SceneManager.LoadScene("Juego", LoadSceneMode.Single);
    }
}

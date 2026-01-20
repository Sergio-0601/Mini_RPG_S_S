using UnityEngine;
using UnityEngine.SceneManagement;
public class Main_Menu_Controller : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("02_House_Start");
    }
    public void ExitGame()
    {
        Debug.Log("Ha salido del juego");
        Application.Quit();
    }
}

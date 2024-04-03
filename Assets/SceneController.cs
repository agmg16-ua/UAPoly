using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Método para cargar una escena por nombre
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
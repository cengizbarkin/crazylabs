using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [SerializeField]
    private Button restartButton;
    

    private void Start()
    {
        restartButton.onClick.AddListener(RestartButtonPressed);
    }

    private void RestartButtonPressed()
    {
        SceneManager.LoadScene("Scenes/Game");
    }
    
}

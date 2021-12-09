using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField]
        private Button playButton;
        
        private void Start()
        {
            playButton.onClick.AddListener(PlayButtonClicked);
        }

        private static void PlayButtonClicked()
        {
            SceneManager.LoadScene("Scenes/Game");
        } 
    }
}

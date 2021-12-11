using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        public static UnityAction<bool> MainMenuAction;

        [SerializeField]
        private Button playButton;
        
        [SerializeField] private Toggle useObstacleToggle;
        private void Start()
        {
            playButton.onClick.AddListener(PlayButtonClicked);
            useObstacleToggle.onValueChanged.AddListener(UseObstacleToggleValueChanged);
            MainMenuData.ChangeObstacleToggle(useObstacleToggle.isOn);
        }
        

        private static void PlayButtonClicked()
        {
            SceneManager.LoadScene("Scenes/Game");
        }
        
        private static void UseObstacleToggleValueChanged(bool value)
        {
            MainMenuData.ChangeObstacleToggle(value);
        }
    }
}

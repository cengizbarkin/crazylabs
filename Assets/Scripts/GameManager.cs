using Drops;
using MainMenu;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
using Utils;

public class GameManager : MonoBehaviour
{
    public static UnityAction RestartGameAction;

    [SerializeField]
    private GameObject obstacleGameObject;
    
    [SerializeField]
    private GameObject spawnerGameObject;
    
    [SerializeField]
    private SpriteAtlas dropsSpriteAtlas;
    
    [SerializeField]
    private Properties properties;
    
    private Spawner _spawner;
    private DropFactory _dropFactory;
    
    private void Start()
    {
        _dropFactory = new DropFactory(dropsSpriteAtlas, properties);
        _spawner = new Spawner(spawnerGameObject, _dropFactory, properties);
        obstacleGameObject.SetActive(MainMenuData.UseObstacleToggle);
    }
    
    public void Restart()
    {
        RestartGameAction?.Invoke();
    }
    
    private void OnDestroy()
    {
        _spawner.GameIsKilled();
        _dropFactory.GameIsKilled();
    }

}

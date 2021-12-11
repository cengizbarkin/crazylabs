using Drops;
using UnityEngine;
using UnityEngine.U2D;

public class GameManager : MonoBehaviour
{

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
        _spawner = new Spawner(spawnerGameObject, _dropFactory);
    }
    
    public void Restart()
    {
        _dropFactory.Restart();
        _spawner.Restart();
    }
    
    private void OnDestroy()
    {
        _spawner.GameIsKilled();
    }

}

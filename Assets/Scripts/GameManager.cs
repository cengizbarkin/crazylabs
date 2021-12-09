using Drops;
using UnityEngine;
using UnityEngine.U2D;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    private GameObject spawnerGameObject;
    
    [SerializeField]
    private SpriteAtlas dropsSpriteAtlas;

    private Spawner _spawner;
    private DropFactory _dropFactory;
    
    private void Start()
    {
        _dropFactory = new DropFactory(dropsSpriteAtlas);
        _spawner = new Spawner(spawnerGameObject, _dropFactory);
        Application.targetFrameRate = 60;
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

    private void OnGUI() {

        int w = Screen.width, h = Screen.height;
        GUIStyle style = new GUIStyle();
        Rect rect = new Rect(100, -100, w, h);
        style.alignment = TextAnchor.LowerLeft;
        style.fontSize = h * 2 / 80;
        style.fontStyle = FontStyle.Bold;
        style.normal.textColor = new Color(1f, 1f, 1f);
        float msec = Time.deltaTime * 1000.0f;
        float fps = 1.0f / Time.deltaTime;
        string text = string.Format("{1:0.}/{0:0.0}", msec, fps);
        GUI.Label(rect, text, style);
        
    }
}

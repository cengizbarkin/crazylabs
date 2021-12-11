using UnityEngine;
public class Borders : MonoBehaviour
{
    [SerializeField]
    private Camera mainCamera;
    private EdgeCollider2D _collider2D;

    private void Start()
    {
        _collider2D = GetComponent<EdgeCollider2D>();
        var bottomLeft = mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
        var topLeft = mainCamera.ScreenToWorldPoint(new Vector3(0, mainCamera.pixelHeight));
        var bottomRight = mainCamera.ScreenToWorldPoint(new Vector3(0, mainCamera.pixelWidth));

        var height = Vector3.Distance(topLeft, bottomLeft);
        var width = Vector3.Distance(bottomLeft, bottomRight);
        _collider2D.offset = new Vector2(0, -height / 2);
        
        var points = new[]
        {
            new Vector2(-width / 2, height),
            new Vector2(-width / 2, 0),
            new Vector2(0, 0),
            new Vector2(width / 2, 0),
            new Vector2(width / 2, height)
        };
        _collider2D.points = points;
        
    }
}

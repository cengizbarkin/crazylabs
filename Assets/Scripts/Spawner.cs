using Drops;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject drop;

    private int _totalDropCount = 100;
    private int _instantiatedDropDropCount;
    private float lastY;
    private int _frameCount;

    private DropFactory _dropFactory;
    
    
    private void Start()
    {
        _dropFactory = new DropFactory();
        for (var i = 0; i < _totalDropCount; i++)
        {
            var position = transform.position;
            var aDrop = (i % 4) switch
            {
                0 => _dropFactory.GetADrop(typeof(GrayDrop)),
                1 => _dropFactory.GetADrop(typeof(GreenDrop)),
                2 => _dropFactory.GetADrop(typeof(PinkDrop)),
                3 => _dropFactory.GetADrop(typeof(YellowDrop)),
                _ => null
            };
            var randomPos = new Vector3(position.x, position.y + i / 2.0f);
            aDrop?.OpenThisDrop(randomPos, transform);
            aDrop?.AddForceToThisDrop();
        }
    }

    private void Update()
    {
        if (_instantiatedDropDropCount < _totalDropCount)
        {
            //var d = Instantiate(drop, transform.localPosition, Quaternion.identity);
            //d.transform.SetParent(gameObject.transform);
            //_instantiatedDropDropCount++;
        }

        _frameCount++;
    }
    
}

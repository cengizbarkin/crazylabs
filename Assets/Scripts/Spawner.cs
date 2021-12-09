using System.Collections.Generic;
using Colors;
using Drops;
using UnityEngine;

public class Spawner
{
    private readonly List<IDropColor> _colorList;
    private readonly GameObject _spawnerGameObject;
    private readonly DropFactory _dropFactory;
    
    public Spawner(GameObject spawnerGameObject, DropFactory dropFactory)
    {
        _spawnerGameObject = spawnerGameObject;
        _dropFactory = dropFactory;
        
        _colorList = new List<IDropColor>
        {
            new Gray(),
            new Green(),
            new Pink(),
            new Yellow()
        };
        SpawnDrops(100);
        DropFactory.DropsAreClosedAction += DropsAreClosedAction;
    }

    private void DropsAreClosedAction(int closedDropCount)
    {
        SpawnDrops(closedDropCount);
    }

    public void Restart()
    {
        SpawnDrops(100);
    }

    private void SpawnDrops(int count)
    {
        for (var i = 0; i < count; i++)
        {
            var position = _spawnerGameObject.transform.position;
            var aDrop = _dropFactory.GetADrop(_colorList[i % 4]);
            var randomPos = new Vector3(position.x, position.y + i / 2.0f);
            aDrop.OpenThisDrop(randomPos,_spawnerGameObject.transform);
            aDrop.AddForceToThisDrop();
        }
    }

    public void GameIsKilled()
    {
        DropFactory.DropsAreClosedAction -= DropsAreClosedAction;
    }
}

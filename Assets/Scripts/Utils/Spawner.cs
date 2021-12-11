using System.Collections.Generic;
using Colors;
using Drops;
using UnityEngine;

namespace Utils
{
    public class Spawner
    {
        private readonly List<IDropColor> _colorList;
        private readonly GameObject _spawnerGameObject;
        private readonly DropFactory _dropFactory;
        private readonly Properties _properties;
        public Spawner(GameObject spawnerGameObject, DropFactory dropFactory, Properties properties)
        {
            _spawnerGameObject = spawnerGameObject;
            _dropFactory = dropFactory;
            _properties = properties;
        
            _colorList = new List<IDropColor>
            {
                new Gray(),
                new Green(),
                new Pink(),
                new Yellow()
            };
            SpawnDrops(_properties.initialDropSpawnCount);
            DropFactory.DropsAreClosedAction += DropsAreClosedAction;
            GameManager.RestartGame += RestartGameAction;
        }

        private void DropsAreClosedAction(IDropColor color, int closedDropCount)
        {
            SpawnDrops(closedDropCount);
        }

        private void RestartGameAction()
        {
            SpawnDrops(_properties.initialDropSpawnCount);
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
            GameManager.RestartGame -= RestartGameAction;
        }
    }
}

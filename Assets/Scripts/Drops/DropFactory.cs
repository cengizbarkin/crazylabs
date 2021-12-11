using System.Collections.Generic;
using Colors;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;
using Utils;


namespace Drops
{
    public class DropFactory
    {
        public static UnityAction<IDropColor, int> DropsAreClosedAction;
        
        private readonly SpriteAtlas _dropsSpriteAtlas;
        private static List<Drop> dropList;
        private static List<Drop> closestDrops;
        private readonly Properties _properties;
        
        public DropFactory(SpriteAtlas dropsSpriteAtlas, Properties properties)
        {
            dropList = new List<Drop>();
            closestDrops = new List<Drop>();
            _dropsSpriteAtlas = dropsSpriteAtlas;
            _properties = properties;
            GameManager.RestartGame += RestartGameAction;
        }
        
        public Drop GetADrop(IDropColor dropColor)
        {
            var drop = dropList.Find(d => d.DropColor ==  dropColor && !d.IsInUse);
            if (drop != null)
            {
                return drop;
            }
            var sprite = _dropsSpriteAtlas.GetSprite(dropColor.GetType().Name); 
            drop = new Drop(sprite, dropColor, this, _properties);
            dropList.Add(drop);
            return drop;    
            
        }

        
        private void RestartGameAction()
        {
            foreach (var drop in dropList)
            {
                drop.CloseThisDrop();
            }
        }
        
        
        public void DropSelected(Drop selectedDrop)
        {
            closestDrops.Add(selectedDrop);
            for (var i = 0; i < dropList.Count; i++)
            {
                if (dropList[i].IsInUse && dropList[i].DropColor == selectedDrop.DropColor)
                {
                    for (var j = 0; j < closestDrops.Count; j++)
                    {
                        var distance = Vector2.Distance(dropList[i].Transform.position, closestDrops[j].Transform.position);
                        if (distance < _properties.maxDistanceToSelectDrop && 
                            !closestDrops.Contains(dropList[i]) && 
                            dropList[i].DropColor == selectedDrop.DropColor)
                        {
                            closestDrops.Add(dropList[i]);
                            i = 0;
                        }
                    }
                }
            }
            if (closestDrops.Count > 1)
            {
                foreach (var drop in closestDrops)
                {
                    drop.CloseThisDrop();
                }
                DropsAreClosedAction?.Invoke(selectedDrop.DropColor, closestDrops.Count);
            }
            closestDrops.Clear();
        }

        public void GameIsKilled()
        {
            GameManager.RestartGame -= RestartGameAction;
        }
    }
}

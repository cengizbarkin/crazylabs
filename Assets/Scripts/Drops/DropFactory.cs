using System.Collections.Generic;
using Colors;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.U2D;


namespace Drops
{
    public class DropFactory
    {
        public static UnityAction<int> DropsAreClosedAction;
        private readonly SpriteAtlas _dropsSpriteAtlas;
        private static List<Drop> dropList;
        private static List<Drop> closestDrops;
        
        public DropFactory(SpriteAtlas dropsSpriteAtlas)
        {
            dropList = new List<Drop>();
            closestDrops = new List<Drop>();
            _dropsSpriteAtlas = dropsSpriteAtlas;
        }

        public Drop GetADrop(IDropColor dropColor)
        {
            var drop = dropList.Find(d => d.DropColor ==  dropColor && !d.IsInUse);
            if (drop != null)
            {
                return drop;
            }
            var sprite = _dropsSpriteAtlas.GetSprite(dropColor.GetType().Name); 
            drop = new Drop(sprite, dropColor);
            dropList.Add(drop);
            return drop;    
            
        }

        public void Restart()
        {
            foreach (var drop in dropList)
            {
                drop.CloseThisDrop();
            }
        }
        
        
        public static void DropSelected(Drop selectedDrop)
        {
            closestDrops.Add(selectedDrop);
            for (var i = 0; i < dropList.Count; i++)
            {
                if (dropList[i].DropColor == selectedDrop.DropColor)
                {
                    for (var j = 0; j < closestDrops.Count; j++)
                    {
                        var distance = Vector2.Distance(dropList[i].Transform.position, closestDrops[j].Transform.position);
                        if (distance < 0.6 && !closestDrops.Contains(dropList[i]))
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
                DropsAreClosedAction?.Invoke(closestDrops.Count);
            }
            closestDrops.Clear();
        }
        
    }
}

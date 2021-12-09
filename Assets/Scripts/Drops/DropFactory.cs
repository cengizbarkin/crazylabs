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
        private static List<Drop> dropToCloseList;

        public DropFactory(SpriteAtlas dropsSpriteAtlas)
        {
            dropList = new List<Drop>();
            dropToCloseList = new List<Drop>();
            _dropsSpriteAtlas = dropsSpriteAtlas;
        }

        public Drop GetADrop(IDropColor dropColor)
        {
            var drop = dropList.Find(d => d.DropColor.GetType() ==  dropColor.GetType() && !d.IsInUse);
            if (drop != null) return drop;
            var sprite = _dropsSpriteAtlas.GetSprite(dropColor.GetType().Name); 
            drop = new Drop(sprite, dropColor);
            dropList.Add(drop);
            return drop;
        }

        
        public static void Called(IDropColor dropColor, Transform dropPosition )
        {
            foreach (var drop in dropList)
            {
                if (drop.DropColor == dropColor)
                {
                    dropToCloseList.Add(drop);
                }
            }

            if (dropToCloseList.Count > 0)
            {
                foreach (var drop in dropToCloseList)
                {
                    drop.CloseThisDrop();
                }
                DropsAreClosedAction?.Invoke(dropToCloseList.Count);
                dropToCloseList.Clear();
            }
        }
        
    }
}

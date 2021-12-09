using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

namespace Drops
{
    public class DropFactory
    {
        private readonly SpriteAtlas _dropsSpriteAtlas = Resources.Load<SpriteAtlas>("SpriteAtlases/Drops");
        private readonly List<Drop> _dropList;

        public DropFactory()
        {
            _dropList = new List<Drop>();
        }

        public Drop GetADrop(Type dropType)
        {
            var drop = _dropList.Find(d => d.GetType() == dropType && !d.IsInUse);
            if (drop != null) return drop;
            if (dropType == typeof(GrayDrop))
            {
                drop = new GrayDrop(_dropsSpriteAtlas);
            } 
            else if (dropType == typeof(GreenDrop))
            {
                drop = new GreenDrop(_dropsSpriteAtlas);
            }
            else if (dropType == typeof(PinkDrop))
            {
                drop = new PinkDrop(_dropsSpriteAtlas);
            }
            else if (dropType == typeof(YellowDrop))
            {
                drop = new YellowDrop(_dropsSpriteAtlas);
            }
            _dropList.Add(drop);
            return drop;
        }
    }
}

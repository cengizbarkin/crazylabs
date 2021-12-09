using UnityEngine;
using UnityEngine.U2D;

namespace Drops
{
    public class YellowDrop : Drop
    {
        public YellowDrop(SpriteAtlas spriteAtlas) : base(spriteAtlas)
        {
            
        }

        public override void AddForceToThisDrop()
        {
            var x = Random.Range(-0.5f, 0.5f);
            var y = Random.Range(-4.0f, -3.0f);
            var force = new Vector2(x, y);
            Rigidbody2D.velocity = force;
        }
    }
}

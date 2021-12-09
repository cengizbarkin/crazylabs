using UnityEngine;
using UnityEngine.U2D;

namespace Drops
{
    public class GreenDrop : Drop
    {
        public GreenDrop(SpriteAtlas spriteAtlas) : base(spriteAtlas) { }

        public override void AddForceToThisDrop()
        {
            var x = Random.Range(-0.5f, 0.5f);
            var y = Random.Range(-0.5f, -0.1f);
            var force = new Vector2(x, y);
            Rigidbody2D.velocity = force;
        }
    }
}

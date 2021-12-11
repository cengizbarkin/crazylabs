using UnityEngine;

namespace Utils
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class SpriteScaler : MonoBehaviour
    {
        private void Start()
        {
            if (Camera.main == null) return;
            var spriteRenderer = GetComponent<SpriteRenderer>();
            var sprite = spriteRenderer.sprite;
        
            var width = sprite.bounds.size.x;
            var height = sprite.bounds.size.y;
     
            var worldScreenHeight = Camera.main.orthographicSize * 2.0f;
            var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

            var scale = new Vector3(worldScreenWidth / width, worldScreenHeight / height);
            transform.localScale = scale;
        }
    
    }
}

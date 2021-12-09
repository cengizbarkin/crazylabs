using Lean.Touch;
using UnityEngine;
using UnityEngine.U2D;

namespace Drops
{
    public abstract class Drop
    {
        private GameObject _dropGameObject;
        private SpriteRenderer _spriteRenderer;
        protected Rigidbody2D Rigidbody2D;
        protected CircleCollider2D CircleCollider2D;
        private SpriteAtlas _spriteAtlas;
        
        protected Transform Transform { get; set; }
        public bool IsInUse { get; set; }
    
        //private Vector2 dropPos;
        private bool _componentsAreAdded;
        
        protected Drop(SpriteAtlas spriteAtlas)
        {
            _spriteAtlas = spriteAtlas;
            LeanTouch.OnFingerTap += HandleFingerTap;
        }


        public void OpenThisDrop(Vector2 position, Transform parentTransform)
        {
            if (!_componentsAreAdded)
            {
                AddComponentsOfDrop();
                _componentsAreAdded = true;
            }
            IsInUse = true;
            _dropGameObject.SetActive(true);
            _dropGameObject.transform.SetParent(parentTransform);
            _dropGameObject.transform.position = position;
        }

        private void CloseThisDrop()
        {
            IsInUse = false;
            _dropGameObject.SetActive(false);
        }
        
        public abstract void AddForceToThisDrop();
        

        private void AddComponentsOfDrop()
        {
            _dropGameObject = new GameObject(GetType().Name);
            _spriteRenderer = _dropGameObject.AddComponent<SpriteRenderer>();
            Rigidbody2D = _dropGameObject.AddComponent<Rigidbody2D>();
            Rigidbody2D.mass = 0.1f;
            CircleCollider2D = _dropGameObject.AddComponent<CircleCollider2D>();
            Transform = _dropGameObject.GetComponent<Transform>();
            _spriteRenderer.sprite = _spriteAtlas.GetSprite(GetType().Name);
            
            var spriteHalfSize = _spriteRenderer.sprite.bounds.extents;
            CircleCollider2D.radius = spriteHalfSize.x > spriteHalfSize.y ? spriteHalfSize.x : spriteHalfSize.y;
        }
        
        private void HandleFingerTap(LeanFinger finger)
        {
            if (AmISelectedTile(finger))
            {
                CloseThisDrop();
            }
        }
        
    
        private bool AmISelectedTile(LeanFinger finger)
        {
            if (!Camera.main) return false;
            //dropPos = _dropGameObject.transform.position;
            var fingerWordPosition = Camera.main.ScreenToWorldPoint(finger.ScreenPosition);
            return CircleCollider2D.OverlapPoint(fingerWordPosition);
        }
    }
}

using Colors;
using Lean.Touch;
using UnityEngine;

namespace Drops
{
    public class Drop
    {
        public IDropColor DropColor { get; }
        public Transform Transform { get; private set; }
        public bool IsInUse { get; private set; }
        
        private GameObject _dropGameObject;
        private SpriteRenderer _spriteRenderer;
        private Rigidbody2D _rigidbody2D;
        private CircleCollider2D _circleCollider2D;
        private readonly Sprite _sprite;
        private bool _componentsAreAdded;
        
        
        public Drop(Sprite sprite, IDropColor dropColor)
        {
            _sprite = sprite;
            DropColor = dropColor;
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

        public void CloseThisDrop()
        {
            IsInUse = false;
            _dropGameObject.SetActive(false);
        }

        public void AddForceToThisDrop()
        {
            var x = Random.Range(-0.5f, 0.5f);
            var y = Random.Range(-0.5f, -0.1f);
            var force = new Vector2(x, y);
            _rigidbody2D.velocity = force;
        }
        

        private void AddComponentsOfDrop()
        {
            _dropGameObject = new GameObject(DropColor.GetType().Name);
            Transform = _dropGameObject.transform;
            _spriteRenderer = _dropGameObject.AddComponent<SpriteRenderer>();
            _rigidbody2D = _dropGameObject.AddComponent<Rigidbody2D>();
            _rigidbody2D.mass = 0.1f;
            _circleCollider2D = _dropGameObject.AddComponent<CircleCollider2D>();
            _spriteRenderer.sprite = _sprite;
            var spriteHalfSize = _spriteRenderer.sprite.bounds.extents;
            _circleCollider2D.radius = spriteHalfSize.x > spriteHalfSize.y ? spriteHalfSize.x : spriteHalfSize.y;
        }
        
        private void HandleFingerTap(LeanFinger finger)
        {
            if (AmISelectedTile(finger))
            {
                DropFactory.DropSelected(this);
            }
        }
        
        private bool AmISelectedTile(LeanFinger finger)
        {
            if (!Camera.main) return false;
            var fingerWordPosition = Camera.main.ScreenToWorldPoint(finger.ScreenPosition);
            return _circleCollider2D.OverlapPoint(fingerWordPosition);
        }
    }
}

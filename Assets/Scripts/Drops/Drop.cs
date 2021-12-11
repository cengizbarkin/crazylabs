using Colors;
using Lean.Touch;
using UnityEngine;
using Utils;

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
        
        private readonly DropFactory _dropFactory;
        private readonly Sprite _sprite;
        private readonly Properties _properties;
        private bool _componentsAreAdded;

        private readonly Camera _mainCamera;
        
        public Drop(Sprite sprite, IDropColor dropColor, DropFactory dropFactory, Properties properties)
        {
            _sprite = sprite;
            DropColor = dropColor;
            _dropFactory = dropFactory;
            _properties = properties;
            _mainCamera = Camera.main;
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
            var randomYValue = Random.Range(_properties.minVelocityInY, _properties.maxVelocityInY);
            var x = Random.Range(-0.5f, 0.5f);
            var y = -randomYValue;
            var velocity = new Vector2(x, y);
            _rigidbody2D.velocity = velocity;
        }
        
        private void AddComponentsOfDrop()
        {
            _dropGameObject = new GameObject(DropColor.GetType().Name);
            Transform = _dropGameObject.transform;
            _spriteRenderer = _dropGameObject.AddComponent<SpriteRenderer>();
            var physicsMaterial = new PhysicsMaterial2D {friction = _properties.frictionValue, bounciness = _properties.bouncinessValue};
            _rigidbody2D = _dropGameObject.AddComponent<Rigidbody2D>();
            _rigidbody2D.mass = _properties.massForEachDrop;
            _rigidbody2D.sharedMaterial = physicsMaterial;
            _circleCollider2D = _dropGameObject.AddComponent<CircleCollider2D>();
            _spriteRenderer.sprite = _sprite;
            var spriteHalfSize = _spriteRenderer.sprite.bounds.extents;
            _circleCollider2D.radius = spriteHalfSize.x > spriteHalfSize.y ? spriteHalfSize.x : spriteHalfSize.y;
        }
        
        private void HandleFingerTap(LeanFinger finger)
        {
            if (AmISelectedTile(finger))
            {
                _dropFactory.DropSelected(this);
            }
        }
        
        private bool AmISelectedTile(LeanFinger finger)
        {
            if (!_mainCamera) return false;
            var fingerWordPosition = _mainCamera.ScreenToWorldPoint(finger.ScreenPosition);
            return _circleCollider2D.OverlapPoint(fingerWordPosition);
        }
    }
}

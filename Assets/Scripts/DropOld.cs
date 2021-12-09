using System;
using Lean.Touch;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody2D))]
public class DropOld : MonoBehaviour
{
    private CircleCollider2D _col;
    private Rigidbody2D _rb2d;
    private SpriteRenderer _spriteRenderer;

    
    private Vector2 _force;
    private Vector2 _dropPos;
    
    private void Start()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _col = GetComponent<CircleCollider2D>();
        
        var x = Random.Range(-0.5f, 0.5f);
        var y = Random.Range(-4.0f, -3.0f);
        _force = new Vector2(x, y);
        _rb2d.velocity = _force;
        LeanTouch.OnFingerTap += HandleFingerTap;
    }
    
    private void HandleFingerTap(LeanFinger finger)
    {
        if (AmISelectedTile(finger))
        {
            Destroy(gameObject);    
        }
    }

    
    private bool AmISelectedTile(LeanFinger finger)
    {
        if (!Camera.main) return false;
        _dropPos = gameObject.transform.position;
        var fingerWordPosition = Camera.main.ScreenToWorldPoint(finger.ScreenPosition);

        return _col.OverlapPoint(fingerWordPosition);
    }
    
    private void OnDestroy()
    {
        LeanTouch.OnFingerTap -= HandleFingerTap;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float StopTime;
    [SerializeField] private float FadeSpeed;

    private Rigidbody2D _Rigidbody;
    private SpriteRenderer _Sprite;

    private void Awake()
    {
        _Rigidbody = GetComponent<Rigidbody2D>();
        _Sprite = GetComponent<SpriteRenderer>();

        _Rigidbody.velocity = Vector2.up * Speed;

        StartCoroutine(Stop());
    }


    IEnumerator Stop()
    {
        yield return new WaitForSecondsRealtime(StopTime);
        _Rigidbody.velocity = Vector2.zero;
        _Rigidbody.gravityScale = 0;

        while (_Sprite.color.a > 0)
        {
            _Sprite.color = new Color(_Sprite.color.r, _Sprite.color.g, _Sprite.color.b, _Sprite.color.a - FadeSpeed);
            yield return new WaitForFixedUpdate();
        }

        Destroy(gameObject);
    }
}

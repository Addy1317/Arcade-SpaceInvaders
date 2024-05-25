using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.ArcadeSpaceInvaders
{
    public class Invader : MonoBehaviour
    {
        [Header("Sprites")]
        [SerializeField] private Sprite[] _animateSprites;
        [SerializeField] private float _animationTime = 1.0f;

        private SpriteRenderer _spriteRenderer;
        private int _animationFrame;

        public System.Action killed;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            InvokeRepeating(nameof(AnimateSprite), this._animationTime, this._animationTime);
        }

        private void AnimateSprite()
        {
            _animationFrame++;

            if (_animationFrame >= this._animateSprites.Length)
            {
                _animationFrame = 0;
            }

            _spriteRenderer.sprite = this._animateSprites[_animationFrame];
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("Laser"))
            {
                this.killed.Invoke();
                this.gameObject.SetActive(false);
            }
        }
    }
}
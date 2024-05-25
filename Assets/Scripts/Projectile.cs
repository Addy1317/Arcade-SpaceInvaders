using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.ArcadeSpaceInvaders
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Vector3 _direction;
        [SerializeField] private float _speed;

        public System.Action destroyed;

        private void Update()
        {
            this.transform.position += this._direction * this._speed * Time.deltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(this.destroyed != null)
            {
                this.destroyed.Invoke();
            }
   
            Destroy(this.gameObject);
        }
    }
}
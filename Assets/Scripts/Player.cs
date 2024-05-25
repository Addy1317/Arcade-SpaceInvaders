using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.ArcadeSpaceInvaders
{
    public class Player : MonoBehaviour
    {
        [Header("Player Speed")]
        [SerializeField] private float _speed = 5.0f;

        [SerializeField] private Projectile _laserPrefab;

        private bool _laserActive;

        private void Update()
        {
            PlayerMovement();
            PlayerShoot();
        }

        private void PlayerMovement()
        {
            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.position += Vector3.left * this._speed * Time.deltaTime;
            }
            else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.position += Vector3.right * this._speed * Time.deltaTime;
            }
        }

        private void PlayerShoot()
        {
            if(Input.GetKey(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            if (!_laserActive)
            {
               Projectile projectile =  Instantiate(this._laserPrefab, this.transform.position, Quaternion.identity);
                projectile.destroyed += LaserDestroyed; 
                _laserActive = true;
            }
        }

        private void LaserDestroyed()
        {
            _laserActive = false;
        }
    }
}
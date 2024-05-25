using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.ArcadeSpaceInvaders
{
    public class Invaders : MonoBehaviour
    {
        [Header("Rows and Columns")]
        [SerializeField] private int _rows = 5;
        [SerializeField] private int _columns = 11;

        [Header("Move Speed")]
        [SerializeField] private AnimationCurve _speed;
        [SerializeField] private float _missileAttackRate = 1.0f;

        [Header("Missile")]
        [SerializeField] private Projectile _missilePrefab;

        [Header("Invaders Prefab")]
        [SerializeField] private Invader[] _invaderPrefabs;

        private Vector3 _direction = Vector2.right;

        public int amountKilled { get; private set; }

        public int totalInvaders => this._rows * this._columns;

        public float percentKilled => (float)this.amountKilled / (float)this.totalInvaders;

        public int amountAlive => this.totalInvaders - this.amountKilled;

        private void Awake()
        {
            InvadersSpawning();
        }

        private void Start()
        {
            InvokeRepeating(nameof(MissielAttack),this._missileAttackRate,this._missileAttackRate);
        }

        private void Update()
        {
            InvadersMovement();
        }

        private void InvadersSpawning()
        {
            for (int row = 0; row < this._rows; row++)
            {
                float width = 2.0f * (this._columns - 1);
                float height = 2.0f * (this._rows - 1);
                Vector2 centering = new Vector2(-width / 2, -height / 2);
                Vector3 rowPosition = new Vector3(centering.x, centering.y + (row * 2.0f), 0.0f);

                for (int col = 0; col < this._columns; col++)
                {
                    Invader invader = Instantiate(this._invaderPrefabs[row], this.transform);
                    invader.killed += InvaderKilled;
                    Vector3 position = rowPosition;
                    position.x += col * 2.0f;
                    invader.transform.localPosition = position;
                }
            }
        }

        private void InvadersMovement()
        {
            this.transform.position += _direction * this._speed.Evaluate(this.percentKilled) * Time.deltaTime;

            Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
            Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

            foreach (Transform invader in this.transform)
            {
                if (!invader.gameObject.activeInHierarchy)
                {
                    continue;
                }

                if(_direction == Vector3.right && invader.position.x >= (rightEdge.x - 1.0f))
                {
                    AdvanceRow();
                }
                else if(_direction == Vector3.left && invader.position.x <= (leftEdge.x + 1.0f))
                {
                    AdvanceRow();
                }
            }  
        }

        private void AdvanceRow()
        {
            _direction.x *= -1.0f;

            Vector3 position = this.transform.position;
            position.y -= 1.0f;
            this.transform.position = position;
        }

        private void InvaderKilled()
        {
            this.amountKilled++;
        }

        private void MissielAttack()
        {
            foreach (Transform invader in this.transform)
            {
                if(!invader.gameObject.activeInHierarchy)
                {
                    continue;
                }
                if(Random.value < (1.0f / (float)this.amountAlive))
                {
                    Instantiate(this._missilePrefab, invader.position, Quaternion.identity);
                    break;
                }
            }
        }
    }
}
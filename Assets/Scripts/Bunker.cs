using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SlowpokeStudio.ArcadeSpaceInvaders
{     
    public class Bunker : MonoBehaviour
    {
        private void Start()
        {
            Debug.Log("Bunker initalized");

        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log("Bunker Intact");
            if(other.gameObject.layer == LayerMask.NameToLayer("Invader"))
            {
                Debug.Log("Collided with Invader : " + gameObject.layer);
                this.gameObject.SetActive(false);
            }
        }
    }
}
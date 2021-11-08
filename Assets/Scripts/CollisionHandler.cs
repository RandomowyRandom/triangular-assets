using System;
using UnityEngine;
using UnityEngine.Events;

namespace TriangularAssets.CollisionHandling
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] private UnityEvent<GameObject> _onCollisionEnter;
        [SerializeField] private UnityEvent<GameObject> _onCollisionExit;
        [SerializeField] private UnityEvent<GameObject> _onCollisionStay;
        [Space]
        [SerializeField] private bool _includeTriggers = true;
        private void OnCollisionEnter2D(Collision2D other)
        {
            _onCollisionEnter.Invoke(other.gameObject);
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            _onCollisionExit.Invoke(other.gameObject);
        }
        
        private void OnCollisionStay2D(Collision2D other)
        {
            _onCollisionStay.Invoke(other.gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_includeTriggers)
                _onCollisionEnter.Invoke(other.gameObject);
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if(_includeTriggers)
                _onCollisionExit.Invoke(other.gameObject);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if(_includeTriggers)
                _onCollisionStay.Invoke(other.gameObject);
        }
    }
}
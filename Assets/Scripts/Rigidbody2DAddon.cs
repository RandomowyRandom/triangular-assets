using System.Collections.Generic;
using UnityEngine;

namespace TriangularAssets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rigidbody2DAddon : MonoBehaviour
    {
        //TODO: still wip, needs a lot of polish and testing
        
        [SerializeField] private AnimationCurve _addForceCurve;
        
        private Rigidbody2D _rigidbody2D;
        
        private List<FixedForce> _forces = new List<FixedForce>();

        private Vector2 _targetVelocity;
        public Vector2 TargetVelocity => _targetVelocity;
        
        private Vector2 _staticVelocity = Vector2.zero;
        public Vector2 StaticVelocity
        {
            get => _staticVelocity;
            set => _staticVelocity = value;
        }

        private void FixedUpdate()
        {
            _targetVelocity = GetTargetVelocity();
            _rigidbody2D.velocity = _targetVelocity + _staticVelocity;
            HandleTimeLeft();
        }

        private void Awake()
        {
            // cache references
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        public void AddForce(FixedForce force)
        {
            _forces.Add(force);
        }
        
        private Vector2 GetTargetVelocity()
        {
            if (_forces.Count == 0)
                return Vector2.zero;

            var vectorList = new List<Vector2>();
            foreach (var fixedForce in _forces)
            {
                var value = fixedForce.Force * (_addForceCurve.Evaluate(Mathf.Lerp(0, fixedForce.Duration, fixedForce.TimeLeft)));

                Debug.Log(_addForceCurve.Evaluate(Mathf.Lerp(0, fixedForce.Duration, fixedForce.TimeLeft)));

                vectorList.Add(value);
            }

            var average = TriangularMath.GetAverageVector2(vectorList);
            return average;
        }
        
        private void HandleTimeLeft()
        {
            for (var i = 0; i < _forces.Count; i++)
            {
                var force = _forces[i];
                
                force.TimeLeft -= Time.fixedDeltaTime;
                if (force.TimeLeft <= 0)
                {
                    _forces.RemoveAt(i);
                    i--;
                }
            }
        }
    }
    
    public class FixedForce
    {
        public Vector2 Force { get; }
        public float Duration { get; }
        
        public float TimeLeft { get; set; }

        public FixedForce(Vector2 force, float duration)
        {
            Force = force;
            Duration = duration;
            TimeLeft = duration;
        }
    }
}
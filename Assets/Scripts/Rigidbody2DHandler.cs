using System.Collections.Generic;
using UnityEngine;

namespace TriangularAssets
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rigidbody2DHandler : MonoBehaviour
    {
        //TODO: still wip, needs a lot of polish and testing
        
        [SerializeField] private AnimationCurve _addForceCurve;
        
        private Rigidbody2D _rigidbody2D;
        
        private List<FixedForce> _forces = new List<FixedForce>();

        private Vector2 _velocity;
        public Vector2 Velocity => _velocity;
        
        private Vector2 _staticVelocity = Vector2.zero;
        public Vector2 StaticVelocity
        {
            get => _staticVelocity;
            set => _staticVelocity = value;
        }

        private void FixedUpdate()
        {
            _velocity = GetTargetVelocity();
            _rigidbody2D.velocity = _velocity + _staticVelocity;
            HandleTimeLeft();
        }

        private void Awake()
        {
            // cache references
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        /// <summary>
        /// Adds a force to the rigidbody
        /// </summary>
        /// <param name="force">
        /// Force to add
        /// </param>
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
                var value = fixedForce.Force * _addForceCurve.Evaluate(Mathf.Lerp(0, fixedForce.Duration, fixedForce.TimeLeft));
                
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

        public FixedForce(Vector2 force)
        {
            var duration = force.magnitude * 0.01f;
            
            Force = force;
            Duration = duration;
            TimeLeft = duration;
        }
    }
}
using System;

namespace TriangularAssets
{
    public class Timer
    {
        /// <summary>
        /// Action is triggered when the timer is finished (currentCooldown == 0)
        /// </summary>
        public event Action OnDone;

        private float _maxCooldown;
        private readonly bool _autoReset;
        private float _currentCooldown = 0;
        private bool _done = true;
        
        /// <summary>
        /// The time that has to pass to trigger the action
        /// </summary>
        public float MaxCooldown { get => _maxCooldown; set => _maxCooldown = value; }
        
        /// <summary>
        /// Time left to trigger the action
        /// </summary>
        public float CurrentCooldown => _currentCooldown;

        /// <summary>
        /// Creates a new timer with a max cooldown
        /// </summary>
        /// <param name="maxCooldown">
        /// The time that has to pass to trigger the action
        /// </param>
        /// <param name="autoReset">
        /// Automatically reset the timer when the action is triggered
        /// </param>
        public Timer(float maxCooldown, bool autoReset = false)
        {
            _maxCooldown = maxCooldown;
            _autoReset = autoReset;
            Reset();
        }

        /// <summary>
        /// Method handles the passing of time. Does not take into account the time scale.
        /// </summary>
        public void HandleTimerUnscaled()
        {
            _currentCooldown -= UnityEngine.Time.unscaledDeltaTime;
            if(_currentCooldown <= 0 && !_done)
            {
                OnDone?.Invoke();
                _done = true;
                
                if(_autoReset)
                    Reset();
            }
        }

        /// <summary>
        /// Method handles the passing of time. Take into account the time scale.
        /// </summary>
        public void HandleTimerScaled()
        {
            _currentCooldown -= UnityEngine.Time.deltaTime;
            if(_currentCooldown <= 0 && !_done)
            {
                OnDone?.Invoke();
                _done = true;
                
                if(_autoReset)
                    Reset();
            }
        }

        /// <summary>
        /// Forces the timer to trigger the action and set cooldown to 0
        /// </summary>
        public void ForceDone()
        {
            _currentCooldown = -1;
        }

        /// <summary>
        /// Set timer cooldown to 0, does not trigger the action
        /// </summary>
        public void ForceDoneNoEffect()
        {
            _currentCooldown = -1;
            _done = true;
        }
        
        /// <summary>
        /// Resets the timer to its max cooldown
        /// </summary>
        public void Reset()
        {
            _currentCooldown = _maxCooldown;
            _done = false;
        }

        /// <summary>
        /// Checks if the timer is done
        /// </summary>
        /// <returns>
        /// Returns true if the timer is done, false otherwise
        /// </returns>
        public bool IsDone()
        {
            return _done;
        }
    }
}
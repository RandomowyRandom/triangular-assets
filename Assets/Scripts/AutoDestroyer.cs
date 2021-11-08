using UnityEngine;

namespace TriangularAssets
{
    public class AutoDestroyer : MonoBehaviour
    {
        [SerializeField] float _timeToDestroy = 1f;
        
        void Start()
        {
            Destroy(gameObject, _timeToDestroy);
        }
    }
}
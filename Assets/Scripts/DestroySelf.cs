using UnityEngine;

namespace Assets.Scripts
{
    public class DestroySelf : MonoBehaviour
    {
        public void DestroyMe()
        {
            Destroy(gameObject);
        }
    }
}
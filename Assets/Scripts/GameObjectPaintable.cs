using UnityEngine;

namespace Assets.Scripts
{
    public class GameObjectPaintable : MonoBehaviour
    {
        public GameObject brushPrefab;

        public void Paint(Vector2 position)
        {
            if (brushPrefab != null)
            {
                GameObject brushInstance = Instantiate(brushPrefab, position, Quaternion.identity);
                brushInstance.name = brushPrefab.name;
                brushInstance.transform.parent = transform;
            }
        }
    }
}
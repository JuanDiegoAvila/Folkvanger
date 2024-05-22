using UnityEngine;

namespace Assets.Scripts
{
    public class Paintable : MonoBehaviour
    {
        public Sprite brushSprite;

        public void Paint(Vector2 position)
        {
            if (brushSprite != null)
            {
                GameObject brushInstance = new GameObject("Brush");
                brushInstance.transform.position = position;
                brushInstance.transform.parent = transform;

                SpriteRenderer spriteRenderer = brushInstance.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = brushSprite;
                spriteRenderer.sortingOrder = 3;
            }
        }
    }
}
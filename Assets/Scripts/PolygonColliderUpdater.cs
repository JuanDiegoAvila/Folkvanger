using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(PolygonCollider2D))]
    public class PolygonColliderUpdater : MonoBehaviour
    {
        private PolygonCollider2D polygonCollider;
        private SpriteRenderer spriteRenderer;

        void Start()
        {
            polygonCollider = GetComponent<PolygonCollider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            UpdatePolygonCollider();
        }

        void LateUpdate()
        {
            if (spriteRenderer.sprite != null)
            {
                UpdatePolygonCollider();
            }
        }

        void UpdatePolygonCollider()
        {
            if (polygonCollider != null && spriteRenderer != null && spriteRenderer.sprite != null)
            {
                polygonCollider.pathCount = spriteRenderer.sprite.GetPhysicsShapeCount();
                List<Vector2> path = new List<Vector2>();
                for (int i = 0; i < polygonCollider.pathCount; i++)
                {
                    path.Clear();
                    spriteRenderer.sprite.GetPhysicsShape(i, path);
                    polygonCollider.SetPath(i, path.ToArray());
                }
            }
        }

        public void MovePlayerOutOfCollider(CapsuleCollider2D otherCollider)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Collider2D playerCollider = player.GetComponent<Collider2D>();
                if (playerCollider != null && (polygonCollider.IsTouching(playerCollider) || polygonCollider.IsTouching(otherCollider)))
                {
                    Vector2 direction = (player.transform.position - transform.position).normalized;
                    // Usa una distancia fija o un pequeño multiplicador de las dimensiones del colisionador
                    Vector2 newPosition = (Vector2)player.transform.position + direction * 0.05f;
                    player.transform.position = newPosition;
                }
            }
        }
    }
}
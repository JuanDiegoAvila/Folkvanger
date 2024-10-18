using UnityEngine;

namespace Assets.Scripts
{
    public class House : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Pawn"))
            {
                if (collision.gameObject.TryGetComponent<Pawn>(out var pawn))
                {
                    pawn.PawnArrivedHome();
                }
            }
        }
    }
}
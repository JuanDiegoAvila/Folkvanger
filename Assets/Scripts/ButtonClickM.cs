using UnityEngine;

namespace Assets.Scripts
{
    public class ButtonClickM : MonoBehaviour
    {
        public Granja granja;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    ButtonClicked();
                }
            }
        }

        private void ButtonClicked()
        {
            granja.RecogerCarne();
        }
    }
}
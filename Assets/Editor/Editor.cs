using UnityEngine;
using UnityEditor;

namespace Assets.Scripts
{
    [CustomEditor(typeof(Paintable))]
    public class PaintableEditor : Editor
    {
        private Paintable paintable;

        private void OnEnable()
        {

            paintable = (Paintable)target;
        }

        private void OnSceneGUI()
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
            {
                Vector2 mousePosition = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;
                paintable.Paint(mousePosition);
            }
        }
    }

    [CustomEditor(typeof(GameObjectPaintable))]
    public class GameObjectPaintableEditor : Editor
    {
        private GameObjectPaintable paintable;

        private void OnEnable()
        {

            paintable = (GameObjectPaintable)target;
        }

        private void OnSceneGUI()
        {
            HandleUtility.AddDefaultControl(GUIUtility.GetControlID(FocusType.Passive));

            Event e = Event.current;
            if (e.type == EventType.MouseDown && e.button == 0 && !e.alt)
            {
                Vector2 mousePosition = HandleUtility.GUIPointToWorldRay(e.mousePosition).origin;
                paintable.Paint(mousePosition);
            }
        }
    }
}
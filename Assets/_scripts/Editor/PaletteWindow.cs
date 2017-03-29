using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PaletteWindow : PopupEditorWindow {

    // Add menu item
    [MenuItem("Example/Popup Example")]
    static void Init()
    {
        EditorWindow window = EditorWindow.CreateInstance<PaletteWindow>();
        window.Show();
    }

    Rect buttonRect;
    void OnGUI()
    {
        {
            GUILayout.Label("Editor window with Popup example",EditorStyles.boldLabel);
            if (GUILayout.Button("Popup Options",GUILayout.Width(200)))
            {
                PopupWindow.Show(buttonRect, new PalettePopup());
            }
            if (Event.current.type == EventType.Repaint) buttonRect = GUILayoutUtility.GetLastRect();
        }
    }

}

using System.Runtime.CompilerServices;
using GameData;
using UnityEditor;
using UnityEditor.Graphs;
using UnityEngine;

[CustomPropertyDrawer(typeof(PaletteColorAttribute))]
public class PaletteColorDrawer : PropertyDrawer
{
    private readonly GUIStyle colorPickerBox = GUI.skin.GetStyle("ColorPickerBox");
    private static int _paletteColorId;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        var colorHash = "paletteHash".GetHashCode();
        var controlId = GUIUtility.GetControlID(colorHash, FocusType.Keyboard, position);
        property.colorValue = DrawColorField(position, controlId, property.colorValue, property.name);
        EditorGUI.EndProperty();
    }

    private Color DrawColorField(Rect position, int id, Color value, string name)
    {
        position = EditorGUI.PrefixLabel(position, new GUIContent(name));
        var current = Event.current;
        var eventForControl = current.GetTypeForControl(id);
        var color1 = value;
        switch (eventForControl)
        {
            case EventType.KeyDown:
                if (current.MainActionKeyForControl(id))
                {
                    Event.current.Use();
                    EditorGUI.showMixedValue = false;
                    DrawColorPicker(position, id);
                    GUIUtility.ExitGUI();
                }
                break;
            case EventType.Repaint:
                if (_paletteColorId == id)
                {
                    var pickedColor = PalettePopup.GetLastPickedColor();
                    EditorGUIUtility.DrawColorSwatch(position, pickedColor);
                }
                else
                {
                    EditorGUIUtility.DrawColorSwatch(position, value);
                }
                colorPickerBox.Draw(position, GUIContent.none, id);
                break;
            default:
                if (eventForControl != EventType.ValidateCommand)
                {
                    if (eventForControl != EventType.ExecuteCommand)
                    {
                        if (eventForControl == EventType.mouseDown)
                        {
                            if (position.Contains(current.mousePosition))
                            {
                                switch (current.button)
                                {
                                    case 0:
                                        GUIUtility.keyboardControl = id;
                                        EditorGUI.showMixedValue = false;
                                        DrawColorPicker(position, id);
                                        GUIUtility.ExitGUI();
                                        break;
                                    case 1:
                                        GUIUtility.keyboardControl = id;
                                        GUIContent[] options1 =
                                        {
                                            new GUIContent("Copy"),
                                            new GUIContent("Paste")
                                        };
                                        EditorUtility.DisplayCustomMenu(position, options1, 0,
                                            (data, options, selected) =>
                                            {
                                                if (selected == 0)
                                                {
                                                    Debug.Log("Copy selected");
                                                    //GUIView.current.SendEvent(EditorGUIUtility.CommandEvent("Copy"));
                                                }
                                                else
                                                {
                                                    if (selected != 1)
                                                        return;
                                                    Debug.Log("Paste Selected");
                                                    //GUIView.current.SendEvent(EditorGUIUtility.CommandEvent("Paste"));
                                                }
                                            }, null);
                                        return color1;
                                }
                            }
                            break;
                        }
                        break;
                    }
                    if (GUIUtility.keyboardControl == id)
                    {
                        switch (current.commandName)
                        {
                            case "PaletteColorClicked":
                                GUI.changed = true;
                                HandleUtility.Repaint();
                                Color lastPickedColor = PalettePopup.GetLastPickedColor();
                                _paletteColorId = 0;
                                return lastPickedColor;
                        }
                        break;
                    }
                }
                switch (current.commandName)
                {
                    case "UndoRedoPerformed":
                        if (GUIUtility.keyboardControl == id)
                        {
                            Debug.Log("Reset color picker color");
                            break;
                        }
                        break;
                    case "Copy":
                    case "Paste":
                        current.Use();
                        break;
                }
                break;
        }
        return color1;
    }

    private void DrawColorPicker(Rect position, int id)
    {
        _paletteColorId = id;
        PopupWindow.Show(position, new PalettePopup());
    }

    public void SetPickedColor(Color c)
    {

    }
}

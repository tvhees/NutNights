using System;
using System.Collections;
using System.Collections.Generic;
using GameData;
using UnityEditor;
using UnityEngine;
using UnityEngine.VR.WSA;

public class PalettePopup : PopupWindowContent
{
    private Color[] primary;
    private Color[] secondary;
    private readonly GUIStyle colorPickerBox = GUI.skin.GetStyle("ColorPickerBox");

    private static Color _lastPickedColor;

    public static Color GetLastPickedColor()
    {
        return _lastPickedColor;
    }

    public override Vector2 GetWindowSize()
    {
        return new Vector2(200,150);
    }

    public override void OnGUI(Rect rect)
    {
        var yDelta = rect.height / 2;
        var swatch = new Rect(rect.position, new Vector2(rect.width, yDelta));
        DrawColorSet(primary, swatch);
        swatch = new Rect(rect.position + yDelta * Vector2.up, new Vector2(rect.width, yDelta));
        DrawColorSet(secondary, swatch);
    }

    private void DrawColorSet(Color[] set, Rect rect)
    {
        var xDelta = rect.width / set.Length;
        var size = new Vector2(xDelta, rect.height);
        var pos = rect.position;
        foreach (var c in set)
        {
            var swatchRect = new Rect(pos, size);

            var colorHash = c.ToString().GetHashCode();
            var controlId = GUIUtility.GetControlID(colorHash, FocusType.Keyboard, swatchRect);
            DrawColorSwatch(swatchRect, c, controlId);

            pos += xDelta * Vector2.right;
        }
    }

    private void DrawColorSwatch(Rect position, Color c, int id)
    {
        var currentEvent = Event.current;
        var eventForControl = Event.current.GetTypeForControl(id);
        switch (eventForControl)
        {
            case EventType.Repaint:
                EditorGUIUtility.DrawColorSwatch(position, c);
                colorPickerBox.Draw(position, GUIContent.none, id);
                break;
            case EventType.MouseDown:
                if (eventForControl == EventType.ValidateCommand || eventForControl == EventType.ExecuteCommand)
                {
                    return;
                }

                if (position.Contains(currentEvent.mousePosition))
                {
                    _lastPickedColor = c;
                    var editorAsm = typeof(Editor).Assembly;
                    var type = editorAsm.GetType("UnityEditor.InspectorWindow");
                    var window = EditorWindow.GetWindow(type);
                    window.SendEvent(EditorGUIUtility.CommandEvent("PaletteColorClicked"));
                }
                break;
        }
    }

    public override void OnOpen()
    {
        primary = GlobalSettings.Palette.Primary;
        secondary = GlobalSettings.Palette.Secondary;
    }

    public override void OnClose()
    {

    }
}

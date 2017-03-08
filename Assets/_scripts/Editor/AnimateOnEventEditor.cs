using Components;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnimateOnEvent))]
public class AnimateOnEventEditor : Editor {

    public override void OnInspectorGUI()
    {
        var script = (AnimateOnEvent)target;

        script.PropertyType = (AnimateOnEvent.PropType)EditorGUILayout.EnumPopup("Property Type", script.PropertyType);
        script.PropertyName = EditorGUILayout.TextField(new GUIContent("Property Name"), script.PropertyName);

        if (script.PropertyType == AnimateOnEvent.PropType.Bool)
        {
            DrawBoolInspector(script);
        }

        if (script.PropertyType == AnimateOnEvent.PropType.Float)
        {
            DrawFloatInspector(script);
        }

        DrawEventToggles(script);
    }

    private static void DrawBoolInspector(AnimateOnEvent script)
    {
        script.Toggle = EditorGUILayout.Toggle("Toggle Property", script.Toggle);
        if (!script.Toggle)
        {
            script.BoolValue = EditorGUILayout.Toggle("Property Value", script.BoolValue);
        }
    }

    private static void DrawFloatInspector(AnimateOnEvent script)
    {
        script.FloatValue = EditorGUILayout.FloatField("Property Value", script.FloatValue);
    }

    private static void DrawEventToggles(AnimateOnEvent script)
    {
        script.WhenPointerEnters = EditorGUILayout.Toggle("On Pointer Enter", script.WhenPointerEnters);
        script.WhenPointerClicks = EditorGUILayout.Toggle("On Pointer Click", script.WhenPointerClicks);
        script.UseCustomEvent = EditorGUILayout.Toggle("Custom Event", script.UseCustomEvent);
    }
}

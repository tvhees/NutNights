using System.IO.Ports;
using System.Runtime.CompilerServices;
using Components;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Path))]
public class PathEditor : Editor
{
    private Path path;
    private Transform pathTransform;
    private Quaternion pathRotation;

    private void GetPath()
    {
        path = target as Path;
        if (path == null)
        {
            return;
        }

        if (path.Points.Length < 2)
        {
            path.Reset();
        }
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GetPath();
        if (GUILayout.Button("Add Point")) {
            Undo.RecordObject(path, "Add Point");
            path.AddPoint();
            EditorUtility.SetDirty(path);
        }
    }

    private void OnSceneGUI()
    {
        GetPath();

        pathTransform = path.transform;
        pathRotation = Tools.pivotRotation == PivotRotation.Local ? pathTransform.rotation : Quaternion.identity;

        var points = new Vector3[path.Points.Length];
        var segmentIndices = new int[points.Length * 2 - 2];
        var index = 0;
        for (var i = 0; i < points.Length; i++)
        {
            points[i] = ShowPoint(i);

            if (i > 0)
            {
                segmentIndices[index++] = i;
                segmentIndices[index++] = i - 1;
            }
        }

        Handles.color = path.PathColor;
        Handles.DrawLines(points, segmentIndices);
    }

    private Vector3 ShowPoint(int index)
    {
        var point = pathTransform.TransformPoint(path.Points[index]);

        EditorGUI.BeginChangeCheck();
        point = Handles.DoPositionHandle(point, pathRotation);

        if (EditorGUI.EndChangeCheck()) {
            Undo.RecordObject(path, "Move Point");
            EditorUtility.SetDirty(path);
            path.Points[index] = pathTransform.InverseTransformPoint(point);
        }

        return point;
    }
}
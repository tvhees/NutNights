using System;
using System.Linq;
using UnityEngine;

namespace Components
{
    public class Path : MonoBehaviour
    {
        public Vector3[] Points = { new Vector3(0, 0, 0), new Vector3(1, 0, 0) };
        public Color PathColor = Color.white;

        public void Reset()
        {
            Points = new[] { new Vector3(0, 0, 0), new Vector3(1, 0, 0) };
        }

        public void AddPoint()
        {
            var index = Points.Length;
            var point = Points[index - 1] + Vector3.right;
            Array.Resize(ref Points, Points.Length + 1);
            Points[index] = point;
        }
    }
}
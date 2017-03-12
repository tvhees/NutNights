using System;
using System.Linq;
using UnityEngine;

namespace Components
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private Vector3[] points = { new Vector3(0, 0, 0), new Vector3(1, 0, 0) };

        public Vector3[] Points {
            get { if (points.Length < 2){ Reset(); }
                return points;
            }
        }

        public Color PathColor = Color.white;

        public void Reset()
        {
            points = new[] { new Vector3(0, 0, 0), new Vector3(1, 0, 0) };
        }

        public void AddPoint(Vector3 increment = default(Vector3))
        {
            var index = points.Length;
            var point = points[index - 1] + increment;
            Array.Resize(ref points, points.Length + 1);
            points[index] = point;
        }

        public Vector3 GetPoint(int index)
        {
            return points[index];
        }

        public void SetPoint(int index, Vector3 point)
        {
            points[index] = point;
        }

        public Vector3 GetPoint(float fractionOfPath)
        {
            var index = Mathf.RoundToInt(fractionOfPath * points.Length);
            return GetPoint(index);
        }
    }
}
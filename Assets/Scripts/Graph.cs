using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Graph : MonoBehaviour
{
    [SerializeField]
    private Transform _pointPrefab;
    [SerializeField]
    [Range(10, 100)]
    private int _resolution = 10;
    Transform[] points;
    [SerializeField]
    private GraphFunctionsName function;
    private static readonly GraphFunctions[] functions = {
        SineFunction, Sine2DFunction, MultiSineFunction, MultiSine2DFunction, Ripple, Cylinder, Sphere, Torus
    };

    // Start is called before the first frame update
    private void Awake()
    {
        float step = 2f / _resolution;
        Vector3 scale = Vector3.one * step;
        points = new Transform[_resolution * _resolution];
        for (int i = 0; i < points.Length; i++)
        {
            Transform point = Instantiate(_pointPrefab);
            point.localScale = scale;
            point.SetParent(transform, false);
            points[i] = point;
        }
    }

    private void Update()
    {
        float t = Time.time;
        GraphFunctions f = functions[(int)function];
        float step = 2f / _resolution;
        for (int i = 0, z = 0; z < _resolution; z++)
        {
            float v = (z + 0.5f) * step - 1f;
            for (int x = 0; x < _resolution; x++, i++)
            {
                float u = (x + 0.5f) * step - 1f;
                points[i].localPosition = f(u, v, t);
            }
        }
    }

    const float pi = math.PI;

    static Vector3 SineFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = math.sin(pi * (x + t));
        p.z = z;
        return p;
    }

    static Vector3 Sine2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = math.sin(pi * (x + t));
        p.y += math.sin(pi * (z + t));
        p.y *= 0.5f;
        p.z = z;
        return p;
    }

    static Vector3 MultiSineFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = math.sin(pi * (x + t));
        p.y += math.sin(2f * pi * (x + 2f * t)) / 2f;
        p.y *= 2f / 3f;
        p.z = z;
        return p;
    }

    static Vector3 MultiSine2DFunction(float x, float z, float t)
    {
        Vector3 p;
        p.x = x;
        p.y = 4f * math.sin(pi * (x + z + t / 2f));
        p.y += math.sin(pi * (x + t));
        p.y += math.sin(2f * pi * (z + 2f * t)) * 0.5f;
        p.y *= 1f / 5.5f;
        p.z = z;
        return p;
    }

    static Vector3 Ripple(float x, float z, float t)
    {
        Vector3 p;
        float d = math.sqrt(x * x + z * z);
        p.x = x;
        p.y = math.sin(pi * (4f * d - t));
        p.y /= 1f + 10f * d;
        p.z = z;
        return p;
    }

    static Vector3 Cylinder(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.8f + math.sin(pi * (6f * u + 2f * v + t)) * 0.2f;
        p.x =  r * math.sin(pi * u);
        p.y = v;
        p.z = r * math.cos(pi * u);
        return p;
    }

    static Vector3 Sphere(float u, float v, float t)
    {
        Vector3 p;
        float r = 0.8f + math.sin(pi * (6f * u + t)) * 0.1f;
        r += math.sin(pi * (4f * v + t)) * 0.1f;
        float s = r * math.cos(pi * 0.5f * v);
        p.x = s * math.sin(pi * u);
        p.y = r * math.sin(pi * 0.5f * v);
        p.z = s * math.cos(pi * u);
        return p;
    }

    static Vector3 Torus(float u, float v, float t)
    {
        Vector3 p;
        float r1 = 0.65f + math.sin(pi * (6f * u + t)) * 0.1f;
        float r2 = 0.2f + math.sin(pi * (4f * v + t)) * 0.05f;
        float s =  r2 * Mathf.Cos(pi * v) + r1;
        p.x = s * Mathf.Sin(pi * u);
        p.y = r2 * Mathf.Sin(pi *  v);
        p.z = s * Mathf.Cos(pi * u);
        return p;
    }

}

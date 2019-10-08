using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyGizmos : MonoBehaviour
{
    public Color _color = Color.yellow;
    [Range(0.1f, 0.8f)]
    public float _radius = 0.3f;
    public enum Type { SPHERE, WIRE }
    public Type type = Type.SPHERE;

    void OnDrawGizmos()
    {
        Gizmos.color = _color;
        if (type == Type.SPHERE)
            Gizmos.DrawSphere(transform.position, _radius);
        else if (type == Type.WIRE)
            Gizmos.DrawWireSphere(transform.position, _radius);
    }
}

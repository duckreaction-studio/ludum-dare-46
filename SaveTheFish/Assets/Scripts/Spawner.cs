using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private Color _color = Color.grey;
    [SerializeField]
    private string _spawnTag;
    [SerializeField]
    [Range(0.02f,20f)]
    private float force = 1f;

    public string spawnTag
    {
        get => _spawnTag;
    }

    public Color color
    {
        get => _color;
    }

    public Vector3 origin
    {
        get => transform.position;
    }

    public Vector3 backward
    {
        get => origin - transform.forward * force;
    }

    public Vector3 forward
    {
        get => origin + transform.forward * force;
    }

#if UNITY_EDITOR
    private static Mesh flag;
    public void OnDrawGizmos()
    {
        if(flag == null)
            flag = AssetDatabase.LoadAssetAtPath<Mesh>("Assets/Models/flag.fbx");
        if (flag != null)
        {
            Gizmos.color = color;
            var rotation = transform.rotation * Quaternion.Euler(0, 90, 0);
            Gizmos.DrawMesh(flag, transform.position, rotation);
        }
    }
#endif
}

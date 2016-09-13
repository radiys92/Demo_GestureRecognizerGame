using System;
using UnityEngine;

[CreateAssetMenu(fileName = "GestureTemplates", menuName = "Gesture/Templates", order = 1)]
public class GestureTemplates : ScriptableObject
{
    public GestureTemplate[] Templates;
}

[Serializable]
public class GestureTemplate
{
    public Vector2[] points;
}
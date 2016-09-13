using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class ReorderableList<T> : SimpleReorderableList
{
    public List<T> List;
}


[Serializable]
public class SimpleReorderableList { }
#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(SimpleReorderableList), true)]
public class ReorderableListDrawer : UnityEditor.PropertyDrawer
{
    private ReorderableList _list;


    private ReorderableList GetList(SerializedProperty property)
    {
        return _list ?? (_list = new ReorderableList(property.serializedObject, property, true, true, true, true)
        {
            drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                rect.width -= 40;
                rect.x += 20;
                EditorGUI.PropertyField(rect, property.GetArrayElementAtIndex(index), true);
            }
        });
    }


    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return GetList(property.FindPropertyRelative("List")).GetHeight();
    }


    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        var listProperty = property.FindPropertyRelative("List");
        var list = GetList(listProperty);
        var height = 0f;
        for (var i = 0; i < listProperty.arraySize; i++)
        {
            height = Mathf.Max(height, EditorGUI.GetPropertyHeight(listProperty.GetArrayElementAtIndex(i)));
        }
        list.elementHeight = height;
        list.DoList(position);
    }
}
#endif
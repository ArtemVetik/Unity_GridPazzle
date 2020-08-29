using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

[CustomEditor(typeof(LevelDataBase))]
public class LevelEditor : Editor
{
    [SerializeField] private GUISkin _itemToggle, _filledToggle, _playerToggle;

    private LevelDataBase _levelDataBase;
    private int _currentLevel;
    private CellType _currentCellType;

    void OnEnable()
    {
        _levelDataBase = (LevelDataBase)target;
        _currentLevel = 0;
    }

    public override void OnInspectorGUI()
    {
        ShowTitle();

        if (_levelDataBase.EmptyOrNull == false)
        {
            EditorGUILayout.BeginVertical("box");
            ShowLevel(_levelDataBase[_currentLevel]);
            EditorGUILayout.EndVertical();
        }

        if (GUI.changed)
            EditorUtility.SetDirty(_levelDataBase);
    }

    private void ShowTitle()
    {
        GUILayout.Label("Total levels: " + _levelDataBase.Count);

        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("<-", GUILayout.Width(0.1f * Screen.width)))
            _currentLevel--;
        if (GUILayout.Button("->", GUILayout.Width(0.1f * Screen.width)))
            _currentLevel++;
        if (GUILayout.Button("+", GUILayout.Width(0.1f * Screen.width)))
            _levelDataBase.Add();
        EditorGUILayout.EndHorizontal();

        _currentLevel = Mathf.Clamp(_currentLevel, 0, _levelDataBase.Count - 1);
    }

    private void ShowLevel(LevelData data)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Level " + (_levelDataBase.IndexOf(data) + 1));
        if (GUILayout.Button("-", GUILayout.Width(0.1f * Screen.width)))
            _levelDataBase.Remove(data);
        if (GUILayout.Button("Up", GUILayout.Width(0.1f * Screen.width)))
            _levelDataBase.MoveTop(data);
        if (GUILayout.Button("Down", GUILayout.Width(0.1f * Screen.width)))
            _levelDataBase.MoveDown(data);
        EditorGUILayout.EndHorizontal();

        _currentCellType = (CellType)EditorGUILayout.EnumPopup("Cell type", _currentCellType, GUILayout.Width(0.5f * Screen.width));

        var style = new GUIStyle(GUI.skin.label);
        style.normal.textColor = _itemToggle.GetStyle("toggle").normal.textColor;
        EditorGUILayout.LabelField("Item", style);
        style.normal.textColor = _playerToggle.GetStyle("toggle").normal.textColor;
        EditorGUILayout.LabelField("Player", style);
        style.normal.textColor = _filledToggle.GetStyle("toggle").normal.textColor;
        EditorGUILayout.LabelField("Filled", style);
        
        FieldInfo dataWidth = data.GetType().GetField("_width", BindingFlags.NonPublic | BindingFlags.Instance);
        FieldInfo dataHeight = data.GetType().GetField("_height", BindingFlags.NonPublic | BindingFlags.Instance);

        int newWidth = EditorGUILayout.IntField("width", (int)(dataWidth.GetValue(data)), GUILayout.Width(0.5f * Screen.width));
        int newHeight = EditorGUILayout.IntField("height", (int)(dataHeight.GetValue(data)), GUILayout.Width(0.5f * Screen.width));

        newWidth = Mathf.Clamp(newWidth, 0, int.MaxValue);
        newHeight = Mathf.Clamp(newHeight, 0, int.MaxValue);

        data.Resize(newWidth, newHeight);

        FieldInfo arrayData = data.GetType().GetField("_grid", BindingFlags.NonPublic | BindingFlags.Instance);
        CellType[] a = (CellType[])arrayData.GetValue(data);

        for (int y = 0; y < newHeight; y++)
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < newWidth; x++)
            {
                bool toggleValueBefore = data[y, x] != CellType.Empty;
                bool toggleValueAfter = EditorGUILayout.Toggle(toggleValueBefore, SkinByType(data[y, x]).GetStyle("toggle"));

                if (toggleValueBefore == false && toggleValueAfter == true)
                    a[y * data.Size.x + x] = _currentCellType;
                if (toggleValueAfter == false)
                    a[y * data.Size.x + x] = CellType.Empty;
            }
            EditorGUILayout.EndHorizontal();
        }
    }

    private GUISkin SkinByType(CellType type)
    {
        switch (type)
        {
            case CellType.Empty:
            case CellType.Item:
                return _itemToggle;
            case CellType.Filled:
                return _filledToggle;
            case CellType.Player:
                return _playerToggle;
            default:
                throw new ArgumentException();
        }
    }
}

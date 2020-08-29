using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Paddings
{
    [SerializeField, Range(0f, 0.5f)] private float _leftPadding;
    [SerializeField, Range(0f, 0.5f)] private float _rightPadding;
    [SerializeField, Range(0f, 0.5f)] private float _topPadding;
    [SerializeField, Range(0f, 0.5f)] private float _downPadding;

    public float LeftPadding => _leftPadding;
    public float RightPadding => _rightPadding;
    public float TopPadding => _topPadding;
    public float DownPadding => _downPadding;
}
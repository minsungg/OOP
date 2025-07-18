﻿using Plugins.Lowscope.ComponentSaveSystem.Components.Scriptableobjects;
using UnityEditor;
using UnityEngine;

namespace Plugins.Lowscope.ComponentSaveSystem.Editor
{
    // Code based off https://github.com/roboryantron/Unite2017/blob/master/Assets/Code/Variables/Editor/FloatReferenceDrawer.cs
    // Licence: https://github.com/roboryantron/Unite2017/blob/master/LICENSE
    // ----------------------------------------------------------------------------
    // Unite 2017 - Game Architecture with Scriptable Objects
    // 
    // Author: Ryan Hipple
    // Date:   10/04/17
    // ----------------------------------------------------------------------------

    [CustomPropertyDrawer(typeof(SaveIdentifierReference))]
    public class SaveIdentifierReferenceDrawer : PropertyDrawer
    {
        /// <summary>
        /// Options to display in the popup to select constant or variable.
        /// </summary>
        private readonly string[] popupOptions =
            { "Use Constant", "Use Variable", "Create + Use Variable" };

        /// <summary> Cached style to use to draw the popup button. </summary>
        private GUIStyle popupStyle;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (popupStyle == null)
            {
                popupStyle = new GUIStyle(GUI.skin.GetStyle("PaneOptions"));
                popupStyle.imagePosition = ImagePosition.ImageOnly;
            }

            label = EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, label);

            EditorGUI.BeginChangeCheck();

            // Get properties
            SerializedProperty useConstant = property.FindPropertyRelative("UseConstant");
            SerializedProperty constantValue = property.FindPropertyRelative("ConstantValue");
            SerializedProperty variable = property.FindPropertyRelative("Variable");

            // Calculate rect for configuration button
            Rect buttonRect = new Rect(position);
            buttonRect.yMin += popupStyle.margin.top;
            buttonRect.width = popupStyle.fixedWidth + popupStyle.margin.right;
            position.xMin = buttonRect.xMax;

            // Store old indent level and set it to 0, the PrefixLabel takes care of it
            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            int result = EditorGUI.Popup(buttonRect, useConstant.boolValue ? 0 : 1, popupOptions, popupStyle);

            useConstant.boolValue = result == 0;

            if (result == 2)
            {
                if (string.IsNullOrEmpty(constantValue.stringValue))
                {
                    Debug.LogError("Please create an ID for the component before creating a new asset");
                }
                else
                {
                    variable.objectReferenceValue = SaveIdentifierVariable.CreateSaveIdentifierAsset(constantValue.stringValue) as Object;
                }
            }

            EditorGUI.PropertyField(position,
                useConstant.boolValue ? constantValue : variable,
                GUIContent.none);

            if (EditorGUI.EndChangeCheck())
                property.serializedObject.ApplyModifiedProperties();

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
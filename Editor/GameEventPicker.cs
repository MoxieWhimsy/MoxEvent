#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace Mox.Events.Editor
{
    [CustomPropertyDrawer(typeof(AGameEvent), false)]
    public class GameEventPicker : PropertyDrawer
    {
        private const int BackButtonWidth = 40;
        private const int SearchButtonWidth = 50;
        private static System.Type[] s_gameEventTypes = { };
        private readonly List<AGameEvent> _gameEvents = new();
        private int _typeIndex = 0;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position = EditorGUI.PrefixLabel(position, label);
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            
            var lineHeight = position.height;
            var typeRect = new Rect(position.x, position.y, position.width - SearchButtonWidth, lineHeight);
            var searchButtonRect = new Rect(position.x + typeRect.width, position.y, SearchButtonWidth, lineHeight);
            
            var assetsRect = new Rect(position.x, position.y, position.width - BackButtonWidth, lineHeight);
            var backButtonRect = new Rect(position.x + position.width - BackButtonWidth, position.y, BackButtonWidth, lineHeight);
            var eventTypeName = s_gameEventTypes[_typeIndex].Name;
            var objectValue = property.objectReferenceValue;
            var eventType = objectValue ? objectValue.GetType() : s_gameEventTypes[_typeIndex];

            if (objectValue || _gameEvents.Count > 0)
            {
                property.objectReferenceValue = EditorGUI.ObjectField(assetsRect, objectValue, eventType, false);
                if (GUI.Button(backButtonRect, "back"))
                {
                    property.objectReferenceValue = null;
                    _gameEvents.Clear();
                }
            }
            else
            {
                _typeIndex = EditorGUI.Popup(typeRect, _typeIndex, s_gameEventTypes.Select(s => s.Name).ToArray());
                if (GUI.Button(searchButtonRect, "search"))
                {
                    _gameEvents.Clear();
                    _gameEvents.AddRange(AssetDatabase.FindAssets($"t:{eventTypeName}").Select(guid
                        => AssetDatabase.LoadAssetAtPath<AGameEvent>(AssetDatabase.GUIDToAssetPath(guid))));
                }
            }

            EditorGUI.indentLevel = indent;
        }


        [DidReloadScripts]
        private static void OnRecompile()
        {
            var assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var filteredTypes = types.Where(type
                => type.IsSubclassOf(typeof(AGameEvent)) && !type.ContainsGenericParameters);
            s_gameEventTypes = filteredTypes.ToArray();
        }
    }
}
#endif
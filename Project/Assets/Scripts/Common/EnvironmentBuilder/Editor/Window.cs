using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Common.EnvironmentBuilder.Editor
{
    internal class Window : EditorWindow
    {
        private const float MinWidth = 200;
        private const float WindowSize = 512;

        private static Data _data;

        private Texture2D _targetTexture;
        private Texture _oldTargetTexture;
        private float _scale;

        private Data.EnvironmentPositions _currentPositions;

        [MenuItem("Tools/EnvironmentBuilderWindow")]
        private static void Init()
        {
            var window = GetWindow(typeof(Window), true, "Builder");
            window.position = new Rect(Screen.width / 2f, Screen.height / 2f, 400, 200);
            window.Show();

            var asset = AssetDatabase.FindAssets("EnvironmentData");
            var path = AssetDatabase.GUIDToAssetPath(asset.First());
            _data = AssetDatabase.LoadAssetAtPath<Data>(path);
        }

        public void OnGUI()
        {
            MouseDownCheck();

            _targetTexture = GetSelectedTexture();

            if (!_targetTexture)
            {
                EditorGUILayout.TextArea("Select texture in assests folder");
                return;
            }

            if (_oldTargetTexture != _targetTexture)
            {
                UpdateWindowDimensions();
                _oldTargetTexture = _targetTexture;
                var hashCode = _targetTexture.GetHashCode();
                _currentPositions = _data.GetByIdOrCreateNew(hashCode);
            }

            DrawPreview();
        }

        private static Texture2D GetSelectedTexture()
        {
            if (Selection.objects == null || Selection.objects.Length == 0)
                return null;

            var path = AssetDatabase.GetAssetPath(Selection.activeObject.GetInstanceID());
            return AssetDatabase.LoadAssetAtPath<Texture2D>(path);
        }

        private void UpdateWindowDimensions()
        {
            _scale = _targetTexture.width / WindowSize;
            var pos = position;
            pos.width = Mathf.Max(_targetTexture.width, MinWidth) / _scale;
            pos.height = _targetTexture.height / _scale;
            position = pos;
            Focus();
            Repaint();
        }

        private void DrawPreview()
        {
            GUI.color = Color.white;
            var w = Mathf.RoundToInt(_targetTexture.width / _scale);
            var h = Mathf.RoundToInt(_targetTexture.height / _scale);
            var rect = new Rect(0, 0, w, h);
            var uv = new Rect(0f, 0f, w, h);
            uv = ConvertToTexCoords(uv, w, h);

            GUI.DrawTextureWithTexCoords(rect, _targetTexture, uv, true);
        }

        private static Rect ConvertToTexCoords(Rect rect, int width, int height)
        {
            var final = rect;
            if (Mathf.Approximately(width, 0f) || Mathf.Approximately(height, 0f))
                return final;

            final.xMin = rect.xMin / width;
            final.xMax = rect.xMax / width;
            final.yMin = 1f - rect.yMax / height;
            final.yMax = 1f - rect.yMin / height;

            return final;
        }

        private void MouseDownCheck()
        {
            var controlID = GUIUtility.GetControlID(FocusType.Passive);
            var eventType = Event.current.GetTypeForControl(controlID);
            if (eventType != EventType.MouseDown)
                return;

            _currentPositions.Positions.Add(
                ConvertTextureCoordToWorldPosition(Event.current.mousePosition));
        }

        private static Vector2 ConvertTextureCoordToWorldPosition(Vector2 texCoord) =>
            new(texCoord.x / 100, texCoord.y / 100);
    }
}
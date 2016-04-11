using UnityEngine;
using UnityEditor;
using System;
using System.Collections;

[CustomEditor(typeof(SavedProjectSettings))]
public class ProjectSettingsEditor : Editor 
{
	////////////////////////////////////////////////////////////////////////////////////////////////
	/// Asset creation

	private const string ASSET_PATH = "Assets/ProjectSettings/SavedProjectSettings.asset";

	[MenuItem ("Window/CommonModules/ProjectSettings")]
	public static void Show()
	{
		SavedProjectSettings asset = AssetDatabase.LoadAssetAtPath<SavedProjectSettings>(ASSET_PATH);

		if(asset == null) {
			asset = ScriptableObject.CreateInstance<SavedProjectSettings>();

			AssetDatabase.CreateAsset(asset, ASSET_PATH);
			AssetDatabase.SaveAssets();
		}

		Selection.activeObject = asset;
	}
	////////////////////////////////////////////////////////////////////////////////////////////////

	public override void OnInspectorGUI()
	{
		SavedProjectSettings myTarget = (SavedProjectSettings)target;

		if(myTarget == null)
			return;

		DrawDownDrop<RenderingPath>(PlayerSettings.renderingPath.ToString(), ref myTarget._renderingPath);

	}


//	private void DrawDownDrop(string currentVal,ref RenderingPath myVar)
//	{
//		EditorGUILayout.BeginHorizontal();
//
//		EditorGUILayout.LabelField(myVar.GetType().Name);
//
//		EditorGUI.BeginDisabledGroup(true);
//		EditorGUILayout.LabelField(currentVal);
//		EditorGUI.EndDisabledGroup();
//
//		myVar = (RenderingPath)EditorGUILayout.EnumPopup(((Enum)myVar));
//		EditorGUILayout.EndHorizontal();
//	}

	private void DrawDownDrop<T>(string currentVal,ref T myVar)
	{
		EditorGUILayout.BeginHorizontal(GUI.skin.customStyles[0], GUILayout.Width(100));

		EditorGUILayout.LabelField(myVar.GetType().Name);

		EditorGUI.BeginDisabledGroup(true);
		EditorGUILayout.LabelField(currentVal);
		EditorGUI.EndDisabledGroup();

		myVar = (T)Enum.ToObject(typeof(T),EditorGUILayout.EnumPopup((Enum)(object)myVar));
		EditorGUILayout.EndHorizontal();
	}

	private GUIStyle FixedWidthStyle()
	{
		GUIStyle s = new GUIStyle();
		s.fixedWidth = 100;
		return s;
	}
}

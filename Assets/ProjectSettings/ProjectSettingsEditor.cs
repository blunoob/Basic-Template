/* 		
		Author : Farhan
		Skype : farhan.blu
		Email : farhan.blu@gmail.com
*/

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

		//Any configuration added in 'SavedProjectSettings', make sure to write its code below so that it shows in the inspector.
		DrawDownDrop<RenderingPath>(PlayerSettings.renderingPath.ToString(), ref myTarget._renderingPath);
		DrawDownDrop<ColorSpace>(PlayerSettings.colorSpace.ToString(), ref myTarget._colorSpace);

		if(GUILayout.Button("Apply Settings"))
			myTarget.Apply();
	}


	private void DrawDownDrop<T>(string currentVal, ref T myVar)
	{
		EditorGUILayout.BeginVertical(GUI.skin.customStyles[Toggle() ? 3 : 7]);

		EditorGUILayout.LabelField("Setting : " + myVar.GetType().Name);

		EditorGUI.BeginDisabledGroup(true);
		EditorGUILayout.LabelField("Current : " + currentVal);
		EditorGUI.EndDisabledGroup();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Saved Setting : ");
		myVar = (T)Enum.ToObject(typeof(T),EditorGUILayout.EnumPopup((Enum)(object)myVar));
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.EndVertical();
	}


	protected bool _toggle;
	protected bool Toggle()
	{
		_toggle = !_toggle;
		return _toggle;
	}
}

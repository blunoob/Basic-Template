using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class LocalizationEditor : EditorWindow 
{
	private const string SETTINGS_ASSET_KEY = "LocalizationSettings";
	private const string ASSET_PATH = "Assets/Localization/LocalizationSettings.asset";

	[MenuItem ("Window/LocalizationManager")]
	public static void Show()
	{
		EditorWindow.GetWindow<LocalizationEditor>(SETTINGS_ASSET_KEY);
		LocalizationSettings asset = AssetDatabase.LoadAssetAtPath<LocalizationSettings>(ASSET_PATH);

		if(asset == null) {
			asset = ScriptableObject.CreateInstance<LocalizationSettings>();
			asset.LoadFromFile();

			AssetDatabase.CreateAsset(asset, ASSET_PATH);
			AssetDatabase.SaveAssets();
		}

		EditorUtility.FocusProjectWindow();
		EditorPrefs.SetString(SETTINGS_ASSET_KEY, AssetDatabase.GetAssetPath(asset));
		Selection.activeObject = asset;
	}

	protected void OnGUI ()
	{
		LocalizationSettings localeSettings = AssetDatabase.LoadAssetAtPath<LocalizationSettings>(EditorPrefs.GetString(SETTINGS_ASSET_KEY));

		if(localeSettings._keyToDefaultDictionary == null)
			localeSettings.LoadFromFile();

		List<KeyValuePair<string, string>> list = localeSettings._keyToDefaultDictionary.ToList();

		EditorGUILayout.LabelField("Defined Keys");
		EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Keys");
		EditorGUILayout.LabelField("Default Values");
		EditorGUILayout.EndHorizontal();

		for(int i = 0; i < list.Count; i++)
		{
			EditorGUILayout.BeginHorizontal();

			KeyValuePair<string, string> pair = list[i];
			string key = EditorGUILayout.TextField(list[i].Key);
			string value = EditorGUILayout.TextField(list[i].Value);

			list[i] = new KeyValuePair<string, string>(key, value);

			EditorGUILayout.EndHorizontal();
		}

		if(GUILayout.Button("Add Key", SimpleButtonStyle()))
			list.Add(new KeyValuePair<string, string>("Key", "Value"));



		if(GUILayout.Button("Save"))
		{
			for(int i = 0; i < list.Count; i++)
			{
				if(string.IsNullOrEmpty(list[i].Key))
					list.RemoveAt(i);
			}

			AssetDatabase.SaveAssets();
			localeSettings.WriteToFile();
		}

		localeSettings._keyToDefaultDictionary = list.Distinct().ToDictionary(pair => pair.Key, pair => pair.Value);
	}

	protected GUIStyle SimpleButtonStyle()
	{
		GUIStyle buttonStyle = GUI.skin.button;
		buttonStyle.fixedHeight = 30;
		buttonStyle.fixedWidth = 100;
		return buttonStyle;
	}
}

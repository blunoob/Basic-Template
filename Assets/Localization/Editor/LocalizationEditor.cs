/* 		
		Author : Farhan
		Skype : farhan.blu
		Email : farhan.blu@gmail.com
*/

using System;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This custom editor kicks in when you go to localizationManager from Window>LocalizationManager. 
/// The editor lets you add more languages for localization, as well as define the localized text.
/// </summary>
public class LocalizationEditor : EditorWindow 
{
	private const string SETTINGS_ASSET_KEY = "LocalizationSettings";
	private const string ASSET_PATH = "Assets/Localization/LocalizationSettings.asset";

	private Vector2 _mainScrollPos = new Vector2(10, 10);

	protected static EditorWindow _mainWindow;

	[MenuItem ("Window/LocalizationManager")]
	public static void Show()
	{
		_mainWindow = EditorWindow.GetWindow<LocalizationEditor>(SETTINGS_ASSET_KEY);

		LocalizationSettings asset = AssetDatabase.LoadAssetAtPath<LocalizationSettings>(ASSET_PATH);

		if(asset == null) {
			asset = ScriptableObject.CreateInstance<LocalizationSettings>();
			asset.LoadFromFile();

			AssetDatabase.CreateAsset(asset, ASSET_PATH);
			AssetDatabase.SaveAssets();
		}

		_mainWindow.Focus();

		EditorPrefs.SetString(SETTINGS_ASSET_KEY, AssetDatabase.GetAssetPath(asset));
		Selection.activeObject = asset;
	}

	protected void OnGUI ()
	{
		_mainScrollPos = EditorGUILayout.BeginScrollView(_mainScrollPos);

		LocalizationSettings localeSettings = AssetDatabase.LoadAssetAtPath<LocalizationSettings>(EditorPrefs.GetString(SETTINGS_ASSET_KEY));
		ManageDefaultDictionary(localeSettings);

		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();

		ManageLanguages(localeSettings);

		EditorGUILayout.EndScrollView();
	}


	protected void ManageDefaultDictionary(LocalizationSettings localeSettings)
	{
		if(localeSettings._keyToDefaultDictionary == null)
			localeSettings.LoadFromFile();

		List<KeyValuePair<string, string>> list = localeSettings._keyToDefaultDictionary.ToList();

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Defined Keys", SmallHeadingStyle());
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


		localeSettings._keyToDefaultDictionary = list.Distinct().ToDictionary(pair => pair.Key, pair => pair.Value);

		if(GUILayout.Button("Save"))
		{
			for(int i = 0; i < list.Count; i++)
			{
				if(string.IsNullOrEmpty(list[i].Key))
					list.RemoveAt(i);
			}

			localeSettings._keyToDefaultDictionary = list.Distinct().ToDictionary(pair => pair.Key, pair => pair.Value);
			AssetDatabase.SaveAssets();
			localeSettings.WriteToFile();
		}

	}

	protected void ManageLanguages(LocalizationSettings localeSettings)
	{
		EditorGUILayout.BeginHorizontal();
		ShowAddLanguageMenu(localeSettings);
		EditorGUILayout.Space();
		ShowAddedLanguages(localeSettings);
		EditorGUILayout.EndHorizontal();
	}

	protected bool _toggle;
	protected bool Toggle()
	{
		_toggle = !_toggle;
		return _toggle;
	}

	protected void ShowAddLanguageMenu (LocalizationSettings localeSettings)
	{
		EditorGUILayout.BeginVertical(GUI.skin.customStyles[9], GUILayout.Width(300));
		EditorGUILayout.LabelField("Available languages", SmallHeadingStyle());
		EditorGUILayout.Space();
		_languagesScrollPos = GUILayout.BeginScrollView(_languagesScrollPos, false, true, GUILayout.Width(300), 
			GUILayout.Height(240));

		SystemLanguage [] systemLanguages = (SystemLanguage []) Enum.GetValues(typeof(SystemLanguage));
		for(int i = 0; i < systemLanguages.Length; i++)
		{
			if(localeSettings._localizedLanguages.Contains(systemLanguages[i]))
				continue;
			
			EditorGUILayout.BeginHorizontal(GUI.skin.customStyles[Toggle() ? 3 : 9]);

			EditorGUILayout.LabelField(systemLanguages[i].ToString());
			if(GUILayout.Button("Add", SmallButtonStyle()))
			{
				if(!localeSettings._localizedLanguages.Contains(systemLanguages[i])) {
					localeSettings.AddLanguage(systemLanguages[i]);
				}

				EditorUtility.SetDirty(localeSettings);
				AssetDatabase.SaveAssets();
				AssetDatabase.Refresh();
			}
				
			EditorGUILayout.EndHorizontal();
		}

		GUILayout.EndScrollView();
		EditorGUILayout.EndVertical();
	}

	private void SaveAsset(UnityEngine.Object obj)
	{
		EditorUtility.SetDirty(obj);
		AssetDatabase.SaveAssets();
	}

	protected void ShowAddedLanguages (LocalizationSettings localeSettings)
	{
		// TODO ... Show menu for 1. removing a language 2. Updating a language ...
		EditorGUILayout.BeginVertical();

		EditorGUILayout.LabelField("Defined Languages :", SmallHeadingStyle());

		EditorGUILayout.Space();

		_myLanguagesScrollPos = GUILayout.BeginScrollView(_myLanguagesScrollPos, false, false, GUILayout.Width(400), 
			GUILayout.Height(240));
		
		if(localeSettings._localizedLanguages.Count == 0) {
			EditorGUILayout.LabelField("None");
			EditorGUILayout.Space();
		}

		for(int i = 0; i < localeSettings._localizedLanguages.Count; i++)
		{
			EditorGUILayout.BeginHorizontal();
			EditorGUILayout.LabelField(localeSettings._localizedLanguages[i].ToString());
			if(GUILayout.Button("Edit", SmallButtonStyle()))
			{
				LanguageEditor e = EditorWindow.GetWindow<LanguageEditor>(localeSettings._localizedLanguages[i].ToString(), 
					true, typeof(LocalizationEditor));
				
				e.LoadWithSettings(localeSettings._localizedLanguages[i], localeSettings);
				// TODO ... Open a tab and allow editing a language...
			}

			if(GUILayout.Button("Remove", SmallButtonStyle()))
			{
				localeSettings.RemoveLanguage(localeSettings._localizedLanguages[i]);
				SaveAsset(localeSettings);
				AssetDatabase.Refresh();
			}
			EditorGUILayout.EndHorizontal();
		}
		GUILayout.EndScrollView();
		EditorGUILayout.EndVertical();
	}

	protected Vector2 _languagesScrollPos = new Vector2(40, 160);
	protected Vector2 _myLanguagesScrollPos = new Vector2(40, 160);


	public static GUIStyle SmallHeadingStyle()
	{
		GUIStyle s = new GUIStyle();
		s.fontSize = 14;
		s.fontStyle = FontStyle.Bold;
		return s;
	}



	public static GUIStyle SimpleButtonStyle()
	{
		GUIStyle buttonStyle = GUI.skin.button;
		buttonStyle.fixedHeight = 30;
		buttonStyle.fixedWidth = 100;
		return buttonStyle;
	}

	public static GUIStyle SmallButtonStyle()
	{
		GUIStyle buttonStyle = GUI.skin.button;
		buttonStyle.fixedHeight = 20;
		buttonStyle.fixedWidth = 60;
		return buttonStyle;
	}
}

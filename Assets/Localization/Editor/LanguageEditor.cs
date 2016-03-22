using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class LanguageEditor : EditorWindow 
{
	protected SystemLanguage _lang;
	protected LocalizationSettings _localeSettings;
	protected Dictionary<string, string> _languageDictionary;

	private Vector2 _mainScrollPos = new Vector2(10, 10);


	public void LoadWithSettings(SystemLanguage lang, LocalizationSettings localeSettings)
	{
		_lang = lang;
		_localeSettings = localeSettings;
		_languageDictionary = _localeSettings.GetLanguageDictionary(_lang);

		this.titleContent = new GUIContent(_lang.ToString());
	}


	protected void OnGUI()
	{
		if(_localeSettings == null || _languageDictionary == null) {
			Close();
			return;
		}

		_mainScrollPos = EditorGUILayout.BeginScrollView(_mainScrollPos);

		List<KeyValuePair<string, string>> list = _languageDictionary.ToList();

		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Defined Keys", LocalizationEditor.SmallHeadingStyle());
		EditorGUILayout.Space();

		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Keys", GUILayout.Width(200));
		EditorGUILayout.LabelField("Values");
		EditorGUILayout.EndHorizontal();

		for(int i = 0; i < list.Count; i++)
		{
			EditorGUILayout.BeginHorizontal();

			KeyValuePair<string, string> pair = list[i];
			EditorGUILayout.LabelField(pair.Key, GUILayout.Width(200));
			string val = EditorGUILayout.TextField(pair.Value);
			list[i] = new KeyValuePair<string, string>(pair.Key, val);

			EditorGUILayout.EndHorizontal();
		}

		_languageDictionary = list.ToDictionary(pair => pair.Key, pair => pair.Value);

		if(GUILayout.Button("Save", LocalizationEditor.SimpleButtonStyle()))
		{
			_localeSettings.SaveLanguage(_lang, _languageDictionary);
		}

		EditorGUILayout.EndScrollView();
	}
}

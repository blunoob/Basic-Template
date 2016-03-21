using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LocalizationSettings : ScriptableObject
{
	private const string SAVED_LANG_KEY = "SavedLanguages";
	private const string DEFAULTS_FILE = "Assets/Localization/Data/Defaults.txt";

	[SerializeField]
	public Dictionary<string, string> _keyToDefaultDictionary;

	public void AddLanguage(SystemLanguage lang)
	{
		List<string> savedLangauges = JsonUtility.FromJson<List<string>>(EditorPrefs.GetString(SAVED_LANG_KEY));
		savedLangauges.Add(lang.ToString());
		EditorPrefs.SetString(SAVED_LANG_KEY, JsonUtility.ToJson(savedLangauges));
	}

	public List<string> GetSavedLanguages()
	{
		List<string> languages = new List<string>();
		string prefsData = EditorPrefs.GetString(SAVED_LANG_KEY, string.Empty);
		if(string.IsNullOrEmpty(prefsData))
			return languages;
		return JsonUtility.FromJson<List<string>>(EditorPrefs.GetString(SAVED_LANG_KEY));
	}

	public void WriteToFile()
	{
		FileIO.WriteToPath(DEFAULTS_FILE, MiniJSON.Json.Serialize(_keyToDefaultDictionary));
	}

	public void LoadFromFile()
	{
		IDictionary defaultsDictionary = MiniJSON.Json.Deserialize(FileIO.ReadFromPath(DEFAULTS_FILE)) as IDictionary;
		_keyToDefaultDictionary = defaultsDictionary.ToDictionary<string, string>();
	}
}

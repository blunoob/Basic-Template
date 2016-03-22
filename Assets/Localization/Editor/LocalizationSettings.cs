using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LocalizationSettings : ScriptableObject
{
	private const string SAVED_LANG_KEY = "SavedLanguages";
	private const string DATA_PATH = "Assets/Localization/Data/";
	private const string DEFAULTS_FILE = "Defaults.txt";
	private const string FILE_SUFFIX = ".txt";

	[SerializeField]
	public Dictionary<string, string> _keyToDefaultDictionary;

	public List<SystemLanguage> _localizedLanguages;

	public void AddLanguage(SystemLanguage lang)
	{
		if(_localizedLanguages == null)
			_localizedLanguages = new List<SystemLanguage>();

		if(!_localizedLanguages.Contains(lang))
			_localizedLanguages.Add(lang);



		FileIO.WriteToPath(DATA_PATH + lang.ToString() + FILE_SUFFIX, MiniJSON.Json.Serialize(_keyToDefaultDictionary.DuplicateKeys()));
		// TODO ... JsonUtility sucks for this purpose... need to replace with MiniJSON
	}

	public void RemoveLanguage(SystemLanguage lang)
	{
		if(_localizedLanguages.Contains(lang)) {
			_localizedLanguages.Remove(lang);
			FileIO.DeleteFile(DATA_PATH + lang.ToString() + FILE_SUFFIX);
		}
	}

	public List<string> GetSavedLanguages()
	{
		// TODO ... JsonUtility sucks for this purpose... need to replace with MiniJSON
		List<string> languages = new List<string>();
		string prefsData = EditorPrefs.GetString(SAVED_LANG_KEY, string.Empty);
		if(string.IsNullOrEmpty(prefsData))
			return languages;
		return JsonUtility.FromJson<List<string>>(EditorPrefs.GetString(SAVED_LANG_KEY));
	}


	public void WriteToFile()
	{
		FileIO.WriteToPath(DATA_PATH + DEFAULTS_FILE, MiniJSON.Json.Serialize(_keyToDefaultDictionary));
	}


	public void LoadFromFile()
	{
		IDictionary defaultsDictionary = MiniJSON.Json.Deserialize(FileIO.ReadFromPath(DATA_PATH + DEFAULTS_FILE)) as IDictionary;
		_keyToDefaultDictionary = defaultsDictionary.ToDictionary<string, string>();
	}
}

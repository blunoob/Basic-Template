using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class LocalizationSettings : ScriptableObject
{
	private const string SAVED_LANG_KEY = "SavedLanguages";
	public const string DATA_PATH = "Assets/Resources/Localization/Data/";
	private const string DEFAULTS_FILE = "Defaults";
	private const string FILE_SUFFIX = ".txt";

	public Dictionary<string, string> _keyToDefaultDictionary;

	[HideInInspector]
	public List<SystemLanguage> _localizedLanguages;

	public void AddLanguage(SystemLanguage lang)
	{
		if(_localizedLanguages == null)
			_localizedLanguages = new List<SystemLanguage>();

		if(!_localizedLanguages.Contains(lang))
			_localizedLanguages.Add(lang);

		FileIO.WriteToPath(DATA_PATH + lang.ToString() + FILE_SUFFIX, MiniJSON.Json.Serialize(_keyToDefaultDictionary.DuplicateKeys()));
	}

	public string GetDefaultValue(string key)
	{
		if(_keyToDefaultDictionary.ContainsKey(key))
			return _keyToDefaultDictionary[key];
		return null;
	}

	public void RemoveLanguage(SystemLanguage lang)
	{
		if(_localizedLanguages.Contains(lang)) {
			_localizedLanguages.Remove(lang);
			FileIO.DeleteFile(DATA_PATH + lang.ToString() + FILE_SUFFIX);
		}
	}


	public Dictionary<string, string> GetLanguageDictionary(SystemLanguage lang)
	{
		#if UNITY_EDITOR
		string fileContent = FileIO.ReadFromPath(DATA_PATH + lang.ToString() + FILE_SUFFIX);
		Debug.Log(Application.dataPath);
		#else
		string fileContent = FileIO.ReadTextFromResources("Localization/Data/"+lang.ToString());
		#endif

		if(fileContent == null) {
			Debug.LogWarning("File Not found. Using Defaults");
			return _keyToDefaultDictionary;
		}

		IDictionary langDictionary = MiniJSON.Json.Deserialize(fileContent) as IDictionary;
		return langDictionary.ToDictionary<string, string>();
	}


	public void SaveLanguage(SystemLanguage lang, Dictionary<string, string> dictionary)
	{
		FileIO.WriteToPath(DATA_PATH + lang.ToString() + FILE_SUFFIX, MiniJSON.Json.Serialize(dictionary));
	}


	public void WriteToFile()
	{
		FileIO.WriteToPath(DATA_PATH + DEFAULTS_FILE + FILE_SUFFIX, MiniJSON.Json.Serialize(_keyToDefaultDictionary));
	}


	public void LoadFromFile()
	{
		#if UNITY_EDITOR
		string fileContent = FileIO.ReadFromPath(DATA_PATH + DEFAULTS_FILE + FILE_SUFFIX);
		#else
		string fileContent = FileIO.ReadTextFromResources("Localization/Data/" + DEFAULTS_FILE);
		#endif
		if(fileContent == null) {
			Debug.LogError("Default file not found!");
			return;
		}
		IDictionary defaultsDictionary = MiniJSON.Json.Deserialize(fileContent) as IDictionary;
		_keyToDefaultDictionary = defaultsDictionary.ToDictionary<string, string>();
	}
}

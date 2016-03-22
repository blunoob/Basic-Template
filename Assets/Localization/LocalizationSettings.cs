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
	private const string ASSET_PATH = "Assets/Localization/LocalizationSettings.asset";

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
	}

	public static LocalizationSettings GetSettings()
	{
		LocalizationSettings asset = AssetDatabase.LoadAssetAtPath<LocalizationSettings>(ASSET_PATH);

		if(asset == null) {
			asset = ScriptableObject.CreateInstance<LocalizationSettings>();
			asset.LoadFromFile();

			AssetDatabase.CreateAsset(asset, ASSET_PATH);
			AssetDatabase.SaveAssets();
		}

		return asset;
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
		string fileContent = FileIO.ReadFromPath(DATA_PATH + lang.ToString() + FILE_SUFFIX);

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
		FileIO.WriteToPath(DATA_PATH + DEFAULTS_FILE, MiniJSON.Json.Serialize(_keyToDefaultDictionary));
	}


	public void LoadFromFile()
	{
		string fileContent = FileIO.ReadFromPath(DATA_PATH + DEFAULTS_FILE);
		if(fileContent == null) {
			Debug.LogError("Default file not found!");
			return;
		}
		IDictionary defaultsDictionary = MiniJSON.Json.Deserialize(fileContent) as IDictionary;
		_keyToDefaultDictionary = defaultsDictionary.ToDictionary<string, string>();
	}
}

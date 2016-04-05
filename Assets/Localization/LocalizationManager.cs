using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocalizationManager : MonoBehaviour 
{
	public LocalizationSettings _settings;

	public static LocalizationManager _instance	{	get;	private set;	}

	protected void Awake()
	{
		if(_instance == null) {
			_instance = this;
			DontDestroyOnLoad(gameObject);
		} else
			Destroy(gameObject);
	}

	protected Dictionary<string, string> _languageDictionary;

	public void LoadLanguage()
	{
		LoadLanguage(Application.systemLanguage);
	}


	public void LoadLanguage(SystemLanguage language)
	{
		_settings.LoadFromFile();
		_languageDictionary = _settings.GetLanguageDictionary(language);
	}


	public string GetLocalizedString(string str)
	{
		if(!_languageDictionary.ContainsKey(str)) {
			Debug.LogWarning("Given string '" + str + "' has no predefined key.");
			return str;
		}
		return _languageDictionary[str];
	}

}

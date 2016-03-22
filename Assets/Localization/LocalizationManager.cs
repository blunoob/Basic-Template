using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LocalizationManager : MonoBehaviour 
{
	private static LocalizationManager instance;
	public static LocalizationManager _instance
	{
		get
		{
			if(instance == null) {
				GameObject localeObj = new GameObject("LocalizationManager");
				DontDestroyOnLoad(localeObj);

				instance = localeObj.AddComponent<LocalizationManager>();
			}

			return instance;
		}
	}

	protected Dictionary<string, string> _languageDictionary;


	public void LoadLanguage()
	{
		LoadLanguage(Application.systemLanguage);
	}


	public void LoadLanguage(SystemLanguage language)
	{
		LocalizationSettings settings = LocalizationSettings.GetSettings();
		settings.LoadFromFile();
		_languageDictionary = settings.GetLanguageDictionary(language);
	}


	public string GetLocalizedString(string str)
	{
		if(!_languageDictionary.ContainsKey(str)) {
			Debug.LogWarning("Given String has no predefined key.");
			return str;
		}
		return _languageDictionary[str];
	}

}

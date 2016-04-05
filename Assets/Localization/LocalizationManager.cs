/* 		
		Author : Farhan
		Skype : farhan.blu
		Email : farhan.blu@gmail.com
*/

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


	#region PUBLIC API - The only methods you will need to call in your game. Everything happens behind the scenes.

	/// <summary>
	/// Loads the system language in memory.
	/// </summary>
	public void LoadLanguage()
	{
		LoadLanguage(Application.systemLanguage);
	}


	/// <summary>
	/// Loads the language specified in parameters.
	/// </summary>
	public void LoadLanguage(SystemLanguage language)
	{
		_settings.LoadFromFile();
		_languageDictionary = _settings.GetLanguageDictionary(language);
	}


	/// <summary>
	/// Gets the localized string for the string/key passed in as parameter.
	/// </summary>
	public string GetLocalizedString(string str)
	{
		if(!_languageDictionary.ContainsKey(str)) {
			Debug.LogWarning("Given string '" + str + "' has no predefined key.");
			return str;
		}
		return _languageDictionary[str];
	}

	#endregion
}

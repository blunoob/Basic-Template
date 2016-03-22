using UnityEngine;
using System.Collections;
using UnityEditor;

public class LanguageEditor : EditorWindow 
{
	public void LoadWithSettings(SystemLanguage lang, LocalizationSettings localeSettings)
	{
		this.titleContent = new GUIContent(lang.ToString());
	}

	protected void OnGUI()
	{
		
	}
}

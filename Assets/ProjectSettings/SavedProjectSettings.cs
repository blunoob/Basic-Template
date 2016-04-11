/* 		
		Author : Farhan
		Skype : farhan.blu
		Email : farhan.blu@gmail.com
*/

using UnityEngine;
using UnityEditor;
using System.Collections;

[System.Serializable]
public class SavedProjectSettings : ScriptableObject 
{
	//Define all the settings you want to save here.
	public RenderingPath _renderingPath;
	public ColorSpace _colorSpace;

	//Any setting you add above, make sure to write code for its application.
	public void Apply()
	{
		PlayerSettings.renderingPath = _renderingPath;
		PlayerSettings.colorSpace = _colorSpace;
	}
}

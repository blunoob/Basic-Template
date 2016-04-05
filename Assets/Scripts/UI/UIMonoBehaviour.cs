/* 		
		Author : Farhan
		Skype : farhan.blu
		Email : farhan.blu@gmail.com
*/

using System;
using UnityEngine;
using System.Collections;

// This class is not really a neccessity. It just lets you quickly see that a script (that inherits from this class) is put on 
// a gameobject that has a UI-related functionality. It doesn't do anything fancy!
public class UIMonoBehaviour : MonoBehaviour 
{
	protected void ExecuteAction(Action action)
	{
		if(action != null)
			action();
	}

}

/* 		
		Author : Farhan
		Skype : farhan.blu
		Email : farhan.blu@gmail.com
*/

using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Extensions
{
	//**********************************************************************************************//

	public static void PerformActionWithDelay (this MonoBehaviour mono, float delay, Action action)
	{
		mono.StartCoroutine (mono.ExecuteDelayedAction (delay, action));
	}
	
	private static IEnumerator ExecuteDelayedAction (this MonoBehaviour ienum, float delay, Action action)
	{
		yield return new WaitForSeconds (delay);
		
		action ();
	}

	//**********************************************************************************************//

	public static T GetRandom <T>(this IList<T> list)
	{
		int randomIndex = UnityEngine.Random.Range(0, list.Count);
		return list[randomIndex];
	}

	//**********************************************************************************************//
}

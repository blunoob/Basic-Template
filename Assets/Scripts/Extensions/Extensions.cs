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

	/// <summary>
	/// Calls a method (no parameters) with delay.
	/// </summary>
	public static void PerformActionWithDelay (this MonoBehaviour mono, float delay, Action action)
	{
		mono.StartCoroutine (mono.ExecuteDelayedAction (delay, action));
	}
	
	private static IEnumerator ExecuteDelayedAction (this MonoBehaviour ienum, float delay, Action action)
	{
		yield return new WaitForSeconds (delay);
		
		action ();
	}

	/// <summary>
	/// Calls a method (1 parameter) with delay.
	/// </summary>
	public static void PerformActionWithDelay <T>(this MonoBehaviour mono, float delay, Action<T> action, params object [] parameters)
	{
		mono.StartCoroutine (mono.ExecuteDelayedAction<T> (delay, action, parameters));
	}

	private static IEnumerator ExecuteDelayedAction <T> (this MonoBehaviour ienum, float delay, Action<T> action, params object [] parameters)
	{
		yield return new WaitForSeconds (delay);
		
		action ((T)parameters[0]);
	}

	/// <summary>
	/// Calls a method (2 parameters) with delay. Avoiding reflection, so order of parameters needs to match.
	/// </summary>
	public static void PerformActionWithDelay <T, U>(this MonoBehaviour mono, float delay, Action<T,U> action, params object [] parameters)
	{
		mono.StartCoroutine (mono.ExecuteDelayedAction <T,U> (delay, action, parameters));
	}

	private static IEnumerator ExecuteDelayedAction <T, U> (this MonoBehaviour ienum, float delay, Action<T,U> action, params object [] parameters)
	{
		yield return new WaitForSeconds (delay);
		
		action ((T)parameters[0], (U)parameters[1]);
	}

	//**********************************************************************************************//

	/// <summary>
	/// Gets a random item from a list.
	/// </summary>
	public static T GetRandom <T>(this IList<T> list)
	{
		int randomIndex = UnityEngine.Random.Range(0, list.Count);
		return list[randomIndex];
	}

	//**********************************************************************************************//

	/// <summary>
	/// Shuffles the element order of the specified list. Taken from Smooth Foundations (has O(n) complexity)
	/// https://www.smooth-games.com/downloads/unity/smooth-foundations/smooth-foundations.unitypackage
	/// </summary>
	public static void Shuffle<T>(this IList<T> ts) {
		var count = ts.Count;
		var last = count - 1;
		for (var i = 0; i < last; ++i) {
			var r = UnityEngine.Random.Range(i, count);
			var tmp = ts[i];
			ts[i] = ts[r];
			ts[r] = tmp;
		}
	}

	//**********************************************************************************************//

	public static Dictionary<T,U> ToDictionary <T,U> (this IDictionary source)
	{
		Dictionary<T,U> genericDictionary = new Dictionary<T, U>();
		IEnumerator iter = source.Keys.GetEnumerator();

		while(iter.MoveNext())
		{ 
			T key = (T) iter.Current;
			U value = (U) (source[iter.Current]);
			genericDictionary.Add(key, value);
		}

		return genericDictionary;
	}

	//**********************************************************************************************//

	public static Dictionary<T,U> DuplicateKeys <T,U>(this Dictionary<T,U> source)
	{
		Dictionary<T,U> copy = new Dictionary<T, U>();

		IEnumerator iter = source.Keys.GetEnumerator();

		while(iter.MoveNext())
		{
			T key = (T) iter.Current;
			copy.Add(key, default(U));
		}

		return copy;
	}

	//**********************************************************************************************//
}

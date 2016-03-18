/* 		
		Author : Farhan
		Skype : farhan.blu
		Email : farhan.blu@gmail.com
*/

/// <summary>
/// Monobehavior with functionality to iterative over arrays/lists etc one by one. You don't really have to
/// subclass it if you don't need this functionality.
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IterativeMono : MonoBehaviour 
{
	protected Dictionary <string, IEnumerator> _collectionDictionary = new Dictionary<string, IEnumerator>();

	public string RegisterEnumerable(IEnumerable enumerable)
	{
		string enumerableKey = Nonce.GetUniqueID();
		_collectionDictionary[enumerableKey] = enumerable.GetEnumerator();
		return enumerableKey;
	}

	public T GetNext <T> (string enumerableKey)
	{
		if(!_collectionDictionary.ContainsKey(enumerableKey))
			return default(T);

		IEnumerator enumerator = _collectionDictionary[enumerableKey];
		if(enumerator.MoveNext())
			return (T) enumerator.Current;
		return default(T);
	}
}

/* 		
		Author : Farhan
		Skype : farhan.blu
		Email : farhan.blu@gmail.com
*/

/// <summary>
/// Provides functionality to LAZY iterative over arrays/lists etc one by one (i.e. execution doesn't happen until next item is requested). 
/// Use it ideally for getting next values from an infinite recursive formula. E.g. Get next number in Fibonacci sequence ...
/// </summary>

using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class LazyIterator
{
	public enum IterativeMode
	{
		Once,
		Loop // IEnumerables are not designed for this purpose really ...
	}

	protected Dictionary <string, EnumerableInfoHolder> _collectionDictionary = new Dictionary<string, EnumerableInfoHolder>();

	public string RegisterEnumerable(IEnumerable enumerable, IterativeMode mode = IterativeMode.Once)
	{
		string enumerableKey = Nonce.GetUniqueID();
		_collectionDictionary[enumerableKey] = new EnumerableInfoHolder {
			_iterMode = mode,
			_enumerable = enumerable,
			_enumerator = enumerable.GetEnumerator()
		};
		return enumerableKey;
	}


	//Returns next item in a sequence.
	public T GetNext <T> (string enumerableKey)
	{
		if(!_collectionDictionary.ContainsKey(enumerableKey))
			return default(T);

		EnumerableInfoHolder info = _collectionDictionary[enumerableKey];

		IEnumerator enumerator = info._enumerator;

		if(enumerator.MoveNext())
			return (T) enumerator.Current;

		if(info._iterMode == IterativeMode.Loop) {
			info._enumerator.Reset();
			return GetNext<T>(enumerableKey);
		}

		return default(T);
	}


	protected class EnumerableInfoHolder
	{
		public IterativeMode _iterMode;
		public IEnumerable _enumerable;
		public IEnumerator _enumerator;
	}

	/*

	// Sample usage : 

		List<string> a = new List<string>();
		a.Add("d");
		a.Add("e");
		a.Add("f");

		LazyIterator i = new LazyIterator();
		string key = i.RegisterEnumerable(a, LazyIterator.IterativeMode.Loop);

		Debug.Log(i.GetNext<string>(key));
      	Debug.Log(i.GetNext<string>(key));
    	Debug.Log(i.GetNext<string>(key));
      	Debug.Log(i.GetNext<string>(key));

	*/
}

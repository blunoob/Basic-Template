/* 		
		Author : Farhan
		Skype : farhan.blu
		Email : farhan.blu@gmail.com
*/

using System;
using System.IO;
using UnityEngine;
using System.Collections;

public static class FileIO
{
	public static void WriteToPersistentStorage(string fileName, string content)
	{
		string filePath = Application.persistentDataPath + "/" + fileName;
		if(File.Exists(filePath))
			File.WriteAllText(filePath, content);
		else {
			StreamWriter sw = File.CreateText (filePath);
			sw.WriteLine (content);
			sw.Close ();
		}
	}


	public static string ReadFromPersistentStorage(string fileName)
	{
		string filePath = Application.persistentDataPath + "/" + fileName;
		return File.ReadAllText(filePath);
	}


	public static bool ExistsInPersistentStorage(string fileName)
	{
		string filePath = Application.persistentDataPath + "/" + fileName;
		return File.Exists(filePath);
	}
}

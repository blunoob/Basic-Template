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

		WriteToPath(filePath, content);
	}


	public static string ReadFromPersistentStorage(string fileName)
	{
		string filePath = Application.persistentDataPath + "/" + fileName;
		return ReadFromPath(filePath);
	}


	public static string ReadTextFromResources(string filePath)
	{
		return Resources.Load<TextAsset>(filePath).text;
	}


	public static bool ExistsInPersistentStorage(string fileName)
	{
		string filePath = Application.persistentDataPath + "/" + fileName;
		return File.Exists(filePath);
	}


	public static void WriteToPath(string fileNameWithPath, string content)
	{
		if(File.Exists(fileNameWithPath))
			File.WriteAllText(fileNameWithPath, content);
		else {
			StreamWriter sw = File.CreateText (fileNameWithPath);
			sw.WriteLine (content);
			sw.Close ();
		}
	}

	public static string ReadFromPath(string fileNameWithPath)
	{
		try
		{
			return File.ReadAllText(fileNameWithPath);
		} catch (Exception e)
		{
			Debug.LogWarning(e);
		}
		return null;
	}

	public static void DeleteFile(string fileWithPath)
	{
		File.Delete(fileWithPath);
	}
}

using UnityEditor;
using UnityEngine;

public class CacheManager : Editor
{
	[MenuItem("Editor Tools/Clean Cache")]
	public static void CleanCache()
	{
#pragma warning disable CS0618 // Type or member is obsolete
		Caching.CleanCache();
		Debug.Log("Cleaned Cache");
#pragma warning restore CS0618 // Type or member is obsolete

		System.IO.DirectoryInfo dataDir = new System.IO.DirectoryInfo(Application.persistentDataPath);
		dataDir.Delete(true);
		Debug.Log("Deleted persistent data path");
	}

	[MenuItem("Editor Tools/Clean PlayerPrefs")]
	public static void CleanPlayerPrefs()
	{
		PlayerPrefs.DeleteAll();
		Debug.Log("Deleted PlayerPrefs");
	}
}

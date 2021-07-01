using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
	public static float TimeSinceLastVisit;

	void Awake()
	{
		string lastDateString = PlayerPrefs.GetString("lastDate", "");
		if (!lastDateString.Equals(""))
		{
			DateTime lastDate = DateTime.Parse(lastDateString);
			DateTime currentDate = DateTime.Now;

			if (currentDate > lastDate)
			{
				TimeSpan timespan = currentDate - lastDate;
				TimeSinceLastVisit = (float)timespan.TotalSeconds;
				Debug.LogFormat("Quit for {0} seconds", timespan.TotalSeconds);
			}

			PlayerPrefs.SetString("lastDate", lastDate.ToString());
		}
	}

	private void OnApplicationQuit()
	{
		DateTime lastDate = DateTime.Now;
		PlayerPrefs.SetString("lastDate", lastDate.ToString());
		Debug.LogFormat("App quit at {0}", lastDate.ToString());
	}
}

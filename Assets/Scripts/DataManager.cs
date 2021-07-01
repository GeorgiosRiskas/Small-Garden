using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1000)]
public class DataManager : MonoBehaviour
{
	private static DataManager Instance;

	[SerializeField] private PlayerData playerData;
	[SerializeField] private SaveData saveData;

	public List<Plot> createdPlotsList;
	public float currentMoney;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else if (Instance != this)
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		EventsManager.OnPlotWasCreatedEvent += EventsManager_OnPlotWasCreatedEvent;
		EventsManager.OnMoneyUpdatedEvent += EventsManager_OnMoneyUpdatedEvent;

		LoadGame();

		if (SceneManager.GetActiveScene().name != "Game")
		{
			SceneManager.LoadScene("Game");
		}
	}

	private void OnApplicationQuit()
	{
		playerData.moneyAmount = currentMoney;

		playerData.plotDataList = new List<PlotSaveData>();
		for (int i = 0; i < createdPlotsList.Count; i++)
		{
			playerData.plotDataList.Add(createdPlotsList[i].CurrentPlotSaveData());
		}

		SaveGame();
	}

	private void EventsManager_OnPlotWasCreatedEvent(Plot plot)
	{
		createdPlotsList.Add(plot);
	}

	private void EventsManager_OnMoneyUpdatedEvent(float amount)
	{
		currentMoney = amount;
	}

	public void SaveGame()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/" + "small-giant-garden.dat");
		saveData = new SaveData();

		saveData.moneyAmount = playerData.moneyAmount;
		saveData.plotDataList = playerData.plotDataList;

		bf.Serialize(file, saveData);

		file.Close();
	}

	public void LoadGame()
	{
		if (File.Exists(Application.persistentDataPath + "/" + "small-giant-garden.dat"))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/" + "small-giant-garden.dat", FileMode.Open);
			SaveData data = (SaveData)bf.Deserialize(file);
			saveData = data;
			file.Close();

			playerData.moneyAmount = saveData.moneyAmount;
			playerData.plotDataList = saveData.plotDataList;

			EventsManager.RaiseGameWasLoadedEvent(playerData);

			Debug.Log("Game was loaded");
		}
		else
		{
			Debug.Log("Nothing to load");
		}
	}
}

[System.Serializable]
public class PlayerData
{
	public float moneyAmount;
	public List<PlotSaveData> plotDataList;

	public PlayerData()
	{
		moneyAmount = 0;
		plotDataList = new List<PlotSaveData>();
	}
}

[System.Serializable]
public class SaveData
{
	public float moneyAmount;
	public List<PlotSaveData> plotDataList;

	public SaveData()
	{
		moneyAmount = 0;
		plotDataList = new List<PlotSaveData>();
	}
}

[System.Serializable]
public class PlotSaveData
{
	public int plotType;
	public int plotUpgradeLevel;
	public int capacityUpgradeLevel;
	public int producedFruitsCount;
	public float elapsedTime;

	public PlotSaveData(int _plotType, int _plotUpgradeLevel, int _capacityUpgradeLevel, int _producedFruitsCount, float _elapsedTime)
	{
		plotType = _plotType;
		plotUpgradeLevel = _plotUpgradeLevel;
		capacityUpgradeLevel = _capacityUpgradeLevel;
		producedFruitsCount = _producedFruitsCount;
		elapsedTime = _elapsedTime;
	}
}
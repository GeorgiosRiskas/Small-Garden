using UnityEngine;

public class PlotsSpawnController : MonoBehaviour
{
	public Transform spawnTransform;
	public GameObject plotPrefab;

	private void Awake()
	{
		EventsManager.OnPlotPurchasedEvent += EventsManager_OnPlotPurchasedEvent;
		EventsManager.OnGameWasLoadedEvent += EventsManager_OnGameWasLoadedEvent;
	}

	private void OnDestroy()
	{
		EventsManager.OnPlotPurchasedEvent -= EventsManager_OnPlotPurchasedEvent;
		EventsManager.OnGameWasLoadedEvent -= EventsManager_OnGameWasLoadedEvent;
	}

	//private void Update()
	//{
	//	Debug.LogFormat("Upgrade value for apples is {0}", InitVariables.PlotByType(0).warehouse.currentCapacityUpgradeLevel);
	//}

	private void EventsManager_OnGameWasLoadedEvent(PlayerData playerData)
	{
		for (int i = 0; i < playerData.plotDataList.Count; i++)
		{
			var go = Instantiate(plotPrefab, spawnTransform);
			var plot = go.GetComponent<Plot>();
			plot.Init(playerData.plotDataList[i]);
			EventsManager.RaisePlotWasCreatedEvent(plot);
		}
	}

	private void EventsManager_OnPlotPurchasedEvent(PurchaseOption purchase)
	{
		var go = Instantiate(plotPrefab, spawnTransform);
		var plot = go.GetComponent<Plot>();
		plot.Init(purchase.type);
		EventsManager.RaisePlotWasCreatedEvent(plot);
	}
}
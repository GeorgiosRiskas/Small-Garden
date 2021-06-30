using UnityEngine;

public class PlotsSpawnController : MonoBehaviour
{
	public Transform spawnTransform;
	public GameObject[] plotsPrefabs;

	private void Awake()
	{
		EventsManager.OnPlotPurchasedEvent += EventsManager_OnPlotPurchasedEvent;
	}

	private void OnDestroy()
	{
		EventsManager.OnPlotPurchasedEvent -= EventsManager_OnPlotPurchasedEvent;
	}

	private void EventsManager_OnPlotPurchasedEvent(PurchaseOption purchase)
	{
		GameObject prefabToSpawn = null;

		foreach (var p in plotsPrefabs)
		{
			if (p.GetComponent<Plot>().plotInfo.type == purchase.type)
			{
				prefabToSpawn = p;
			}
		}

		Instantiate(prefabToSpawn, spawnTransform);
	}
}

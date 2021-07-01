using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
	[SerializeField] private float startingAmount;
	private bool amountChangedOnLoad;

	private static float moneyAmount = 0;
	public static float CurrentMoneyAmount { get { return moneyAmount; } }
	[SerializeField] private TextMeshProUGUI moneyText = null;

	private void Awake()
	{
		EventsManager.OnProductsSoldEvent += EventsManager_OnProductsSoldEvent;
		EventsManager.OnPlotPurchasedEvent += EventsManager_OnPlotPurchasedEvent;
		EventsManager.OnUpgradePurchasedEvent += EventsManager_OnUpgradePurchasedEvent;
		EventsManager.OnGameWasLoadedEvent += EventsManager_OnGameWasLoadedEvent;
	}

	private void OnDestroy()
	{
		EventsManager.OnProductsSoldEvent -= EventsManager_OnProductsSoldEvent;
		EventsManager.OnPlotPurchasedEvent -= EventsManager_OnPlotPurchasedEvent;
		EventsManager.OnUpgradePurchasedEvent -= EventsManager_OnUpgradePurchasedEvent;
		EventsManager.OnGameWasLoadedEvent -= EventsManager_OnGameWasLoadedEvent;
	}

	private void EventsManager_OnGameWasLoadedEvent(PlayerData playerData)
	{
		moneyAmount = playerData.moneyAmount;
		amountChangedOnLoad = true;
	}

	void Start()
	{
		if (!amountChangedOnLoad)
		{
			moneyAmount = startingAmount;
		}
		UpdateMoneyText();
		EventsManager.RaiseMoneyUpdatedEvent(moneyAmount);
	}

	private void EventsManager_OnProductsSoldEvent(float sellingPrice)
	{
		moneyAmount += sellingPrice;
		UpdateMoneyText();
		EventsManager.RaiseMoneyUpdatedEvent(moneyAmount);
	}

	private void EventsManager_OnPlotPurchasedEvent(PurchaseOption purchase)
	{
		moneyAmount -= purchase.purchasePrice;
		UpdateMoneyText();

		EventsManager.RaiseMoneyUpdatedEvent(moneyAmount);
	}

	private void EventsManager_OnUpgradePurchasedEvent(float price)
	{
		moneyAmount -= price;
		UpdateMoneyText();

		EventsManager.RaiseMoneyUpdatedEvent(moneyAmount);
	}


	void UpdateMoneyText()
	{
		moneyText.text = moneyAmount.ToString();
	}
}

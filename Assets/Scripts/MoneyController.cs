using TMPro;
using UnityEngine;

public class MoneyController : MonoBehaviour
{
	public float moneyAmount;
	public TextMeshProUGUI moneyText;

	private void Awake()
	{
		EventsManager.OnProductsSoldEvent += EventsManager_OnProductsSoldEvent;
		EventsManager.OnPlotPurchasedEvent += EventsManager_OnPlotPurchasedEvent;
	}

	private void OnDestroy()
	{
		EventsManager.OnProductsSoldEvent -= EventsManager_OnProductsSoldEvent;
		EventsManager.OnPlotPurchasedEvent -= EventsManager_OnPlotPurchasedEvent;
	}

	void Start()
	{
		moneyAmount = 2000;
		UpdateMoneyText();
		EventsManager.RaiseMoneyUpdatedEvent(moneyAmount);
	}

	private void EventsManager_OnProductsSoldEvent(float sellingPrice)
	{
		moneyAmount += sellingPrice;
		UpdateMoneyText();
		EventsManager.RaiseMoneyUpdatedEvent(moneyAmount);
		//Debug.LogFormat("Product sold for {0}", sellingPrice);
	}

	private void EventsManager_OnPlotPurchasedEvent(PurchaseOption purchase)
	{
		moneyAmount -= purchase.purchasePrice;
		UpdateMoneyText();

		EventsManager.RaiseMoneyUpdatedEvent(moneyAmount);
	}

	void UpdateMoneyText()
	{
		moneyText.text = moneyAmount.ToString();
	}
}

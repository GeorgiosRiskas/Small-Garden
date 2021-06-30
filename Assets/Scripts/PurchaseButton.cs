using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PurchaseButton : MonoBehaviour
{
	public TextMeshProUGUI purchasePriceText;
	private Button button;
	public PurchaseOption purchase;

	private void Awake()
	{
		EventsManager.OnMoneyUpdatedEvent += EventsManager_OnMoneyUpdatedEvent;
	}

	private void OnDestroy()
	{
		EventsManager.OnMoneyUpdatedEvent -= EventsManager_OnMoneyUpdatedEvent;
	}

	void Start()
	{
		button = GetComponent<Button>();
		UpdatePriceText();
	}

	private void EventsManager_OnMoneyUpdatedEvent(float amount)
	{
		button.interactable = purchase.purchasePrice <= amount;
	}

	private void UpdatePriceText()
	{
		purchasePriceText.text = string.Format("{0}$", purchase.purchasePrice);
	}

	public void UI_PurchasePlot()
	{
		EventsManager.RaisePlotPurchasedEvent(purchase);
	}
}

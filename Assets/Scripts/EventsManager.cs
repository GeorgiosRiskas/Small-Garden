public class EventsManager
{
	public delegate void OnProductsSold(float sellingPrice);
	public static event OnProductsSold OnProductsSoldEvent;
	public static void RaiseProductsSoldEvent(float sellingPrice)
	{
		OnProductsSoldEvent?.Invoke(sellingPrice);
	}

	public delegate void OnUpgradePurchased(float price);
	public static event OnUpgradePurchased OnUpgradePurchasedEvent;
	public static void RaiseUpgradePurchasedEvent(float price)
	{
		OnUpgradePurchasedEvent?.Invoke(price);
	}

	public delegate void OnPlotPurchased(PurchaseOption purchase);
	public static event OnPlotPurchased OnPlotPurchasedEvent;
	public static void RaisePlotPurchasedEvent(PurchaseOption purchase)
	{
		OnPlotPurchasedEvent?.Invoke(purchase);
	}

	public delegate void OnMoneyUpdated(float amount);
	public static event OnMoneyUpdated OnMoneyUpdatedEvent;
	public static void RaiseMoneyUpdatedEvent(float amount)
	{
		OnMoneyUpdatedEvent?.Invoke(amount);
	}
}

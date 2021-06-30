using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Plot : MonoBehaviour
{
	public PlotInfo plotInfo;

	private int producedFruitsCount;
	private float elapsedTime;

	public Slider progressSlider;
	public TextMeshProUGUI productsCountText;
	public TextMeshProUGUI sellingPriceText;

	public Button capacityUpgradeButton;
	public TextMeshProUGUI capacityUpgradeText;

	public Button plotSizeUpgradeButton;
	public TextMeshProUGUI plotSizeUpgradeText;

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
		UpdateProgressSlider();
		UpdateInfoTexts();
		UpdateUpgradeText();
	}

	void Update()
	{
		if (producedFruitsCount == plotInfo.warehouse.currentCapacity)
			return;

		elapsedTime += Time.deltaTime;
		if (elapsedTime >= plotInfo.productionTime)
		{
			producedFruitsCount++;
			elapsedTime = 0;
		}

		if (producedFruitsCount < plotInfo.warehouse.currentCapacity)
		{
			progressSlider.value = elapsedTime;
		}

		UpdateInfoTexts();
	}

	void UpdateProgressSlider()
	{
		progressSlider.minValue = 0;
		progressSlider.maxValue = plotInfo.productionTime;
	}

	void UpdateInfoTexts()
	{
		productsCountText.text = string.Format("{0}/{1}", producedFruitsCount, plotInfo.warehouse.currentCapacity);
		sellingPriceText.text = string.Format("Sell \n {0}$", SellingPrice());
	}

	void UpdateUpgradeText()
	{
		if ((plotInfo.warehouse.currentCapacityUpgradeLevel < plotInfo.warehouse.capacityUpgradeLevelMax))
		{
			capacityUpgradeText.text = string.Format("Capacity upgrade\nLevel {0}\n${1}", NextCapacityUpgradeLevel(), plotInfo.warehouse.capacityUpgradeCost);
		}
		else
		{
			capacityUpgradeText.text = "The capacity has maxed out";
		}

		if ((plotInfo.currentPlotSizeUpgradeLevel < plotInfo.plotSizeUpgradeLevelMax))
		{
			plotSizeUpgradeText.text = string.Format("Speed upgrade\nLevel {0}\n${1}", NextPlotSizeUpgradeLevel(), plotInfo.plotSizeUpgradeCost);
		}
		else
		{
			plotSizeUpgradeText.text = "The speed has maxed out";
		}
	}

	private void EventsManager_OnMoneyUpdatedEvent(float amount)
	{
		capacityUpgradeButton.interactable = (plotInfo.warehouse.capacityUpgradeCost <= amount) && (plotInfo.warehouse.currentCapacityUpgradeLevel < plotInfo.warehouse.capacityUpgradeLevelMax);
		plotSizeUpgradeButton.interactable = (plotInfo.plotSizeUpgradeCost <= amount) && (plotInfo.currentPlotSizeUpgradeLevel < plotInfo.plotSizeUpgradeLevelMax);
	}

	public void UI_SellProducts()
	{
		EventsManager.RaiseProductsSoldEvent(SellingPrice());
		producedFruitsCount = 0;
		UpdateInfoTexts();
	}

	public void UI_UpgradeCapacity()
	{
		// Getting a reference of the price before the currentCapacityUpgradeLevel is incremented
		var price = plotInfo.warehouse.capacityUpgradeCost;
		plotInfo.warehouse.currentCapacityUpgradeLevel++;
		UpdateUpgradeText();
		EventsManager.RaiseUpgradePurchasedEvent(price);
	}

	public void UI_UpgradePlotSize()
	{
		var price = plotInfo.plotSizeUpgradeCost;
		plotInfo.currentPlotSizeUpgradeLevel++;
		UpdateProgressSlider();
		UpdateUpgradeText();
		EventsManager.RaiseUpgradePurchasedEvent(price);
	}

	private float SellingPrice()
	{
		return producedFruitsCount * plotInfo.warehouse.fixedUnitPrice;
	}

	private float NextCapacityUpgradeLevel()
	{
		return plotInfo.warehouse.currentCapacityUpgradeLevel + 1;
	}

	private float NextPlotSizeUpgradeLevel()
	{
		return plotInfo.currentPlotSizeUpgradeLevel + 1;
	}
}

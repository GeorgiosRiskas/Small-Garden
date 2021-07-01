using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Plot : MonoBehaviour
{
	public PlotInfo plotInfo;

	private int producedFruitsCount;
	private float elapsedTime;

	[Header("Icons are assigned in the same order as the types")]
	public Sprite[] icons;
	public Image mainIcon;
	public Slider progressSlider;
	public TextMeshProUGUI productsCountText;
	public TextMeshProUGUI sellingPriceText;

	public Button capacityUpgradeButton;
	public TextMeshProUGUI capacityUpgradeText;

	public Button plotSizeUpgradeButton;
	public TextMeshProUGUI plotSizeUpgradeText;

	private float currentMoneyAmount;

	private void Awake()
	{
		EventsManager.OnMoneyUpdatedEvent += EventsManager_OnMoneyUpdatedEvent;
	}

	private void OnDestroy()
	{
		EventsManager.OnMoneyUpdatedEvent -= EventsManager_OnMoneyUpdatedEvent;
	}

	public void Init(ProductType type)
	{
		Debug.Log("Init by spawn");
		plotInfo = JsonLoader.Instance.GetPlotInfoByType(type); //new PlotInfo(info.type, info.unitProductionRate, info.plotSizeFixedUpgradeAmount, info.plotSizeUpgradeLevelMax, info.warehouse);
		mainIcon.sprite = icons[(int)type];

		currentMoneyAmount = MoneyController.CurrentMoneyAmount;
	}

	public void Init(PlotSaveData loadData)
	{
		Debug.Log("Init by load");
		//var info = InitVariables.PlotByType((ProductType)loadData.plotType);
		plotInfo = JsonLoader.Instance.GetPlotInfoByType((ProductType)loadData.plotType); //new PlotInfo(info.type, info.unitProductionRate, info.plotSizeFixedUpgradeAmount, info.plotSizeUpgradeLevelMax, info.warehouse);
		mainIcon.sprite = icons[loadData.plotType];

		// Values that change when the game saves
		plotInfo.currentPlotSizeUpgradeLevel = loadData.plotUpgradeLevel;
		plotInfo.warehouse.currentCapacityUpgradeLevel = loadData.capacityUpgradeLevel;
		producedFruitsCount = loadData.producedFruitsCount;
		elapsedTime = loadData.elapsedTime;

		currentMoneyAmount = MoneyController.CurrentMoneyAmount;
	}

	void Start()
	{
		SetButtonsInteractable();
		UpdateProgressSliderDefaultValues();
		UpdateInfoTexts();
		UpdateUpgradeText();
	}

	void Update()
	{
		if (producedFruitsCount == plotInfo.warehouse.currentCapacity)
		{
			progressSlider.value = progressSlider.maxValue;
			return;
		}


		elapsedTime += Time.deltaTime;
		progressSlider.value = elapsedTime;
		if (elapsedTime >= plotInfo.productionTime)
		{
			producedFruitsCount++;
			elapsedTime = 0;
		}

		UpdateInfoTexts();
		UpdateUpgradeText();
	}

	void UpdateProgressSliderDefaultValues()
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
		currentMoneyAmount = amount;
		SetButtonsInteractable();
	}

	void SetButtonsInteractable()
	{
		capacityUpgradeButton.interactable = (plotInfo.warehouse.capacityUpgradeCost <= currentMoneyAmount) && (plotInfo.warehouse.currentCapacityUpgradeLevel < plotInfo.warehouse.capacityUpgradeLevelMax);
		plotSizeUpgradeButton.interactable = (plotInfo.plotSizeUpgradeCost <= currentMoneyAmount) && (plotInfo.currentPlotSizeUpgradeLevel < plotInfo.plotSizeUpgradeLevelMax);
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
		UpdateProgressSliderDefaultValues();
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

	public PlotSaveData CurrentPlotSaveData()
	{
		return new PlotSaveData((int)plotInfo.type, plotInfo.currentPlotSizeUpgradeLevel, plotInfo.warehouse.currentCapacityUpgradeLevel, producedFruitsCount, elapsedTime);
	}
}
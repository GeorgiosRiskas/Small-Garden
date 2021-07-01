using UnityEngine;

public enum ProductType { Apple, Bananas, Cherry, Grapes };

[System.Serializable]
public class PlotInfo
{
	public ProductType type;
	public float unitProductionRate;
	public float plotSizeFixedUpgradeAmount;
	public int plotSizeUpgradeLevelMax;

	public float plotSizeBase;
	public int currentPlotSizeUpgradeLevel;

	public float plotSize { get { return plotSizeBase + (plotSizeFixedUpgradeAmount * currentPlotSizeUpgradeLevel); } }
	public float plotSizeUpgradeCost { get { return Mathf.Pow(currentPlotSizeUpgradeLevel + 1, 2) * plotSize * 100; } }
	public float productionTime { get { return unitProductionRate / plotSize; } }

	public Warehouse warehouse;

	public PlotInfo(ProductType _type, float _unitProductionRate, float _plotSizeFixedUpgradeAmount, int _plotSizeUpgradeLevelMax, Warehouse _warehouse)
	{
		type = _type;
		unitProductionRate = _unitProductionRate;
		plotSizeFixedUpgradeAmount = _plotSizeFixedUpgradeAmount;
		plotSizeUpgradeLevelMax = _plotSizeUpgradeLevelMax;
		warehouse = _warehouse;

		plotSizeBase = 1;
		//currentPlotSizeUpgradeLevel = _currentPlotSizeUpgradeLevel;
	}

	public PlotInfo()
	{

	}
}

[System.Serializable]
public class Warehouse
{
	public int capacityBase;
	public int capacityFixedUpgradeAmount;
	public int capacityUpgradeLevelMax;
	public float fixedUnitPrice;

	public int currentCapacityUpgradeLevel;

	public int currentCapacity { get { return capacityBase + (capacityFixedUpgradeAmount * currentCapacityUpgradeLevel); } }
	public float capacityUpgradeCost { get { return Mathf.Pow(currentCapacityUpgradeLevel + 1, 2) * currentCapacity * 100; } }

	public Warehouse(int _capacityBase, int _capacityFixedUpgradeAmount, int _capacityUpgradeLevelMax, float _fixedUnitPrice)
	{
		capacityBase = _capacityBase;
		capacityFixedUpgradeAmount = _capacityFixedUpgradeAmount;
		capacityUpgradeLevelMax = _capacityUpgradeLevelMax;
		fixedUnitPrice = _fixedUnitPrice;

		//currentCapacityUpgradeLevel = _currentCapacityUpgradeLevel;
	}
}


[System.Serializable]
public class PurchaseOption
{
	public ProductType type;
	public float purchasePrice;
}
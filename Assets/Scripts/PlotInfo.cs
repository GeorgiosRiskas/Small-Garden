using UnityEngine;

public enum ProductType { Apple, Cherry, Grapes, Bananas };

[System.Serializable]
public class PlotInfo
{
	public ProductType type;
	public float unitProductionRate;

	public float plotSizeBase;
	public int currentPlotSizeUpgradeLevel;
	public int plotSizeUpgradeLevelMax;
	public float plotSize { get { return plotSizeBase + (plotSizeFixedUpgradeAmount * currentPlotSizeUpgradeLevel); } }
	public float plotSizeFixedUpgradeAmount;

	public float plotSizeUpgradeCost { get { return Mathf.Pow(currentPlotSizeUpgradeLevel + 1, 2) * plotSize * 100; } }

	public float productionTime { get { return unitProductionRate / plotSize; } }
	public Warehouse warehouse;
}

[System.Serializable]
public class Warehouse
{
	public int capacityBase;
	public int currentCapacity { get { return capacityBase + (capacityFixedUpgradeAmount * currentCapacityUpgradeLevel); } }
	public int currentCapacityUpgradeLevel;
	public int capacityUpgradeLevelMax;
	public int capacityFixedUpgradeAmount;
	public float capacityUpgradeCost { get { return Mathf.Pow(currentCapacityUpgradeLevel + 1, 2) * currentCapacity * 100; } }

	public float fixedUnitPrice;
}


[System.Serializable]
public class PurchaseOption
{
	public ProductType type;
	public float purchasePrice;
}
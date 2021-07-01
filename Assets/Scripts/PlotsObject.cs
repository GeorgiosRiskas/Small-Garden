using System.Collections.Generic;

[System.Serializable]
public class PlotsObjectRoot
{
	public List<PlotsObject> plots;
}

[System.Serializable]
public class PlotsObject
{
	public int type;
	public float unitProductionRate;
	public float plotSizeFixedUpgradeAmount;
	public int plotSizeUpgradeLevelMax;
	public WarehouseObject warehouse;
}

[System.Serializable]
public class WarehouseObject
{
	public int capacityBase;
	public int capacityFixedUpgradeAmount;
	public int capacityUpgradeLevelMax;
	public float fixedUnitPrice;
}

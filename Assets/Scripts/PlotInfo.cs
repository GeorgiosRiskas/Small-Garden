public enum ProductType { Apple, Cherry, Grapes, Bananas };

[System.Serializable]
public class PlotInfo
{
	public ProductType type;
	public float unitProductionRate;
	public float plotSize;
	public float productionTime { get { return unitProductionRate / plotSize; } }
	public Warehouse warehouse;
}

[System.Serializable]
public class Warehouse
{
	public int capacity;
	public float fixedUnitPrice;
}

[System.Serializable]
public class PurchaseOption
{
	public ProductType type;
	public float purchasePrice;
}
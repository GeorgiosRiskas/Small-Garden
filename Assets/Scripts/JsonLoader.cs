using System.Collections.Generic;
using UnityEngine;

public class JsonLoader : MonoBehaviour
{
	public static JsonLoader Instance;
	public PlotsObjectRoot plotsObjectRoot;
	public List<PlotsObject> plotObjectsList;


	void Awake()
	{
		Instance = this;

		var plotsJson = Resources.Load<TextAsset>("plots");
		plotsObjectRoot = JsonUtility.FromJson<PlotsObjectRoot>("{\"plots\":" + plotsJson.text + "}");
	}

	public PlotInfo GetPlotInfoByType(ProductType type)
	{
		PlotInfo info = new PlotInfo();
		foreach (var plotDefault in plotsObjectRoot.plots)
		{
			if (plotDefault.type == (int)type)
			{
				info = new PlotInfo(type, plotDefault.unitProductionRate, plotDefault.plotSizeFixedUpgradeAmount, plotDefault.plotSizeUpgradeLevelMax,
					new Warehouse(plotDefault.warehouse.capacityBase, plotDefault.warehouse.capacityFixedUpgradeAmount, plotDefault.warehouse.capacityUpgradeLevelMax, plotDefault.warehouse.fixedUnitPrice));
			}
		}

		return info;
	}


}

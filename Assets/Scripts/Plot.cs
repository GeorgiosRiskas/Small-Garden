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
	public TextMeshProUGUI sellingPricetext;


	void Start()
	{
		//Debug.LogFormat("Production time is: {0}", plotInfo.productionTime);

		progressSlider.minValue = 0;
		progressSlider.maxValue = plotInfo.productionTime;

		UpdateInfoTexts();
	}

	void Update()
	{

		if (producedFruitsCount == plotInfo.warehouse.capacity)
			return;

		elapsedTime += Time.deltaTime;
		if (elapsedTime >= plotInfo.productionTime)
		{
			producedFruitsCount++;
			elapsedTime = 0;
			UpdateInfoTexts();
		}

		if (producedFruitsCount < plotInfo.warehouse.capacity)
		{
			progressSlider.value = elapsedTime;
		}
	}

	void UpdateInfoTexts()
	{
		productsCountText.text = string.Format("{0}/{1}", producedFruitsCount, plotInfo.warehouse.capacity);
		sellingPricetext.text = string.Format("Sell \n {0}$", SellingPrice());
	}

	public void UI_SellProducts()
	{
		EventsManager.RaiseProductsSoldEvent(SellingPrice());
		producedFruitsCount = 0;
		UpdateInfoTexts();
	}

	private float SellingPrice()
	{
		return producedFruitsCount * plotInfo.warehouse.fixedUnitPrice;
	}
}

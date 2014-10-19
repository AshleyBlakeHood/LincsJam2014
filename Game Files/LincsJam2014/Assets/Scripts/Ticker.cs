using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ticker : MonoBehaviour
{
	Image tickerImage;

	// Use this for initialization
	void Start () {
		tickerImage = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate()
	{
		//rectTransform.sizeDelta = new Vector2( yourWidth, yourHeight);
		tickerImage.rectTransform.sizeDelta = new Vector2(tickerImage.rectTransform.rect.width - Time.deltaTime * 50, tickerImage.rectTransform.rect.height);
		//timeLimitImage.rectTransform.rect.width = timeLimitImage.rectTransform.rect.width + Time.deltaTime * 5;
	}
}

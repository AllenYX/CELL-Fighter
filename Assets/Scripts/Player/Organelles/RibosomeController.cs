using UnityEngine;
using System.Collections;

public class RibosomeController : OrganelleController {

	private float proteinSynthesisTime = 20f;
	
	public GUIText ribosomeAmountText;
	private float ribosomeAmountFontSize = 16f;

	public Sprite ribosomes_1;
	public Sprite ribosomes_2;
	public Sprite ribosomes_3;
	public Sprite ribosomes_4;
	public Sprite ribosomes_5;
	public Sprite ribosomes_6;
	public Sprite ribosomes_7;
	public Sprite ribosomes_8;
	public Sprite ribosomes_9;
	
	private SpriteRenderer ribosomesRenderer;

	public void Start () {
		ReferenceObjects ();
		ResizeText ();
		ribosomeAmountText.fontSize = Mathf.FloorToInt (ribosomeAmountFontSize * resizeRatio);
		
		StartCoroutine ("ProteinSynthesis");
	}
	
	private void ReferenceObjects () {
		ReferenceCellController ();
		
		ribosomesRenderer = gameObject.GetComponent<SpriteRenderer>();
	}

	private void Update () {
		if (organelleText.enabled) {
			ribosomeAmountText.enabled = true;

			ribosomeAmountText.text = cellController.playerCell.Ribosomes + 
								"/" + cellController.playerCell.RibosomesLimit;
		} else 
			ribosomeAmountText.enabled = false;


		int ribosomes = cellController.playerCell.Ribosomes;
		
		if (ribosomes == 0)
			ribosomesRenderer.sprite = null;
		else if (ribosomes == 1)
			ribosomesRenderer.sprite = ribosomes_1;
		else if (ribosomes == 2)
			ribosomesRenderer.sprite = ribosomes_2;
		else if (ribosomes == 3)
			ribosomesRenderer.sprite = ribosomes_3;
		else if (ribosomes == 4)
			ribosomesRenderer.sprite = ribosomes_4;
		else if (ribosomes == 5)
			ribosomesRenderer.sprite = ribosomes_5;
		else if (ribosomes == 6)
			ribosomesRenderer.sprite = ribosomes_6;
		else if (ribosomes == 7)
			ribosomesRenderer.sprite = ribosomes_7;
		else if (ribosomes == 8)
			ribosomesRenderer.sprite = ribosomes_8;
		else if (ribosomes == 9)
			ribosomesRenderer.sprite = ribosomes_9;
	}

	IEnumerator ProteinSynthesis () {
		while (true) {
			yield return new WaitForSeconds(proteinSynthesisTime);

			cellController.CreateProtein ();
		}
	}
}
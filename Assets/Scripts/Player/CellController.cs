using UnityEngine;
using System.Collections;

public class CellController : MonoBehaviour {

	public Cell playerCell = new Cell ("Plant"); // Creates a new "character" for the player

	private GameObject gameControllerObject;
	private GameController gameController;
	// playerObject is passed in through Unity editor, as using FindWithTag("Player") causes a bug
	// where an invisible "Player" clone is created and referenced to instead of the player
	public GameObject playerObject;
	private PlayerController playerController;
	
	public GUIText atpText;
	public GUIText waterVacuoleText; // GUI text displaying resource values
	public GUIText glucoseText;
	public GUIText proteinText;
	private float atpTextSize = 28f;
	private float resourceTextSize = 18f;
	private float aspectWidth = 1366f;
	private float aspectHeight = 768f;

	private float atpConsumeTime = 3f; // Normal consumption of ATP (for life)
	private int atpConsumeRate = 1;
	private float atpDecayTime = 7.3f; // ATP "tax" to promote resource management
	private float atpDecayRate = 0.05f; 
	private int atpHealthDecay = 2; // HP loss when atp = 0

	private int waterHealthDamage = 1; // HP loss when water is collected with a full vacuole
	private int healingAmount = 5;
	private int healingProteinCost = 3; // Golgi apparatus healing cost
	private float shieldRegenerationCurrent = 0f; 
	private float shieldRegenerationEnd = 8f;

	private float addChloroplastWaterPercent = 6f; //Divides by a value instead of percent
	private int createGlucoseRate = 1; // Amount of a resource that gets created
	private int createGlucoseWaterCost = 6; // Cost to produce that resource
	private int createAtpRate = 30;
	private int createAtpGlucoseCost = 1;
	private int createRibosomeProteinCost = 4;
	private int createProteinRate = 1;
	private int createProteinAtpCost = 4;
	
	private float shieldedTime = 0.6f; // Shield hit frames
	private bool shielded = false;

	private float invincibilityTime = 1f; // Invincibility frames after player is hit
	private bool invincible = false;

	private void Start () {
		ReferenceObjects ();
		SetPlayerVariables ();
		ResizeText ();
	}

	private void Update () {
		UpdateText ();
	}

	// References other gameobjects for code interaction
	private void ReferenceObjects () {
		gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
			gameController = gameControllerObject.GetComponent <GameController>();
		if (gameController == null)
			Debug.Log ("Cannot find 'GameController' script");

		if (playerObject != null)
			playerController = playerObject.GetComponent <PlayerController>();	
		if (playerController == null)
			Debug.Log ("Cannot find 'PlayerController' script");
	}

	public void SetPlayerVariables () {
		if (gameController.Level == (int) Scenes.PlantTutorial1) {
			StartCoroutine ("RegenerateShield");
			playerCell.VacuoleWaterLimit = 10;
			playerCell.VacuoleWater = 0;
		} else if (gameController.Level == (int) Scenes.PlantTutorial2) {
			StartCoroutine ("ConsumeAtp");
			StartCoroutine ("RegenerateShield");
			playerCell.Atp = 100;
		} 

		if (gameController.Level == (int)Scenes.Level0) {
			StartCoroutines ();
		}
	}

	private void ResizeText () {
		aspectWidth = GameController.aspectWidth;
		aspectHeight = GameController.aspectHeight;
		
		float widthRatio = Screen.width / aspectWidth;
		float heightRatio = Screen.height / aspectHeight;
		float resizeRatio = Mathf.Min (widthRatio, heightRatio);

		atpText.fontSize = Mathf.FloorToInt (atpTextSize * resizeRatio);
		waterVacuoleText.fontSize = Mathf.FloorToInt (resourceTextSize * resizeRatio);
		glucoseText.fontSize = Mathf.FloorToInt (resourceTextSize * resizeRatio);
		proteinText.fontSize = Mathf.FloorToInt (resourceTextSize * resizeRatio);
	}
	
	private void UpdateText () {
		if (playerCell.Atp <= 0)
			atpText.color = Color.red;
		else 
			atpText.color = Color.white;
		atpText.text = "" + playerCell.Atp;

		waterVacuoleText.text = playerCell.VacuoleWater + "/" + playerCell.VacuoleWaterLimit;
		glucoseText.text = "" + playerCell.Glucose;
		proteinText.text = "" + playerCell.Protein;
	}

	private void StartCoroutines () {
		StartCoroutine ("ConsumeAtp");
		StartCoroutine ("DecayAtp");
		StartCoroutine ("RegenerateShield");
	}

	// Constant ATP consumption during game to represent atp used in living
	public IEnumerator ConsumeAtp () {
		while (true) {
			yield return new WaitForSeconds(atpConsumeTime);

			if (playerCell.Atp > 0) {
				playerCell.Atp -= atpConsumeRate;
				if (playerCell.Atp < 0)
					playerCell.Atp = 0;
			} else {
				DamageMembrane (atpHealthDecay, false);
			}
		}
	}

	// Decay of ATP, like a tax, to represent conservation
	private IEnumerator DecayAtp () {
		while (true) {
			yield return new WaitForSeconds(atpDecayTime);

			playerCell.Atp -= (int) Mathf.Floor (playerCell.Atp * atpDecayRate);
		}
	}	

	// Cell wall shields regenerate slowly depending on amount of water in vacuole
	private IEnumerator RegenerateShield () {
		while (true) {
			if (playerCell.WallShieldCurrent < playerCell.WallShieldTotal) {
				yield return new WaitForSeconds(1f);
				if (shieldRegenerationCurrent < shieldRegenerationEnd)
					shieldRegenerationCurrent += (float) playerCell.VacuoleWater /
						playerCell.VacuoleWaterLimit;
				else {
					playerCell.WallShieldCurrent += 1; 
					shieldRegenerationCurrent = 0f;
				}
			} else {
				yield return new WaitForSeconds(2f);
			}
		}
	}

	private IEnumerator ShieldOnHit () {
		shielded = true;
		playerController.ToggleShielded ();

		yield return new WaitForSeconds(shieldedTime);

		shielded = false;
		playerController.ToggleShielded ();

		yield return 0;
	}

	// Makes player invincible to enemies for a period of time
	private IEnumerator InvincibilityOnHit () {
		invincible = true;
		playerController.ToggleInvincibility ();
		playerController.Knockback = true;

		yield return new WaitForSeconds(invincibilityTime);

		invincible = false;
		playerController.ToggleInvincibility ();

		yield return 0;
	}

	// Handles damaging health
	public void DamageMembrane (int damage, bool activeDamage) {
		// Non-active damage causes direct damage to health
		if (!activeDamage) {
			if (playerCell.MembraneHealthCurrent > damage) {
				playerCell.MembraneHealthCurrent -= damage;
			} else {
				playerCell.MembraneHealthCurrent = 0;
				gameController.GameOver = true;
				playerController.Dead = true;
				print ("You have died");
			}
		} else if (!invincible && !shielded) { // Invincibility after hit stops constant hits
			// Shield stops one instance of damage
			if (playerCell.WallShieldCurrent > 0) {
				playerCell.WallShieldCurrent -= 1;
				StartCoroutine ("ShieldOnHit");
				return;
			}
			StartCoroutine ("InvincibilityOnHit");
			
			if (playerCell.MembraneHealthCurrent > damage) {
				playerCell.MembraneHealthCurrent -= damage;
			} else {
				playerCell.MembraneHealthCurrent = 0;
				gameController.GameOver = true;
				playerController.Dead = true;
			}
		}
	}

	// Handles healing health
	public void HealMembrane () {
		if (playerCell.MembraneHealthCurrent == playerCell.MembraneHealthTotal) {
			print ("Health already full");
			return;
		} else if (playerCell.Protein >= healingProteinCost) {
			playerCell.MembraneHealthCurrent += healingAmount;
			playerCell.Protein -= healingProteinCost;

			if (playerCell.MembraneHealthCurrent > playerCell.MembraneHealthTotal)
				playerCell.MembraneHealthCurrent = playerCell.MembraneHealthTotal;
		}

	}

	public void AddVacuoleWater (int waterValue) {
		if (playerCell.VacuoleWater + waterValue <= playerCell.VacuoleWaterLimit)
			playerCell.VacuoleWater += waterValue;
		else {
			playerCell.VacuoleWater = playerCell.VacuoleWaterLimit;
			print ("Vacuoles are bursting");
			DamageMembrane (waterHealthDamage, false);
		}
	}

	public void AddChloroplastWater () {
		// Amount of water draggable from vacuole to chloroplast is a set percentage
		int waterValue = Mathf.FloorToInt (playerCell.VacuoleWaterLimit / addChloroplastWaterPercent);
		int chloroplastSpaceRemained = playerCell.ChloroplastWaterLimit - playerCell.ChloroplastWater;

		if (playerCell.VacuoleWater < waterValue)
			waterValue = playerCell.VacuoleWater;

		if (waterValue > chloroplastSpaceRemained)
			waterValue = chloroplastSpaceRemained;

		playerCell.ChloroplastWater += waterValue;
		playerCell.VacuoleWater -= waterValue;
	}

	public void CreateGlucose () {
		if (playerCell.ChloroplastWater >= createGlucoseWaterCost) {
			playerCell.Glucose += createGlucoseRate;
			playerCell.ChloroplastWater -= createGlucoseWaterCost;
		} else {
			print ("Not enough water in chloroplast for glucose synthesis");
		}
	}

	public void CreateAtp () {
		playerCell.Atp += createAtpRate;
		playerCell.Glucose -= createAtpGlucoseCost;
	}

	public void CreateRibosome () {
		if (playerCell.Ribosomes == playerCell.RibosomesLimit) {
			print ("Ribosome limit reached");
			return;
		}

		if (playerCell.Protein >= createRibosomeProteinCost) {
			playerCell.Protein -= createRibosomeProteinCost;
			playerCell.Ribosomes += 1;
		}
	}

	public void CreateProtein () {
		// Every ribosomes produces protein at the same time and cost
		for (int i = 0; i < playerCell.Ribosomes; i++) {
			if (playerCell.Atp >= createProteinAtpCost) {
				playerCell.Protein += createProteinRate;
				playerCell.Atp -= createProteinAtpCost;
			} else {
				i = playerCell.Ribosomes;
				print ("Not enough ATP for protein synthesis");
			}
		}
	}

	public int CreateGlucoseWaterCost {
		get { return createGlucoseWaterCost;}
	}

	public int CreateRibosomeProteinCost {
		get { return createRibosomeProteinCost;}
	}
}

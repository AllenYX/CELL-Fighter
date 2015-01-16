using UnityEngine;
using System.Collections;

public class EndoplasmicSmoothController : OrganelleController {

	private bool hasProtein;

	private void Start () {
		ReferenceCellController ();
		ResizeText ();
	}
}

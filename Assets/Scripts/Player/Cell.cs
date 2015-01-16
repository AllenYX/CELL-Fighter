using UnityEngine;
using System.Collections;

public class Cell {

	private string cellType; // Distinguishes between plant/animal cell
	private int membraneHealthCurrent; // Cell membrane = health
	private int membraneHealthTotal;
	private int wallShieldCurrent; // Cell wall = shield, only plants have
	private int wallShieldTotal;
	private int wallShieldLimit;
	private int vacuoleWater; // Water for plants, food for animals
	private int vacuoleWaterLimit;
	private int chloroplastWater; // Chloroplasts also hold water
	private int chloroplastWaterLimit;
	private int food;
	private int foodLimit;
	private int glucose; // Glucose + light = ATP
	private int atp; // ATP = currency / energy
	private int protein; // Protein = special currency
	private int ribosomes; // Number of ribosomes
	private int ribosomesLimit;
	private bool moving; // Cell progresses along map when moving, but this uses ATP
	
	public Cell (string cellType){
		this.cellType = cellType;
		membraneHealthTotal = 30;
		membraneHealthCurrent = membraneHealthTotal;
		wallShieldCurrent = 0; // Initial = 0-total
		wallShieldTotal = 1; // Initial = 1
		wallShieldLimit = 3; // Limits numbers of shield upgrades
		vacuoleWaterLimit = 30;
		vacuoleWater = vacuoleWaterLimit / 2 ;
		chloroplastWater = 0;
		chloroplastWaterLimit = vacuoleWaterLimit / 2;
		foodLimit = 10;
		food = foodLimit / 2;
		glucose = 0;
		atp = 30;
		protein = 1;
		ribosomes = 1;
		ribosomesLimit = 3; //Upgrades = 3,6,9
	}

	public string CellType{
		get{ return cellType; }
		set{ cellType = value;}
	}
	
	public int MembraneHealthCurrent{
		get{ return membraneHealthCurrent; }
		set{ membraneHealthCurrent = value;}
	}

	public int MembraneHealthTotal{
		get{ return membraneHealthTotal; }
		set{ membraneHealthTotal = value;}
	}

	public int WallShieldCurrent{
		get{ return wallShieldCurrent; }
		set{ wallShieldCurrent = value;}
	}
	
	public int WallShieldTotal{
		get{ return wallShieldTotal; }
		set{ wallShieldTotal = value;}
	}
	
	public int VacuoleWater{
		get{ return vacuoleWater; }
		set{ vacuoleWater = value;}
	}
	
	public int VacuoleWaterLimit{
		get{ return vacuoleWaterLimit; }
		set{ vacuoleWaterLimit = value;}
	}

	public int ChloroplastWater{
		get{ return chloroplastWater; }
		set{ chloroplastWater = value;}
	}

	public int ChloroplastWaterLimit{
		get{ return chloroplastWaterLimit; }
		set{ chloroplastWaterLimit = value;}
	}

	public int Food{
		get{ return food; }
		set{ food = value;}
	}
	
	public int FoodLimit{
		get{ return foodLimit; }
		set{ foodLimit = value;}
	}
	
	public int Glucose{
		get{ return glucose; }
		set{ glucose = value;}
	}
	
	public int Atp{
		get{ return atp; }
		set{ atp = value;}
	}
	
	public int Protein{
		get{ return protein; }
		set{ protein = value;}
	}

	public int Ribosomes{
		get{ return ribosomes; }
		set{ ribosomes = value;}
	}

	public int RibosomesLimit{
		get{ return ribosomesLimit; }
		set{ ribosomesLimit = value;}
	}
}

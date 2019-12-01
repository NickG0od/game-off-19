using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Data : MonoBehaviour {
	public static Data Instance {get; private set;}
	public int currentType; // номер куба : 0-по умолчанию
	public int countElements = 15; // кол - во элементов
	public int[] isPowerLimit;
	public int[] isBought;
	public int[] powerCostElement;
	public int[] costElement;
	public float[] healthElement;
    public int[] powerEffElement;
	public float[] speedElement;
    // Объявление максимальных значений парметров кубика
    public float health_Max;
    public float powerEff_Max;
    public float speed_Max;

    void Awake ()
	{
		Instance = this;
		currentType = 0;
		isPowerLimit = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
		powerCostElement = new int[] {0, 0, 0, 5000, 5000, 5000, 10000, 10000, 10000, 15000, 15000, 15000, 20000, 20000, 20000};
		isBought = new int[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
		costElement = new int[] {0, 270, 420, 660, 840, 1010, 1230, 1475, 1620, 1845, 2010, 2255, 2415, 2650, 2870};
		healthElement = new float[] {100f, 110f, 110f, 130f, 130f, 150f, 170f, 170f, 180f, 190f, 200f, 210f, 210f, 230f, 240f};
        powerEffElement = new int[] {10, 15, 15, 20, 20, 25, 30, 35, 40, 40, 45, 50, 50, 55, 55};
		speedElement = new float[] {3f, 3.2f, 3.25f, 3.4f, 3.5f, 3.58f, 3.6f, 3.7f, 3.8f, 3.83f, 4.0f, 4.1f, 4.14f, 4.2f, 4.31f};

        health_Max = 500f;
        powerEff_Max = 150f;
        speed_Max = 8f;
    }
}
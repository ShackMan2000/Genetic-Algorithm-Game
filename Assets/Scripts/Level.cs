using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="Level", menuName ="Level")]
public class Level : ScriptableObject {

    public int levelID;
    public float zoomLevel;

    public Vector3[] GatePositions, gateRotations;
    public Vector3 goalPosition;

    public int[] gateColors;
    public int mergedColor;

    public int scoreNeeded;

    public GameObject grid;





}

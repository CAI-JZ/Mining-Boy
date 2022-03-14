using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = ("RockUnit"))]
public class Rock : ScriptableObject
{
    public float Value;
    public float MaxDurability;
    public Sprite ArtWork;
    public int AppearanceLevel;
    public string SpiecalFeature;
    public float FeatureValue;
    public int RockLevel;
}

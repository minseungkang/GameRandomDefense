using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerAttributes : MonoBehaviour
{
    public enum Attributes
    {
        UnitId, Name, TowerType, Atk, Firerate,Rank, IsGOTY
    }

    public enum Types
    {
        Sword, Gun, Magic
    }

    public static int MaxRank { get; } = 3;

    public static int MinRank { get; } = 1;
}

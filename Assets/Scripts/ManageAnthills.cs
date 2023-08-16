using Enemies;
using UnityEngine;

public class ManageAnthills : MonoBehaviour
{
    private static Anthill[] _anthills;

    private void Awake()
    {
        _anthills = FindObjectsOfType<Anthill>();
    }

    public static Anthill GetRandomAnthill()
    {
        return _anthills[Random.Range(0, _anthills.Length)];
    }
}
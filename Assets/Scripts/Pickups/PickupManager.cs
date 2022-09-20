using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour
{
    public static PickupManager instance;

    [SerializeField] private float _minTimeBetweenPickupSpawns;

    private List<Pickup> _pickups;
    private float _lastSpawnPickupTime;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _pickups = new List<Pickup>();
        _lastSpawnPickupTime = Time.time;
    }

    public void SpawnPickupOnChance(Pickup pickup, float chance, Vector3 position)
    {
        if (Time.time - _lastSpawnPickupTime < _minTimeBetweenPickupSpawns)
            return;

        float pickupChance = UnityEngine.Random.Range(0f, 1f);
        if (pickupChance <= chance && pickup != null)
        {
            Pickup pickupInstance = Instantiate(pickup, position, Quaternion.identity);
            _pickups.Add(pickupInstance);
        }

        _lastSpawnPickupTime = Time.time;
    }

    public void RemovePickup(Pickup pickup)
    {
        _pickups.Remove(pickup);
        Destroy(pickup.gameObject);
    }

    public void DestroyPickupsOnScreen()
    {
        while(_pickups.Count > 0)
        {
            Pickup pickup = _pickups[_pickups.Count-1];
            _pickups.RemoveAt(_pickups.Count - 1);
            Destroy(pickup.gameObject);
        }
    }
}

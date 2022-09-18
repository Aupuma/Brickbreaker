using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesIndicator : MonoBehaviour
{
    [SerializeField] private GameObject _LifeIndicatorUI;

    private void Start()
    {
        //Empty();
    }

    public void Refill(int lives)
    {
        for (int i = 0; i < lives; i++)
        {
            Instantiate(_LifeIndicatorUI, transform);
        }
    }

    private void Empty()
    {
        while(transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }

    public void RemoveLife()
    {
        Destroy(transform.GetChild(transform.childCount - 1).gameObject);
    }

}

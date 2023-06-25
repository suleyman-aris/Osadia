using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    private HealthSystem _healthSystem;
    private void Setup(HealthSystem healthSystem)
    {
        this._healthSystem = healthSystem;
    }
}

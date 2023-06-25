
using UnityEngine;

public class HealthSystemComponent : MonoBehaviour, IGetHealthSystem
{

        [Tooltip("Maximum Health amount")]
        [SerializeField] private float healthAmountMax = 100f;

        [Tooltip("Starting Health amount, leave at 0 to start at full health.")]
        [SerializeField] private float startingHealthAmount;

        private HealthSystem healthSystem;


        private void Awake() 
        {
            // Create Health System
            healthSystem = new HealthSystem(healthAmountMax);

            if (startingHealthAmount != 0) {
                healthSystem.SetHealth(startingHealthAmount);
            }
        }

        
        public HealthSystem GetHealthSystem()
        {
            return healthSystem;
        }


    }

using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    [Header("Health Data")]
    [Tooltip("The health of the object.")] public float currentHealth; 
    [Tooltip("The total health pool the object has")] public float maxHealth;
    public UnityEvent onTakeDamage;

    public Slider HealthBar;
    [SerializeField]
    protected Character character;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        character = GetComponent<Character>();
	}
	
	// Update is called once per frame
	void Update () {
		
        //Health Decrease
        if (Input.GetKeyDown(KeyCode.G))
        {
            TakeDamage(10);
        }
        //Health Increase
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(-10);
        }

	}

    public void TakeDamage(float amountOfDamage)
    {
        currentHealth -= amountOfDamage;

        //healthbar properties, some objects don't have health bars yet. 
        if (HealthBar != null)
        {
            float healthbarPercentage = currentHealth / maxHealth;
            HealthBar.value = healthbarPercentage;
        }


        if (currentHealth <= 0)
        {
            Die();
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        onTakeDamage.Invoke();
    }

    public void Die()
    {
        if (currentHealth <= 0)
        {
            character.Die();
        }
    }
}

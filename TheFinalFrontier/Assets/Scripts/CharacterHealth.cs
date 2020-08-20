using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CharacterHealth : MonoBehaviour
{
    public float CurrentHealth { get; set; }
    public float MaxHealth { get; set; }
    private bool Healing = false;
    public Slider healthbar;


    // Start is called before the first frame update
    void Start()
    {
        MaxHealth = 100f; 
        // resets health to full on game load
        CurrentHealth = MaxHealth;

        healthbar.value = CalculateHealth();
    }

    IEnumerator Regen(){
        Healing = true;
        yield return new WaitForSeconds(3.0f);
        while(CurrentHealth < MaxHealth){
            CurrentHealth += 0.5f;
            yield return new WaitForSeconds(0.01f);
        }
        Healing = false;
    }


    // Update is called once per frame
    void Update()
    {
        healthbar.value = CalculateHealth();
        if(Input.GetKeyDown(KeyCode.X)){
            DealDamage(10);
        }
    }

    public void DealDamage(float damageValue){
        CurrentHealth -= damageValue; // deducts damage amount to the player's health
        //healthbar.value = CalculateHealth();
        Debug.Log("Damage Taken: -10 health.   Total Current Health: " + CurrentHealth);

        if(CurrentHealth <= 0){ // if character out of healhth, then die
            Die();
            SceneManager.LoadScene("GameOver");
        }

        //If the player has not started healing then begin healing
        if(Healing == false){
            StartCoroutine(Regen());
        //Else if the player is healing while being attacked, restart the healing process
        }else{
            StopAllCoroutines();
            StartCoroutine(Regen());
        }
        
    }

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }


    void Die(){
        CurrentHealth = 0;
        Debug.Log("You Died");
    }
}

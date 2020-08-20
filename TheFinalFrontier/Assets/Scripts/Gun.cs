using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Gun : MonoBehaviour
{

    public float fireRate;
    private int Reserve;
    private int Clip;
    public int ClipCapacity;
    public int ReserveCapacity;
    public float reloadTime = 1f;
    private bool isReloading = false;
    private bool isShooting = false;
    //public GameObject FireGameObject;
    //private Fire FireObject;
    public GameObject CrosshairGameObject;
    private Crosshair CrosshairObject;
    public bool ammoCrate = false;
    //Inventory inventory;
    public GameObject[] inventoryHere;// = new GameObject[9];
    public int weaponSelected = 0;
    public Text TextAmmo;
    bool pickedUpAmmoCrate  = false;
    public TabButton theTabButtonFile;
    private bool DidClickOnUI = false;
    public float Damage = 25.0f;
    public GameObject Barrel;
    public Text ReloadingText;
    public AudioClip ShotSound;
    public AudioClip ReloadSound;
    public AudioSource AudioSourceShot;
    public AudioSource AudioSourceReload;
    public SoundController soundController;

    void Awake() {
        CrosshairObject = CrosshairGameObject.GetComponent<Crosshair>();
        //FireObject = FireGameObject.GetComponent<Fire>();
    }

    void Start()
    {
        Clip = ClipCapacity;
        Reserve = ReserveCapacity;
        AudioSourceShot.clip = ShotSound;
        AudioSourceReload.clip = ReloadSound;
        //currentAmmo = maxAmmo;
        //Debug.Log("--- currentAmmo for " + gameObject.name + " = " + currentAmmo);
        //Debug.Log("--- currentAmmo : " + currentAmmo);
    }


    void OnEnable() // called every time the gameobject is enabled
    {
        isReloading = false;
        isShooting = false;
    }

 
    public void AmmoCrateAquired(GameObject currentObject)
    {
        pickedUpAmmoCrate = true;
        //StartCoroutine( Reload() );
        currentObject.SetActive(false);
        ammoCrate = true;
    }

    // Update is called once per frame
    void Update()
    {     
        //For pistol:   
        if(Reserve == -1){
            TextAmmo.text = String.Concat("Ammo: ", Clip.ToString(), "/Inf.");
        //For every other gun:    
        }else{
            TextAmmo.text = String.Concat("Ammo: ", Clip.ToString(), "/", Reserve.ToString());    
        }
        DidClickOnUI = theTabButtonFile.GetComponent<TabButton>().clickOnUI;
        //DidClickOnUI = theTabButtonFile.clickOnUI;
        
        if(isReloading)
        return;
        
        //bool pickedUpAmmoCrate  = false;
 
        //foreach( GameObject otherweapon in inventoryHere)
        //{
        //    pickedUpAmmoCrate = otherweapon.GetComponent<Gun>().ammoCrate;
        //    if(pickedUpAmmoCrate) // if any weapon in your inventory has an AmmoCrate, set var to true to allow any weapon to reload
        //        break;
        //}
    
        if(pickedUpAmmoCrate){
            Debug.Log("--- inside if (pickedUpAmmoCrate)  : current gun: "  + this.name + "  this.ammocrate: "  + this.ammoCrate );
             
            var i = 0;
            foreach (GameObject weapon in inventoryHere)
            {
                Debug.Log("--- in foreach. Current gun: " + weapon.gameObject.name + " : ammoCrate : " + weapon.GetComponent<Gun>().ammoCrate );
                if( weapon.GetComponent<Gun>().Clip < ClipCapacity || weapon.GetComponent<Gun>().Reserve < ReserveCapacity )
                    Debug.Log("--- this gun had lower ammo: " + weapon.GetComponent<Gun>().name + "   ammo: " + weapon.GetComponent<Gun>().Clip );
                
                weapon.GetComponent<Gun>().Clip = weapon.GetComponent<Gun>().ClipCapacity;
                weapon.GetComponent<Gun>().Reserve = weapon.GetComponent<Gun>().ReserveCapacity;

                Debug.Log("--- before this.ammoCrate: " + this.ammoCrate);  

                ammoCrate = false;
                this.ammoCrate = false;
                pickedUpAmmoCrate = false;
                this.pickedUpAmmoCrate = false;
                Debug.Log("--- after this.ammoCrate: " + this.ammoCrate);  
                //Debug.Log("--- after reloading: " + weapon.GetComponent<Gun>().name + "   ammo: " + weapon.GetComponent<Gun>().Clip );
                //Debug.Log("--- after removing ammocrate: " + weapon.GetComponent<Gun>().name + "   ammocrate = " + ammoCrate + "     weapon.GetComponent<Gun>().ammoCrate = " + weapon.GetComponent<Gun>().ammoCrate );

                
                Debug.Log("--- current i = " + i );  
                i++;
                //return;
            
                
                if( i >= inventoryHere.Length-1 )
                {
                    //Debug.Log("--- at inventoryHere.Length: pickedUpAmmoCrate: "  + pickedUpAmmoCrate );
                    //Debug.Log("--- at inventoryHere.Length: pickedUpAmmoCrate: "  + this.pickedUpAmmoCrate );
                    //Debug.Log("--- after inventoryHere.Length: pickedUpAmmoCrate: "  + this.pickedUpAmmoCrate );
                    //Debug.Log("--- this.ammoCrate: " + this.ammoCrate);
                    Debug.Log("--- Break Now: " );  
                    break;
                }
                               

            }//closes foreach
        }



        
        if(Input.GetButton("Fire1") && Clip <= 0) { 
            Debug.Log("--- No Ammo, need to reload");
        }

        if(Input.GetButton("Fire1") && Clip > 0 && !DidClickOnUI && isShooting == false && isReloading == false)
        {
            StartCoroutine(Shoot());
        }

        if(Input.GetButtonDown("Reload") && Clip < ClipCapacity ){
            StartCoroutine(Reload());
            //pickedUpAmmoCrate = false;
            Debug.Log("--- PRESSED RELOAD BUTTON.  "  );
        }

        //Auto Reload clip on empty
        if(Clip == 0 && Reserve != 0){
            StartCoroutine(Reload());
        }

        //Rotate the weapon
        Vector3 mousePos = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float ang = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(ang, Vector3.forward);

        //Flip the sprite if rotates too much
        if(transform.rotation.z > 0.75f || transform.rotation.z < -0.75f){
            //transform.eulerAngles = new Vector3(0, 180, transform.rotation.eulerAngles.z);
            transform.Rotate(Vector3.left * 180);
        }else{
        }

    
    }//closes update

    IEnumerator Shoot()
    {
        //FireGameObject.SetActive(true);

        //if(soundController.sfxON == true)
        if( SoundController.Instance.sfxON == true )
            AudioSourceShot.Play();

        //if( soundController.GetComponent<SoundController>().sfxON == true )



        isShooting = true;
        Clip--;
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x,Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 gunPos = new Vector2(transform.position.x, transform.position.y);
        Barrel.GetComponent<Barrel>().Fire();
        //GameObject firedBullet = Instantiate(bullet, transform.position, transform.rotation, transform.parent.parent );
        // RaycastHit2D objectShot = Physics2D.Raycast(transform.position, mousePos - gunPos);
        //Debug.DrawRay(transform.position, mousePos - gunPos, Color.green, 10.0f);

        // if (objectShot.collider != null && objectShot.collider.tag == "Enemy") {
        //if (CrosshairObject.HitEnemy()) {
        //if ((CrossHairCollider.IsTouching(objectShot.collider) || FireCollider.IsTouching(objectShot.collider)) && objectShot.collider.tag == "Enemy") {
         //   var objectShot = CrosshairObject.GetObjectShot();
           // print("You hit an enemy");
           // objectShot.transform.SendMessage("DamageEnemy", Damage, SendMessageOptions.DontRequireReceiver);
        //} else if (FireObject.HitEnemy()) {
          //  var objectShot = FireObject.GetObjectShot();
            //print("You hit an enemy");
            //objectShot.transform.SendMessage("DamageEnemy", Damage, SendMessageOptions.DontRequireReceiver);
        //}
        yield return new WaitForSeconds(fireRate);
        //FireGameObject.SetActive(false);
        isShooting = false;
        //Debug.Log("---Shot fired");
        //Debug.Log("--- currentAmmo for " + gameObject.name + " = " + currentAmmo);
        //Debug.Log("--- currentAmmo : " + currentAmmo);
    }

    
    public IEnumerator Reload()
    {
        if( SoundController.Instance.sfxON == true )
            AudioSourceReload.Play();
        ReloadingText.GetComponent<Text>().enabled = true;
        isReloading = true;
        Debug.Log("---Reloading. duration in seconds:" + reloadTime);
        yield return new WaitForSeconds(reloadTime);
        
        //Reload for pistol
        if(Reserve == -1){
            Clip = ClipCapacity;
        //Reload for everything else
        }else{
            if(Reserve + Clip > ClipCapacity){
                var taken = ClipCapacity - Clip;
                Clip = ClipCapacity;
                Reserve = Reserve - taken;
            }else{
                Clip = Clip + Reserve;
                Reserve = 0;
            }
        }
        ReloadingText.GetComponent<Text>().enabled = false;


        //GameObject[] theInventory = inventoryHere;
        //foreach (GameObject weapon in theInventory)
        //{
         //   weapon.GetComponent<Gun>().currentAmmo = maxAmmo;
        //}//closes for each
        //ammoCrate = false;

        //pickedUpAmmoCrate = false;
        //Debug.Log("--- end of reload. current status of pickedUpAmmoCrate: " + pickedUpAmmoCrate );

        //Debug.Log("--- Clip for " + gameObject.name + " = " + currentAmmo);
        isReloading = false;
    }


}

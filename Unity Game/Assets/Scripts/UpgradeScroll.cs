using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScroll : Interact
{
    public GameObject ui;
    public Button buttonElemental;
    public Button buttonHoly;
    public Button buttonVoid;
 
    private GameObject player;
    // Start is called before the first frame update
    public override void interact()
    {
        ui.SetActive(true);
        
    }

    private void Start()
    {
        
        buttonElemental.onClick.AddListener(elementalClicked);
        buttonHoly.onClick.AddListener(holyClicked);
        buttonVoid.onClick.AddListener(voidClicked);
        player = GameObject.Find("Player");
    }


    private void elementalClicked()
    {
        
        player.GetComponent<Character>().increaseElementalDamage(25);
        Destroy(this.gameObject);
    }
    private void holyClicked()
    {

        player.GetComponent<Character>().increaseHolyDamage(25);
        Destroy(this.gameObject);
    }
    private void voidClicked()
    {

        player.GetComponent<Character>().increaseVoidDamage(25);
        Destroy(this.gameObject);
    }
}

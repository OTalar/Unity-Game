using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellScroll : Interact
{
    public GameObject ui;
    public Button buttonSpell1;
    public Button buttonSpell2;
    public Button buttonClose;
    private GameObject player;
    public GameObject spell;
    public Text spellText;
    //public GameObject projectTilePrefab;
    // Start is called before the first frame update
    public override void interact()
    {
        ui.SetActive(true);

    }

    private void Start()
    {
        spellText.text = spell.GetComponent<BaseSpell>().getSpellName();
        buttonSpell1.onClick.AddListener(spell1Cliked);
        buttonSpell2.onClick.AddListener(spell2Cliked);
        buttonClose.onClick.AddListener(closeClicked);
        player = GameObject.Find("Player");
    }

    private void spell1Cliked()
    {
        GameObject b = GameObject.Instantiate(spell) as GameObject;
        b.transform.parent = player.transform;
        player.GetComponent<Player>().setSpell1(b.GetComponent<BaseSpell>());
        Destroy(this.gameObject);
    }
    private void spell2Cliked()
    {
        GameObject b = GameObject.Instantiate(spell) as GameObject;
        b.transform.parent = player.transform;
        player.GetComponent<Player>().setSpell2(b.GetComponent<BaseSpell>());
        Destroy(this.gameObject);
    }
    private void closeClicked()
    {
        ui.SetActive(false);
    }
}

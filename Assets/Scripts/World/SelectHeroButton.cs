using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectHeroButton : MonoBehaviour
{
    public Transform heroPrefab;
    public Text costTextBox;
    public Image heroHeadshot;
    public Button heroButton;
    private RequisitionManager requisitionManager;
    private HeroSelector heroSelector;
    private HeroStats heroStats;

    void Start(){
        heroSelector = GameObject.FindWithTag("HeroSelector").GetComponent<HeroSelector>();
        requisitionManager = GameObject.FindWithTag("RequisitionManager").GetComponent<RequisitionManager>();
    }
    
    public void spawnHeroAtLocation(){
        Vector3 hardpointLocation = heroSelector.getLastHardpointSelected().position;
        Transform createdUnit = Instantiate(heroPrefab, new Vector3(hardpointLocation.x, 
            hardpointLocation.y + 1, hardpointLocation.z), heroPrefab.rotation);
        createdUnit.GetComponent<FreeHardpoint>().setHardpoint(heroSelector.getLastHardpointSelected().GetComponent<Hardpoint>());
        heroSelector.GetComponent<Animator>().Play("SlideDown");
        requisitionManager.deductPoints(heroStats.getCost());
        heroSelector.getLastHardpointSelected().GetComponent<Hardpoint>().placeUnit();
    }

    public void setHeroPrefab(Transform heroPrefab){
        this.heroPrefab = heroPrefab;
        setHeroStats(heroPrefab.GetComponent<HeroStats>());
        setHeroHeadshot(heroPrefab.GetComponent<HeroImageLibarary>());
    }

    public void setHeroHeadshot(HeroImageLibarary imageLibarary){
        heroHeadshot.sprite = imageLibarary.getHeroHeadshot();
    }

    public void setHeroStats(HeroStats heroStats){
        this.heroStats = heroStats;
        costTextBox.text = ""+heroStats.getCost();
    }

    public void setPosition(Vector3 newPosition){
        transform.position = newPosition;
    }

    public int getCost(){
        return heroStats.getCost();
    }

    public Utils.UnitType getUnitType(){
        return heroStats.getUnitType();
    }

    public void hideButton(){
        gameObject.SetActive(false);
    }

    public void showButton(){
        gameObject.SetActive(true);
    }

    public bool iSActive(){
        return gameObject.activeSelf;
    }

    public void disableButton(){
        heroButton.interactable = false;
    }

    public void enableButton(){
        heroButton.interactable = true;
    }

}

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;


public class HeroDetails : MonoBehaviour
{
  public Transform selectedHero;
  public HeroStats selectedHeroStats;
  public HealthBar selectedHeroHealthBar;
  public Animator heroDetailsAnimator;
  public string openAnimation;
  public string closeAnimation;
  public DetailsState currentState;
  public Image heroProfile;
  public Transform requisitionManagerObject;
  public RequisitionManager requisitionManager;
  public Ability heroAbility;

  //dynamic UI elements
  public Text currentHp;
  public Text currentAttackSpeed;
  public Text unitType;
  public Text currentExperience;
  public Text Level;
  public Text nextLevelXP;
  public Text Health;
  public Text Attack;
  public Text Magic;
  public Text Defense;
  public Text Resistance;
  public Text Agility;
  public Text refundButtonText;
  public Button abilityButton;

  //static text UI elements
  public Text heroName;
  public Text Block;

  public enum DetailsState { Open, Closed }
  void Start()
  {
    selectedHero = null;
    heroDetailsAnimator = gameObject.GetComponent<Animator>();
    currentState = DetailsState.Closed;
    requisitionManager = requisitionManagerObject.GetComponent<RequisitionManager>();
  }

  void Update()
  {
    if (Input.GetMouseButtonUp(0))
    {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit))
      {
        if (hit.transform.tag == "Hero")
        {
          selectedHero = hit.transform;
          openDetails();
        }
      }
    }

    if (currentState == DetailsState.Open)
    {
      updateUI();
    }
  }

  public void openDetails()
  {
    if (currentState == DetailsState.Closed)
    {
      heroDetailsAnimator.Play(openAnimation);
      currentState = DetailsState.Open;
    }
    populateUI();
  }

  public void populateUI()
  {
    HeroDisplayLibrary displayLibrary = selectedHero.GetComponent<HeroDisplayLibrary>();
    heroProfile.sprite = displayLibrary.getHeroProfile();
    heroName.text = displayLibrary.getHeroDisplayName();
    selectedHeroStats = selectedHero.GetComponent<HeroStats>();
    selectedHeroHealthBar = selectedHero.GetComponent<HealthBar>();
    heroAbility = selectedHero.GetComponent<Ability>();

    updateUI();
  }

  public void updateUI()
  {
    currentHp.text = "" + selectedHeroHealthBar.getCurrentHealth() + " / " + selectedHeroHealthBar.getMaxHealth();
    currentAttackSpeed.text = "" + Utils.calculateAttackRate(selectedHeroStats.getUnitType(), selectedHeroStats.getAgility());
    unitType.text = "" + Enum.GetName(typeof(Utils.UnitType), selectedHeroStats.getUnitType());
    currentExperience.text = "" + selectedHeroStats.getExperience();
    Level.text = "" + selectedHeroStats.getLevel();
    nextLevelXP.text = "" + selectedHeroStats.getNextLevelExperience();
    Health.text = "" + selectedHeroStats.getHealth();
    Attack.text = "" + selectedHeroStats.getAttack();
    Magic.text = "" + selectedHeroStats.getMagic();
    Defense.text = "" + selectedHeroStats.getDefense();
    Resistance.text = "" + selectedHeroStats.getResistance();
    Agility.text = "" + selectedHeroStats.getResistance();
    Block.text = "" + selectedHeroStats.getBlock();
    refundButtonText.text = "REFUND\n +" + (int)(selectedHeroStats.getCost() * .66f) + " REQ";
    updateAbilityButtonValidity();
  }

  public void activateAbility()
  {
    Debug.Log("Abilities aren't a thing yet boss");
  }

  public void refundHero()
  {
    int pointsToRefund = (int)(selectedHeroStats.getCost() * .66f);
    requisitionManager.addPoints(pointsToRefund);
    Destroy(selectedHero.gameObject);
    closeDetails();
  }

  public void closeDetails()
  {
    if (currentState == DetailsState.Open)
    {
      heroDetailsAnimator.Play(closeAnimation);
      currentState = DetailsState.Closed;
    }
    selectedHero = null;
  }

  private void updateAbilityButtonValidity()
  {
    if (heroAbility != null)
    {
      if (heroAbility.isAvailable() && !abilityButton.interactable)
      {
        abilityButton.interactable = true;
      }
      else if (!heroAbility.isAvailable() && abilityButton.interactable)
      {
        abilityButton.interactable = false;
      }
    }
    else
    {
      abilityButton.interactable = false;
    }
  }
}

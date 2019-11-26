using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HeroSelector : MonoBehaviour
{
  public Animator heroSelectorUiAnimator;
  public Transform lastHardpointSelected;
  //hero prefabs
  public Transform[] heroRoster;
  public List<SelectHeroButton> heroButtons;
  public GameObject heroButtonPrefab;
  public Transform heroPicker;
  public RequisitionManager requisitionManager;

  public int itemsPerRow = 8;
  public int verticalOffset;
  public int horizontalOffset;
  public int verticalMargin;
  public int horizontalMargin;


  public enum SelectorState { Open, Closed }
  public SelectorState currentState;


  void Start()
  {
    heroButtons = gameObject.GetComponentsInChildren<SelectHeroButton>().ToList();
    heroSelectorUiAnimator = gameObject.GetComponent<Animator>();
    buildHeroPicker();
    requisitionManager = GameObject.FindWithTag("RequisitionManager").GetComponent<RequisitionManager>();
    currentState = SelectorState.Closed;
  }

  void Update()
  {
    if (Input.GetMouseButtonUp(0))
    {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast(ray, out hit))
      {
        if (hit.transform.tag == "Hardpoint" && !hit.transform.GetComponent<Hardpoint>().getHasUnit())
        {
          openHeroSelector();
          lastHardpointSelected = hit.transform;
          updateButtonValidity();
        }
      }
    }
  }

  public Transform getLastHardpointSelected()
  {
    return lastHardpointSelected;
  }

  public void updateButtonValidity()
  {
    heroButtons.ForEach(button =>
    {
      if (button.iSActive() && (requisitionManager.getCurrentRequisitionPoints() < button.getCost() ||
            button.getUnitType() != lastHardpointSelected.GetComponent<Hardpoint>().getHardpointType()))
      {
        button.disableButton();
      }
      else
      {
        button.enableButton();
      }
    });
  }

  public void openHeroSelector()
  {
    if (currentState == SelectorState.Closed)
    {
      heroSelectorUiAnimator.Play("SlideUp");
      currentState = SelectorState.Open;
    }
    else
    {
      heroSelectorUiAnimator.Play("Blink");
    }
  }

  public void closeHeroSelector()
  {
    if (currentState == SelectorState.Open)
    {
      currentState = SelectorState.Closed;
      heroSelectorUiAnimator.Play("SlideDown");
    }

  }

  public void buildHeroPicker()
  {
    for (int i = 0; i < heroButtons.Count; i++)
    {
      if (i < heroRoster.Length)
      {
        heroButtons[i].setHeroPrefab(heroRoster[i]);
      }
      else
      {
        heroButtons[i].hideButton();
      }
    }

    // Unity really doesn't like dynamically created UI, gonna stash this for now and maybe re-visit it later.
    // for(int i = 0; i < heroRoster.Length; i++){
    //    int row = (i/itemsPerRow); 
    //    int x = horizontalOffset + (1+i)*horizontalMargin;
    //    int y = - (row * verticalMargin + verticalOffset);
    //    Debug.Log("Positioning button at row"+row+", ("+x+","+y+")");
    //    Vector2 buttonPosition = new Vector3(x, y);
    //    Transform newButton = Instantiate(heroButtonPrefab).transform;
    //    newButton.SetParent(transform, false);
    //    newButton.GetComponent<RectTransform>().anchoredPosition = buttonPosition;
    //    SelectHeroButton selectButtonScript = newButton.GetComponent<SelectHeroButton>();
    //    selectButtonScript.setHeroPrefab(heroRoster[i]);
    //    selectButtonScript.setPosition(buttonPosition);
    //    heroButtons.Add(selectButtonScript);
    // }
  }
}

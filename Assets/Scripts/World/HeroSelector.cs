using System;
using System.Collections;
using System.Collections.Generic;
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

   void Start(){
      heroSelectorUiAnimator = gameObject.GetComponent<Animator>();
      buildHeroPicker();
      requisitionManager = GameObject.FindWithTag("RequisitionManager").GetComponent<RequisitionManager>();
   }

   void Update(){
      if(Input.GetMouseButtonUp(0)){
         RaycastHit hit;
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         if(Physics.Raycast(ray, out hit)){
            if(hit.transform.tag == "Hardpoint" && !hit.transform.GetComponent<Hardpoint>().getHasUnit()){
               heroSelectorUiAnimator.Play("SlideUp");
               lastHardpointSelected = hit.transform;
               updateButtonValidity();
            }
         }
      }
   }

   public Transform getLastHardpointSelected(){
      return lastHardpointSelected;
   }

   public void updateButtonValidity(){
      heroButtons.ForEach(button => {
         if(requisitionManager.getCurrentRequisitionPoints() < button.getCost() ||
             button.getUnitType() != lastHardpointSelected.GetComponent<Hardpoint>().getHardpointType()){
            button.disableButton();
         } else {
            button.enableButton();
         }
      });
   }

   public void buildHeroPicker(){
      for(int i = 0; i < heroRoster.Length; i++){
         int row = 1 + (i/itemsPerRow); 
         int x = horizontalOffset + (1+i)*horizontalMargin;
         int y = - (row * verticalMargin + verticalOffset);
         Debug.Log("Positioning button at row"+row+", ("+x+","+y+")");
         Vector3 buttonPosition = new Vector3(x, y,  heroPicker.position.z);
         Transform newButton = Instantiate(heroButtonPrefab, buttonPosition, heroPicker.rotation).transform;
         newButton.SetParent(transform, false);
         SelectHeroButton selectButtonScript = newButton.GetComponent<SelectHeroButton>();
         selectButtonScript.setHeroPrefab(heroRoster[i]);
         selectButtonScript.setPosition(buttonPosition);
         heroButtons.Add(selectButtonScript);
      }

   }

   public void hideSelector(){
      heroSelectorUiAnimator.Play("SlideDown");
   }
}

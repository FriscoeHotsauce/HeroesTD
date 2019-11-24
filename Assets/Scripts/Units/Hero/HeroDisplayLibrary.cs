using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDisplayLibrary : MonoBehaviour
{
  public Sprite heroHeadshot;

  public Sprite heroProfile;

  public string heroDisplayName;

  public string getHeroDisplayName()
  {
    return heroDisplayName;
  }

  public Sprite getHeroHeadshot()
  {
    return heroHeadshot;
  }

  public Sprite getHeroProfile()
  {
    return heroProfile;
  }
}


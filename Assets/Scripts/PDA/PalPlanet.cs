using UnityEngine;

public class PalPlanet : IPlanet
{
    private PlanetHandler planetHandler;
    private GameObject outline;
    private Animator animator;
    private string PlanetName;
    public PalPlanet(PlanetHandler planetHandler, GameObject outline, Animator animator, string PlanetName)
    {
        this.planetHandler = planetHandler;
        this.outline = outline;
        this.animator = animator;
        this.PlanetName = PlanetName;
    }

    public void Point(bool isEnter)
    {
        if (planetHandler.isSelected) return;
        animator.SetBool("atPoint", isEnter);
    }

    public void Click()
    {
        if (planetHandler.isSelected) return;

        planetHandler.isSelected = true; 
        outline.SetActive(true);

        animator.SetBool("atPoint", true);
        PlanetManager.SelectPlanet(PlanetName);
    }

    public void PlanetFound()
    {
       //ничего
    }
}

using TMPro;
using UnityEngine;

public class ManuPlanet : IPlanet
{
    private PlanetHandler planetHandler;
    private GameObject outline;
    private Animator animator;
    private string PlanetName;
    private SpriteRenderer render;

    public ManuPlanet(PlanetHandler planetHandler, GameObject outline, Animator animator, string PlanetName, SpriteRenderer render)
    {
        this.planetHandler = planetHandler;

        this.outline = outline;
        this.animator = animator;
        this.render = render;
        this.PlanetName = PlanetName;

        PlanetFound();
    }

    public void Point(bool isEnter)
    {
        if (!PDAController.isCardFound) return;
        if (planetHandler.isSelected) return;
        animator.SetBool("atPoint", isEnter);
    }

    public void Click()
    {
        Debug.Log("Is card found? " + PDAController.isCardFound);
        if (!PDAController.isCardFound) return;
        if (planetHandler.isSelected) return;

        planetHandler.isSelected = true;
        outline.SetActive(true);

        animator.SetBool("atPoint", true);
        PlanetManager.SelectPlanet(PlanetName);
    }

    public void PlanetFound()
    {
        Collider2D collider = render.GetComponent<Collider2D>();
        TextMeshProUGUI planetNamea = render.transform.Find("PlanetName").GetComponent<TextMeshProUGUI>();

        render.color = PDAController.isCardFound ? Color.white : Color.black;
        collider.enabled = PDAController.isCardFound ? true : false;
        planetNamea.text = PDAController.isCardFound ? "MANU" : "$%#@";
    }
}

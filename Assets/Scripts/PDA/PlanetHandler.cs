using UnityEngine;
using UnityEngine.EventSystems;

public class PlanetHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public bool isSelected = false;
    public static PlanetHandler SelectedPlanet;

    [SerializeField] private PlanetType planetType;
    [SerializeField] private string planetName;
    public GameObject outLine;
    public Animator animator;
    private IPlanet planetLogic;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        outLine.SetActive(false);

        switch (planetType)
        {
            case PlanetType.Pal:
                planetLogic = new PalPlanet(this, outLine, animator, planetName);
                break;
            case PlanetType.Manu:
                planetLogic = new ManuPlanet(this, outLine, animator, planetName, GetComponent<SpriteRenderer>());
                break;
        }
    }

    private void Start()
    {
        planetLogic.PlanetFound();
    }

    public void OnPointerEnter(PointerEventData eventData) => planetLogic.Point(true);
    public void OnPointerExit(PointerEventData eventData) => planetLogic.Point(false);

    public void OnPointerClick(PointerEventData eventData)
    {
        if (SelectedPlanet == this)
        {
            return;
        }

        if (SelectedPlanet != null) return;
        SelectedPlanet = this;
        AudioManager.Instance.PlaySound(AudioManager.Instance.SFXSource, AudioManager.Instance.PlanetChoose, AudioManager.Instance.sfxVolume);
        planetLogic.Click();
    }
}

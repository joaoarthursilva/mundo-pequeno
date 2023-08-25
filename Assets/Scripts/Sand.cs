using UnityEngine;

public class Sand : MonoBehaviour
{
    [SerializeField] private float timeToVanish = 1f;
    private bool _hasBeenTouched;
    private GameObject _sandSprite;
    [SerializeField] private Sugar sugarScript;

    private void Awake()
    {
        _hasBeenTouched = false;
        _sandSprite = transform.GetChild(0).gameObject;
        _sandSprite.SetActive(true);
        if (sugarScript == null) return;
        sugarScript.transform.position = gameObject.transform.position;
    }

    private void Start()
    {
        if (sugarScript == null) return;
        sugarScript.GetComponent<Sugar>().DeactivateSugar();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        col.TryGetComponent(out PlayerStats playerStats);
        if (!playerStats) return;
        if (_hasBeenTouched) return;
        _hasBeenTouched = true;
        Invoke(nameof(VanishSand), timeToVanish);
    }

    private void VanishSand()
    {
        if (sugarScript != null)
            sugarScript.GetComponent<Sugar>().ActivateSugar();
        _sandSprite.SetActive(false);
    }
}
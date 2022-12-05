using UnityEngine;
using TMPro;

public class PimientosCasa : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI matertialesTexto;
    [SerializeField] private ContadorCasa contadorCasa;

    private void Awake() => contadorCasa.Changed += UpdateText;
    private void OnDestroy() => contadorCasa.Changed -= UpdateText;
    private void OnEnable()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        matertialesTexto.SetText(contadorCasa.Pimientos.ToString());

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Vida : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI VidaTexto;
    [SerializeField] private ContadoresPlayer contadoresPlayer;

    private void Awake() => contadoresPlayer.Changed += UpdateText;

    private void OnDestroy() => contadoresPlayer.Changed -= UpdateText;

    private void OnEnable()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        VidaTexto.SetText(contadoresPlayer.health.ToString());
    }

}

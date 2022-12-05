using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Pimientos : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MunicionTexto;
    [SerializeField] private ContadoresPlayer contadoresPlayer;

    private void Awake() => contadoresPlayer.Changed += UpdateText;

    private void OnDestroy() => contadoresPlayer.Changed -= UpdateText;

    private void OnEnable()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        MunicionTexto.SetText(contadoresPlayer.Pimientos.ToString());
    }
}

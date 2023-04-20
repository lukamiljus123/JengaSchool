using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class JengaFocusButtonView : MonoBehaviour
{
    [SerializeField] private Jenga _jenga;
    [SerializeField] private TMP_Text _gradeText;
    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _keyText;
    
    private void Awake()
    {
        UpdateGradeText();
        UpdateKeyText();
        _button.onClick.AddListener(ButtonClicked);
    }

    private void UpdateGradeText()
    {
        _gradeText.text = _jenga.Grade.Substring(0, 3);
    }

    private void UpdateKeyText()
    {
        _keyText.text = _jenga.Grade.Substring(0, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(_jenga.KeyCode))
        {
            ButtonClicked();
        }
    }

    private void ButtonClicked()
    {
        ReferenceManager.Instance.CameraMovement.Center = _jenga.transform.position;
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TestMyStackButtonView : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private KeyCode _keyCode;
    [SerializeField] private TMP_Text _keyText;
    
    private void Awake()
    {
        UpdateKeyText();
        _button.onClick.AddListener(ButtonClicked);
    }

    private void UpdateKeyText()
    {
        _keyText.text = _keyCode.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(_keyCode))
        {
            ButtonClicked();
        }
    }
    
    private void ButtonClicked()
    {
        foreach (Transform jenga in ReferenceManager.Instance.JengasParent)
        {
            jenga.GetComponent<Jenga>().TestMyStack();
        }
    }
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RestartGameButtonView : MonoBehaviour
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
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
    }
}
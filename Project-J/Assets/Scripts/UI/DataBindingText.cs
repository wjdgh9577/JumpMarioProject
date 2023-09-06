using UnityEngine;
using UnityEngine.UI;

public class DataBindingText : MonoBehaviour
{
    [SerializeField]
    Text _textComponent;
    [SerializeField]
    string _dataID;

    private TextData _data;

    private void Reset()
    {
        _textComponent = GetComponent<Text>();
        _dataID = gameObject.name;
    }

    private void OnEnable()
    {
        if (_textComponent == null)
        {
            _textComponent = gameObject.GetComponent<Text>();
                
            if (_textComponent == null)
            {
                Debug.LogErrorFormat("{0} doesn't have a text component.", gameObject.name);
            }
        }
        if (string.IsNullOrEmpty(_dataID))
        {
            Debug.LogWarningFormat("Invalid text data ID. {0}", gameObject.name);
            _dataID = gameObject.name;
        }
            
        _data = UIView.GetValue(_dataID);
        _textComponent.text = _data.text;
        _data.callback += UpdateText;
    }

    private void OnDisable()
    {
        _data.callback -= UpdateText;
    }

    public void UpdateText(string text)
    {
        _textComponent.text = text;
    }
}
using UnityEngine;
using UnityEngine.UI;

namespace Runningboy.UI
{
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
}
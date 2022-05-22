using TMPro;
using UnityEngine;

public class Localize : MonoBehaviour
{
    [SerializeField] private string _key;

    private void Start() => ChangeText();

    public void ChangeText() => GetComponent<TextMeshProUGUI>().text = Translator.instance.GetText(_key);
}

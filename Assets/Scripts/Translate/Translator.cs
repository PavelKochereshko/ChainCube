using UnityEngine;
using System.Collections.Generic;

public class Translator : MonoBehaviour
{
    public static Translator instance;

    private readonly string defaultLanguage = "ENG";
    private string language;

    private TranslationsArray translation = new TranslationsArray();

    private List<Localize> _localizes = new List<Localize>();

    public void ChangeLanguage(string language)
    {
        switch (language)
        {
            case "UA":
                this.language = language;
                break;
            default:
                this.language = defaultLanguage;
                break;
        }

        PlayerPrefs.SetString("Language", language);

        FromJson();
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;

        _localizes.AddRange(FindObjectsOfType<Localize>());

        if (PlayerPrefs.GetString("Language") == "")
            ChangeLanguage(defaultLanguage);
        else
            ChangeLanguage(PlayerPrefs.GetString("Language"));
    }

    private void FromJson()
    {
        TextAsset jsonTextFile = null;

        jsonTextFile = Resources.Load<TextAsset>(language);

        if (jsonTextFile == null)
            jsonTextFile = Resources.Load<TextAsset>(defaultLanguage);

        translation = JsonUtility.FromJson<TranslationsArray>(jsonTextFile.text);

        foreach (var localize in _localizes)
            localize.ChangeText();
    }

    public string GetText(string key) => translation.translations.Find(item => item.Key == key).Text;
}

[System.Serializable]
public class TranslationsArray
{
    public List<TranslateObject> translations;
}

[System.Serializable]
public class TranslateObject
{
    public string Key;
    public string Text;
}

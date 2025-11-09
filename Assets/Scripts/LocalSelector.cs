using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;

public class LocalSelector : MonoBehaviour
{
    private bool active = false;

    private void Start()
    {
        int id = PlayerPrefs.GetInt("localKey", 0);
        ChangeLocale(id);
    }

    public void ChangeLocale(int localeId)
    {
        if (active== true)
        {
            return;
        }

        StartCoroutine(SetLocale(localeId));
    }

    private IEnumerator SetLocale(int localeId)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeId];
        PlayerPrefs.SetInt("localKey",localeId);
        active = false;
    }
}

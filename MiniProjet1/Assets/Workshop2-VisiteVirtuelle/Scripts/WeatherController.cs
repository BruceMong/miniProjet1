using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class WeatherController : MonoBehaviour
{
    private string apiKey = "77839f7d0076e26bc1b0a7d9018c1f8f";
    private string apiUrl = "https://api.weatherstack.com/current";
    private string cityName = "Nanterre";

    public TextMeshProUGUI TexteMeteo; // Assurez-vous que le composant TextMeshProUGUI est correctement assigné dans l'inspecteur Unity

    void Start()
    {
        StartCoroutine(GetWeatherData());
    }

    IEnumerator GetWeatherData()
    {
        string requestUrl = $"{apiUrl}?access_key={apiKey}&query={cityName}";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(requestUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Erreur de connexion à l'API météo: " + webRequest.error);
            }
            else
            {
                // Parsez les données JSON ici. Cela dépendra de la structure de votre API météo.
                string jsonData = webRequest.downloadHandler.text;
                WeatherData weatherData = JsonUtility.FromJson<WeatherData>(jsonData);

                // Affichez la température dans le TextMeshProUGUI
                if (weatherData != null && TexteMeteo != null) // Ajoutez la vérification TexteMeteo != null
                {
                    float temperature = weatherData.current.temperature;
                    TexteMeteo.text = $"{cityName} : {temperature} °C";
                }
                else
                {
                    Debug.LogError("Impossible de parser les données météo ou TexteMeteo non initialisé.");
                }
            }
        }
    }
}

[System.Serializable]
public class WeatherData
{
    public CurrentData current;
}

[System.Serializable]
public class CurrentData
{
    public float temperature;
}

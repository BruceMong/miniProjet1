using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class WeatherController : MonoBehaviour
{
    private string apiKey = "77839f7d0076e26bc1b0a7d9018c1f8f";
    private string apiUrl = "https://api.weatherstack.com/current";
    private string cityName = "Nanterre";

    public TextMeshProUGUI TexteMeteo; // Assurez-vous que le composant TextMeshProUGUI est correctement assign� dans l'inspecteur Unity

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
                Debug.LogError("Erreur de connexion � l'API m�t�o: " + webRequest.error);
            }
            else
            {
                // Parsez les donn�es JSON ici. Cela d�pendra de la structure de votre API m�t�o.
                string jsonData = webRequest.downloadHandler.text;
                WeatherData weatherData = JsonUtility.FromJson<WeatherData>(jsonData);

                // Affichez la temp�rature dans le TextMeshProUGUI
                if (weatherData != null && TexteMeteo != null) // Ajoutez la v�rification TexteMeteo != null
                {
                    float temperature = weatherData.current.temperature;
                    TexteMeteo.text = $"{cityName} : {temperature} �C";
                }
                else
                {
                    Debug.LogError("Impossible de parser les donn�es m�t�o ou TexteMeteo non initialis�.");
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

using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class EnregistrementPositions : MonoBehaviour
{
    [System.Serializable]
    private class ChaiseData
    {
        public int idChaise;
        public Vector3 position;
    }

    private List<ChaiseData> positionsChaises = new List<ChaiseData>();
    public GameObject chaisePrefab; // Assurez-vous de définir votre préfab dans l'inspecteur.

    void Start()
    {
        // Charger les positions enregistrées lors du démarrage du jeu
        ChargerPositions();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Enregistrer les positions actuelles des chaises
            EnregistrerPositions();
        }
    }

    void EnregistrerPositions()
    {
        positionsChaises.Clear();

        // Parcourir toutes les chaises
        GameObject[] chaises = GameObject.FindGameObjectsWithTag("CESI_chair");
        for (int i = 0; i < chaises.Length; i++)
        {
            ChaiseData chaiseData = new ChaiseData();
            chaiseData.idChaise = i; // Utilisez un ID unique, ici nous utilisons l'index du tableau.
            chaiseData.position = chaises[i].transform.position;

            positionsChaises.Add(chaiseData);

            // Débogage : Affiche l'ID et la position de chaque chaise enregistrée
            Debug.Log("Position enregistrée pour Chaise ID " + chaiseData.idChaise + " : " + chaiseData.position);
        }

        // Convertir la liste en JSON
        string json = JsonUtility.ToJson(positionsChaises, true);

        // Enregistrer le JSON dans un fichier
        string cheminFichier = Application.persistentDataPath + "/positionsChaises.json";
        File.WriteAllText(cheminFichier, json);

        Debug.Log("Positions des chaises enregistrées dans : " + cheminFichier);
    }

    void ChargerPositions()
    {
        string cheminFichier = Application.persistentDataPath + "/positionsChaises.json";

        // Vérifier si le fichier existe
        if (File.Exists(cheminFichier))
        {
            // Lire le contenu du fichier JSON
            string json = File.ReadAllText(cheminFichier);

            // Convertir le JSON en liste d'objets ChaiseData
            positionsChaises = JsonUtility.FromJson<List<ChaiseData>>(json);

            // Appliquer les positions chargées aux chaises
            foreach (ChaiseData chaiseData in positionsChaises)
            {
                // Instancier une nouvelle chaise à partir du préfab
                GameObject nouvelleChaise = Instantiate(chaisePrefab, chaiseData.position, Quaternion.identity);
                nouvelleChaise.name = "Chaise ID " + chaiseData.idChaise.ToString(); // Définir le nom en fonction de l'ID.

                // Débogage : Affiche l'ID et la position chargée de chaque chaise
                Debug.Log("Position chargée pour Chaise ID " + chaiseData.idChaise + " : " + chaiseData.position);
            }

            Debug.Log("Positions des chaises chargées depuis : " + cheminFichier);
        }
        else
        {
            Debug.LogWarning("Le fichier de positions n'existe pas : " + cheminFichier);
        }
    }
}

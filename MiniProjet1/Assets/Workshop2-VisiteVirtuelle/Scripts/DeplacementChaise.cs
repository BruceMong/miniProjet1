using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class DeplacementChaise : MonoBehaviour
{
    private bool enDeplacement = false;
    private Vector3 positionInitiale;
    private Vector3 offset;
    private GameObject chaiseSelectionnee;
    private string jsonFilePath = "Assets/Workshop2-VisiteVirtuelle/chaises.json";
    private List<ChaiseData> chaisesList = new List<ChaiseData>();

    void Start()
    {
        ChargerChaises();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !enDeplacement)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("CESI_chair"))
            {
                enDeplacement = true;
                chaiseSelectionnee = hit.collider.gameObject;
                positionInitiale = chaiseSelectionnee.transform.position;
                offset = chaiseSelectionnee.transform.position - hit.point;

                Debug.Log("Chaise détectée ! Position initiale : " + positionInitiale);
            }
        }
        else if (Input.GetMouseButtonDown(0) && enDeplacement)
        {
            enDeplacement = false;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                chaiseSelectionnee.transform.position = new Vector3(hit.point.x + offset.x, positionInitiale.y, hit.point.z + offset.z);

                Debug.Log("Enregistrement de la chaise - ID : " + chaiseSelectionnee.GetInstanceID() + ", Position : " + chaiseSelectionnee.transform.position);

                EnregistrerChaise(chaiseSelectionnee.GetInstanceID(), chaiseSelectionnee.transform.position);
            }
        }

        if (enDeplacement)
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            Vector3 deplacement = new Vector3(mouseX, 0f, mouseY);

            chaiseSelectionnee.transform.position = new Vector3(positionInitiale.x + deplacement.x - offset.x, positionInitiale.y, positionInitiale.z + deplacement.z - offset.z);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            // Exemple : Changer la position de la chaise avec l'ID 123 à une nouvelle position
            ChangerPositionChaise(123, new Vector3(1.0f, 0.0f, 2.0f));
        }
    }

    void ChargerChaises()
    {
        if (File.Exists(jsonFilePath))
        {
            string json = File.ReadAllText(jsonFilePath);
            chaisesList = JsonUtility.FromJson<List<ChaiseData>>(json);

            Debug.Log("Chaises chargées depuis le fichier : " + json);

            foreach (var chaiseData in chaisesList)
            {
                // Utilisez l'ID pour trouver la chaise
                GameObject chaiseObj = GameObject.Find(chaiseData.idChaise.ToString());
                if (chaiseObj != null)
                {
                    chaiseObj.transform.position = chaiseData.position;
                    Debug.Log("Chaise ID " + chaiseData.idChaise + " positionnée à : " + chaiseData.position);
                }
            }
        }
    }

    void EnregistrerChaise(int idChaise, Vector3 position)
    {
        ChaiseData chaiseData = new ChaiseData
        {
            idChaise = idChaise,
            position = position
        };


        Debug.Log("chaiseData info : " + JsonUtility.ToJson(chaiseData));

        Debug.Log("Chaise ajoutée - ID : " + idChaise + ", Position : " + position);

        string json = JsonUtility.ToJson(chaiseData);
        Debug.Log("Contenu du JSON à enregistrer : " + json);
        File.WriteAllText(jsonFilePath, json);
        Debug.Log("Fichier JSON enregistré à l'emplacement : " + Path.GetFullPath(jsonFilePath));
    }

    void ChangerPositionChaise(int idChaise, Vector3 nouvellePosition)
    {
        int indexChaise = chaisesList.FindIndex(chaise => chaise.idChaise == idChaise);

        if (indexChaise != -1)
        {
            // Créer une copie de l'élément pour éviter l'erreur CS1612
            ChaiseData chaiseModifiee = chaisesList[indexChaise];
            chaiseModifiee.position = nouvellePosition;

            GameObject chaiseObj = GameObject.Find(idChaise.ToString());
            if (chaiseObj != null)
            {
                chaiseObj.transform.position = nouvellePosition;
                Debug.Log("Chaise ID " + idChaise + " positionnée à : " + nouvellePosition);
            }

            // Réassigner l'élément modifié à la liste
            chaisesList[indexChaise] = chaiseModifiee;

            // Mettre à jour le fichier JSON
            SauvegarderChaises();
        }
        else
        {
            Debug.LogWarning("Chaise avec l'ID " + idChaise + " non trouvée dans la liste.");
        }
    }


    void SauvegarderChaises()
    {
        string json = JsonUtility.ToJson(chaisesList);
        File.WriteAllText(jsonFilePath, json);
        Debug.Log("Fichier JSON enregistré à l'emplacement : " + Path.GetFullPath(jsonFilePath));
    }

    [System.Serializable]
    public struct ChaiseData
    {
        public int idChaise;
        public Vector3 position;
    }
}

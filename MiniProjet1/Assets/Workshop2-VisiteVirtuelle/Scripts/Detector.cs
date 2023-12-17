using System.Collections;
using UnityEngine;

public class Detector : MonoBehaviour
{
    // Déclarer une variable publique qui permettra d'accueillir le Transform du pivot de la porte
    public Transform Pivot;

    // Déclarer une variable privée qui aura pour objectif de savoir si la porte est déjà ouverte ou non
    private bool m_bIsOpen = false;

    // Référence à l'AudioSource
    private AudioSource doorAudioSource;

    // Clips audio pour l'ouverture et la fermeture de la porte
    public AudioClip openSound;
    public AudioClip closeSound;

    void Start()
    {
        // Ajoutez un composant AudioSource à l'objet si nécessaire
        doorAudioSource = gameObject.GetComponent<AudioSource>();
        if (doorAudioSource == null)
        {
            doorAudioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    // OnTriggerEnter est appelé lorsqu'un autre collider entre dans le trigger
    private void OnTriggerEnter(Collider other)
    {
        // Ajouter une condition qui vérifiera que l'objet en collision a bien le tag "Player"
        // et que la porte n'est pas déjà ouverte
        if (!other.CompareTag("Player") || m_bIsOpen) return;

        // Appliquer la rotation
        Pivot.Rotate(new Vector3(0, -90, 0));

        // Jouer le son d'ouverture
        if (doorAudioSource != null && openSound != null)
        {
            doorAudioSource.clip = openSound;
            doorAudioSource.Play();
        }

        // Mettre à jour la variable Boolean privée
        m_bIsOpen = true;
    }

    // Ajoutez un gestionnaire pour la fermeture de la porte
    private void OnTriggerExit(Collider other)
    {
        // Ajouter une condition qui vérifiera que l'objet en collision a bien le tag "Player"
        // et que la porte est actuellement ouverte
        if (!other.CompareTag("Player") || !m_bIsOpen) return;

        // Appliquer la rotation inverse
        Pivot.Rotate(new Vector3(0, 90, 0));

        // Jouer le son de fermeture
        if (doorAudioSource != null && closeSound != null)
        {
            doorAudioSource.clip = closeSound;
            doorAudioSource.Play();
        }

        // Mettre à jour la variable Boolean privée
        m_bIsOpen = false;
    }
}

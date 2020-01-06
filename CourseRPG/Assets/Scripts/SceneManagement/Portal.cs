using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using RPG.SceneManagement;
using UnityEngine.AI;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] int sceneToLoad = 1;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Destination destination;
    [SerializeField] float fadeOutTime = 0.5f; 
    [SerializeField] float fadeInTime = 0.5f;
    [SerializeField] float fadeWaitTime = 0.5f;
 
    enum Destination
    {
        A, B, C, D, E, F
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Transition());
        }
    }

    private IEnumerator Transition()
    {
        
        DontDestroyOnLoad(gameObject);
        
        Fader fader = FindObjectOfType<Fader>();
        
        yield return fader.FadeOut(fadeOutTime);

        SavingWrapper wrapper = FindObjectOfType<SavingWrapper>();
        wrapper.Save();

        yield return SceneManager.LoadSceneAsync(sceneToLoad);

        wrapper.Load();

        Portal otherPortal = GetOtherPortal();
        UpdatePlayer(otherPortal);

        wrapper.Save();

        yield return new WaitForSeconds(fadeWaitTime);
        yield return fader.FadeIn(fadeInTime);

        Destroy(gameObject);
    }

    private Portal GetOtherPortal()
    {
        foreach (Portal portal in FindObjectsOfType<Portal>())
        {
            if (portal == this) continue;
            if (portal.destination != destination) continue;
            return portal;
        }
        print("Null");
        return null;
    }

    private void UpdatePlayer(Portal otherPortal)
    {
        GameObject player = GameObject.FindWithTag("Player");
        player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.position);
        player.transform.rotation = otherPortal.spawnPoint.rotation;
    }
}

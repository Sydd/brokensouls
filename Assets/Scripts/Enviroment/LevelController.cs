using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelController : MonoBehaviour
{
    [SerializeField] private CharacterMovement player;
    [SerializeField] private Transform spawnPoint;

    public GameObject menu;
    // Start is called before the first frame update

    bool dead;

    private void Start()
    {
    }

    void PlayerDeath()
    {
        player.OnDeath -= PlayerDeath;
       // player.body.MovePosition(spawnPoint.position);
        Invoke("Revive", 1f);

    }

    void Revive()
    {
        //player.OnDeath += PlayerDeath;
        // player.Revive();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        player.gameObject.SetActive(true);
        player.OnDeath += PlayerDeath;
        menu.SetActive(false);
    }



}

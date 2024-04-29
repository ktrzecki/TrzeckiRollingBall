using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class SpeedPowerUp : MonoBehaviour
{
    [SerializeField] private float speedIncreaseAmount = 10f;
    [SerializeField] private float powerupDuration = 5;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }


    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement playerMovement = other.gameObject.GetComponent <PlayerMovement>();
        if (playerMovement != null )
        {
            StartCoroutine(PowerUpSequence(playerMovement));
        }
    }

    public IEnumerator PowerUpSequence(PlayerMovement playerMovement)
    {
        _collider.enabled = false;


        ActivatePowerUp(playerMovement);


        yield return new WaitForSeconds(powerupDuration);

        DeactivatePowerUp(playerMovement);

        Destroy(gameObject);

    }

    private void ActivatePowerUp(PlayerMovement playerMovement)
    {
        playerMovement.SetMoveSpeed(speedIncreaseAmount);
    }

    private void DeactivatePowerUp(PlayerMovement playerMovement)
    {
        playerMovement.SetMoveSpeed(-speedIncreaseAmount);
    }

}

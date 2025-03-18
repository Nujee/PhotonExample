using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPun
{
    [field: SerializeField] public float Speed { get; private set; } = 5f;

    private void Update()
    {
        if (photonView.IsMine)
        {
            float moveX = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
            float moveZ = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
            transform.Translate(moveX, 0, moveZ);
        }
    }
}
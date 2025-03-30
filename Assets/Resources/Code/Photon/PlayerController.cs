using Photon.Pun;
using UnityEngine;

public class PlayerController : MonoBehaviourPun, IPunObservable
{
    private Vector3 _networkPosition;

    [field: SerializeField] public float Speed { get; private set; } = 5f;

    private void Update()
    {
        if (photonView.IsMine)
        {
            float moveX = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
            float moveZ = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
            transform.Translate(moveX, 0, moveZ);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _networkPosition, Time.deltaTime * 10);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            _networkPosition = (Vector3)stream.ReceiveNext();
        }
    }
}
using UnityEngine;

public class MessageLogger : MonoBehaviour
{
    private void OnEnable() => Bullet.OnHit += LogMessage;
    private void OnDisable() => Bullet.OnHit -= LogMessage;

    private void LogMessage(string message) => Debug.Log(message);
}

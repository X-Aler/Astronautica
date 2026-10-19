using UnityEngine;

public class SoundListener : MonoBehaviour
{
    private Character player;
    private AudioSource AudioSource;

    private float maxDistance = 15f;

    private void Awake()
    {
        AudioSource = GetComponent<AudioSource>();
        player = FindAnyObjectByType<Character>();
    }

    private void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        
        AudioSource.volume = Mathf.Clamp01(1 - distance / maxDistance);
    }
}

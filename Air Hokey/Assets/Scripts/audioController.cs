using UnityEngine;

public class audioController : MonoBehaviour
{
    public AudioClip BallHit, Goal,Lose,Win;
    public AudioSource audioSource;
    private void Start() {
        audioSource=GetComponent<AudioSource>();
    }

    public void BallHitSound(){
        audioSource.PlayOneShot(BallHit);
    }
    public void GoalSound(){
        audioSource.PlayOneShot(Goal);
    }
    public void WinSound(){
        audioSource.PlayOneShot(Win);
    }
    public void LoseSound(){
        audioSource.PlayOneShot(Lose);
    }
}

using UnityEngine;

public class audioController : MonoBehaviour
{
    public AudioClip BallHit, Goal,Lose,Win;
    public AudioSource audioSource;
    private void Start() {
        if(SceneScript.Sounds)
        audioSource=GetComponent<AudioSource>();
    }

    public void BallHitSound(){
        if(SceneScript.Sounds)
        audioSource.PlayOneShot(BallHit);
    }
    public void GoalSound(){
        if(SceneScript.Sounds)
        audioSource.PlayOneShot(Goal);
    }
    public void WinSound(){
        if(SceneScript.Sounds)
        audioSource.PlayOneShot(Win);
    }
    public void LoseSound(){
        if(SceneScript.Sounds)
        audioSource.PlayOneShot(Lose);
    }
}

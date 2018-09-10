using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreen : MonoBehaviour
{

    readonly int k_fartKey = Animator.StringToHash("Fart");

    [SerializeField]
    string m_sceneToLoad;

    [SerializeField]
    Animator m_animator;
    
    [SerializeField]
    AudioSource m_audioSource;

    [SerializeField]
    ParticleSystem m_particleSystem;

    AsyncOperation m_asynOp;
    bool m_played = false;

    // Use this for initialization
    void Start ()
    {
        m_animator.SetTrigger(k_fartKey);
        m_audioSource.Play();
        m_particleSystem.Play();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_asynOp != null)
        {
            if (m_asynOp.progress > .8f && !m_asynOp.allowSceneActivation)
            {
                m_asynOp.allowSceneActivation = true;
            }
            return;
        }

        if (!m_played && !m_particleSystem.isPlaying)
        {
            m_played = true;
            m_asynOp = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(m_sceneToLoad);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody rb;
    public float bounceForce = 700f;
    AudioManager audioManager;
    public GameObject splitPrefab;
    public GameObject trailParticlePrefab;  // S�z�lme s�ras�nda olu�acak Particle System
    private GameObject activeTrailParticle;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // E�er top a�a�� do�ru hareket ediyorsa Particle System'� olu�tur veya g�ncelle
        if (rb.velocity.y < -2f)
        {
            if (activeTrailParticle == null)
            {
                activeTrailParticle = Instantiate(trailParticlePrefab, transform.position, Quaternion.identity);
                activeTrailParticle.transform.parent = null; // Particle System'� topa ba�lama, world space'te b�rak
            }
            else
            {
                // Mevcut Particle System'� topun pozisyonuna ta��
                activeTrailParticle.transform.position = transform.position;

                // E�er daha �nce durdurulmu�sa, tekrar ba�lat ve mevcut partik�lleri temizle
                var particleSystem = activeTrailParticle.GetComponent<ParticleSystem>();
                if (particleSystem.isStopped)
                {
                    particleSystem.Clear(); // �nceki partik�lleri temizler
                    particleSystem.Play();  // Tekrar ba�lat�r
                }
            }
        }

        // E�er top yukar� z�pl�yorsa veya yere yak�nsa Particle System'� durdur
        if (rb.velocity.y >= 0f && activeTrailParticle != null)
        {
            var particleSystem = activeTrailParticle.GetComponent<ParticleSystem>();
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);  // Partik�lleri hemen durdur ve temizle
            }
        }
    }
    private void OnCollisionEnter(Collision other)

    {
        rb.velocity = new Vector3(rb.velocity.x, bounceForce * Time.deltaTime, rb.velocity.z);
        audioManager.Play("Land");

        GameObject newsplit = Instantiate(splitPrefab, new Vector3(transform.position.x, other.transform.position.y + 0.29f, transform.position.z), transform.rotation);

        newsplit.transform.localScale = Vector3.one * Random.Range(0.7f, 1.3f);
        newsplit.transform.parent = other.transform;

        string materialName = other.transform.GetComponent<MeshRenderer>().material.name;
        Debug.Log(materialName);

        if (materialName == "Normal (Instance)")
        {
            Debug.Log("You are safe");
        }

        if (materialName == "UnSafe (Instance)")
        {
            GameManager.gameOver = true;
            audioManager.Play("GameOver");
        }

        if (materialName == "Finish (Instance)" && !GameManager.levelWin)
        {
            GameManager.levelWin = true;
            audioManager.Play("LevelWin");
        }

        if (materialName == "Finish 2 (Instance)" && !GameManager.levelWin)
        {
            GameManager.levelWin = true;
            audioManager.Play("LevelWin");
        }
    }
}


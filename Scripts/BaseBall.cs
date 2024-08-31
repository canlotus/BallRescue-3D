using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBall : MonoBehaviour
{
    Rigidbody rb;
    public float bounceForce = 700f;
    AudioManager audioManager;
    public GameObject splitPrefab;
    public GameObject trailParticlePrefab;  // Süzülme sýrasýnda oluþacak Particle System
    private GameObject activeTrailParticle;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Eðer top aþaðý doðru hareket ediyorsa Particle System'ý oluþtur veya güncelle
        if (rb.velocity.y < -2f)
        {
            if (activeTrailParticle == null)
            {
                activeTrailParticle = Instantiate(trailParticlePrefab, transform.position, Quaternion.identity);
                activeTrailParticle.transform.parent = null; // Particle System'ý topa baðlama, world space'te býrak
            }
            else
            {
                // Mevcut Particle System'ý topun pozisyonuna taþý
                activeTrailParticle.transform.position = transform.position;

                // Eðer daha önce durdurulmuþsa, tekrar baþlat ve mevcut partikülleri temizle
                var particleSystem = activeTrailParticle.GetComponent<ParticleSystem>();
                if (particleSystem.isStopped)
                {
                    particleSystem.Clear(); // Önceki partikülleri temizler
                    particleSystem.Play();  // Tekrar baþlatýr
                }
            }
        }

        // Eðer top yukarý zýplýyorsa veya yere yakýnsa Particle System'ý durdur
        if (rb.velocity.y >= 0f && activeTrailParticle != null)
        {
            var particleSystem = activeTrailParticle.GetComponent<ParticleSystem>();
            if (particleSystem.isPlaying)
            {
                particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);  // Partikülleri hemen durdur ve temizle
            }
        }

        // Top y ekseninde 200 birimden daha aþaðýya düþtüðünde topu ve particle sistemi yok et
        if (transform.position.y < -200f)
        {
            if (activeTrailParticle != null)
            {
                Destroy(activeTrailParticle); // Particle sistemi yok et
            }
            Destroy(gameObject); // Topu yok et
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        rb.velocity = new Vector3(rb.velocity.x, bounceForce * Time.deltaTime, rb.velocity.z);

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
        }

        if (materialName == "Finish (Instance)" && !GameManager.levelWin)
        {
            GameManager.levelWin = true;
        }

        if (materialName == "Finish 2 (Instance)" && !GameManager.levelWin)
        {
            GameManager.levelWin = true;
        }
    }
}

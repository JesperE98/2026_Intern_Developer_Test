using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro;
using UnityEditor.ShaderGraph.Internal;
using System.Collections;

public sealed class GameplayHud : Hud
{
    [Header("UI Elements"), Space(5)]
    [SerializeField] private TMP_Text scoreText;

    [Header("UI Settings"), Space(5)]
    [SerializeField, Range(0, 1)] private float scoreTextUpscaleDuration = 0.5f;
    [SerializeField, Range(0, 1)] private float scoreTextDownscaleDuration = 0.5f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Initialize() {
        base.Initialize();

        GameManager.Instance.OnUpdateScore += UpdateScoreText;

        this.gameObject.SetActive(true);
    }

    public override void Deinitialize() {
        base.Deinitialize();

        GameManager.Instance.OnUpdateScore -= UpdateScoreText;
        this.gameObject.SetActive(false);
    }

    private void UpdateScoreText(float inValue) {
        StartCoroutine(AnimateScoreText(inValue));
    }

    private IEnumerator AnimateScoreText(float inValue) {
        float score = GameManager.Instance.Score;

        score += inValue;
        Vector3 upscaleSize = new(1.5f, 1.5f, 1.5f);
        float elapsed = 0;

        // Upscale
        while (elapsed < scoreTextUpscaleDuration) {
            elapsed += Time.deltaTime;
            float t = elapsed / scoreTextUpscaleDuration;
            scoreText.rectTransform.localScale = Vector3.Lerp(Vector3.one, upscaleSize, t);
            yield return null;
        }

        scoreText.text = "Score: " + score;
        elapsed = 0;

        // Downscale
        while (elapsed < scoreTextDownscaleDuration) {
            elapsed += Time.deltaTime;
            float t = elapsed / scoreTextDownscaleDuration;
            scoreText.rectTransform.localScale = Vector3.Lerp(upscaleSize, Vector3.one, t);
            yield return null;
        }

        scoreText.rectTransform.localScale = Vector3.one;
    }
}

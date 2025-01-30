using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class BikeLightController : MonoBehaviour
{
    [Header("発光量倍率")]
    [SerializeField] private float emissionMin = 0;
    [SerializeField] private float emissionMax = 0;

    [Header("アニメーションスピード")]
    [SerializeField] private float animateSpeed = 0;

    //---------------------------------------

    private ElectricGenerating electricGenerating;
    private Material material;
    private Color color;

    void Awake()
    {
        GameObject parentObj = this.gameObject.transform.parent.gameObject;
        if (parentObj.TryGetComponent<ElectricGenerating>(out var _comp1))
        {
            electricGenerating = _comp1;
        }

        if (this.gameObject.TryGetComponent<Renderer>(out var _comp2))
        {
            material = _comp2.material;
            color = material.GetColor("_EmissionColor");
        }
    }

    void Start()
    {
        generatingBonus = electricGenerating.hapinessGeneratingBounus;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        LightAnimation();
    }

    //発光アニメーション------------------------------

    private float generatingBonus = 0;
    private float emission = 1;

    private void LightAnimation()
    {
        generatingBonus = electricGenerating.hapinessGeneratingBounus;
        float targetEmission = Mathf.Lerp(emissionMin, emissionMax, generatingBonus);
        emission = Mathf.Lerp(emission, targetEmission, animateSpeed);
        material.SetColor("_EmissionColor", color * emission);
    }
}

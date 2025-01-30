using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddictionParticle : MonoBehaviour
{
    [Header("パーティクル")]
    [SerializeField] private List<Particle> particles = new List<Particle>();
    [System.Serializable]
    private struct Particle
    {
        public string itemName;
        public GameObject particle;
    }

    //-------------------------------------------

    private Dictionary<string, bool> isAddictionItems = new Dictionary<string, bool>();

    void Awake()
    {
        if (this.gameObject.transform.parent.gameObject.TryGetComponent<BoredomAndAddiction>(out var _comp1))
        {
            isAddictionItems = _comp1.isAddictionItems;
        }
    }

    void Start()
    {

    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        DisplayParticle();
    }

    //中毒パーティクル表示----------------------------------
    private void DisplayParticle()
    {
        foreach (var _st in particles)
        {
            if (isAddictionItems.ContainsKey(_st.itemName))
            {
                if (isAddictionItems[_st.itemName] == true)
                {
                    _st.particle.SetActive(true);
                }
                else
                {
                    _st.particle.SetActive(false);
                }
            }

        }
    }
}

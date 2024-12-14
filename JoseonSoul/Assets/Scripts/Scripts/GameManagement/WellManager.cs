using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WellManager : MonoBehaviour
{
    [Header("Particle Effects")]
    [SerializeField] private GameObject debuffParticle;
    [SerializeField] private GameObject magicCircleParticle;
    
    private bool isPurified = false;
    private int wellId;
    private GameObject currentParticle;

    void Start()
    {
        // Get well ID from object name (assuming names are like "Well0", "Well1", etc.)
        string wellName = gameObject.name;
        char lastChar = wellName[wellName.Length - 1];
        wellId = int.Parse(lastChar.ToString());

        // Check if well is already purified from GameManager
        isPurified = GameManager.Instance.wellPurified[wellId];
        
        // Initialize appropriate particle effect
        if (isPurified)
        {
            currentParticle = Instantiate(magicCircleParticle, transform.position, Quaternion.identity);
            currentParticle.transform.parent = transform;
        }
        else
        {
            currentParticle = Instantiate(debuffParticle, transform.position, Quaternion.identity);
            currentParticle.transform.parent = transform;
        }
    }

    public void PurifyWell(int wellId)
{
    Debug.Log(wellId.ToString() + " Purified!!");
    if(!wellPurified[wellId])
    {
        stage_UIManager.gameObject.GetComponent<EventResultUIManager>().ActvateEventCanvas("Well Purifed");
        wellPurified[wellId] = true;
        
        // Find the well object and trigger particle effect change
        GameObject well = GameObject.Find("Well" + wellId);
        if (well != null)
        {
            WellManager wellManager = well.GetComponent<WellManager>();
            if (wellManager != null)
            {
                wellManager.PurifyWell();
            }
        }
    }
}
}

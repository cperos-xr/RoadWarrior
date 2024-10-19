using System.Collections.Generic;
using Unity.VRTemplate;
using UnityEngine;

public class PopulateContentXR : MonoBehaviour
{
    public GameObject scrollIconPrefab;
    public Transform scrollContent;
    public List<VehiclePart> vehicleParts;

    [SerializeField] private StepManager stepManager;

    private void Start()
    {
        PopulateMenu();
    }

    void PopulateMenu()
    {
        bool setFirstActive = true;
        foreach (VehiclePart vehiclePart in vehicleParts)
        {
            GameObject newIcon = Instantiate(scrollIconPrefab, scrollContent);
            ScrollImage scrollImage = newIcon.GetComponent<ScrollImage>();
            scrollImage.SetImage(vehiclePart.icon);
            scrollImage.nameText.text = vehiclePart.name;
            scrollImage.part = vehiclePart;

            Step step = new Step();
            step.stepObject = newIcon;
            step.buttonText = vehiclePart.name;

            if (setFirstActive)
            {
                newIcon.SetActive(true);
            }
            else
            {
                newIcon.SetActive(false);
            }

            setFirstActive = false;
            stepManager.m_StepList.Add(step);
        }

    }
}

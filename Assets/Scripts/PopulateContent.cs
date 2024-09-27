using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateContent: MonoBehaviour
{
    public GameObject scrollIconPrefab;
    public Transform scrollContent;
    public List<VehiclePart> vehicleParts;

    [SerializeField] private ScrollViewController scrollViewController;

    private void Awake()
    {
        PopulateMenu();
    }

    void PopulateMenu()
    {
        foreach (VehiclePart vehiclePart in vehicleParts)
        {
            GameObject newIcon = Instantiate(scrollIconPrefab, scrollContent);
            ScrollImage scrollImage = newIcon.GetComponent<ScrollImage>();
            scrollImage.SetImage(vehiclePart.icon);
            scrollImage.nameText.text = vehiclePart.name;
            scrollImage.part = vehiclePart;
            scrollViewController.items.Add(newIcon);
        }

        scrollViewController.UpdateScrollAmount();
    }
}

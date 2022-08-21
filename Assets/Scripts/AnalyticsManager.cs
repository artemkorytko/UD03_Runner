using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using System;

public class AnalyticsManager : MonoBehaviour
{
    public static AnalyticsManager Instance { get; private set; }

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
        }
        catch (ConsentCheckException e)
        {
            // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
        }
    }

    public void LevelStart()
    {
        // Send custom event
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "time", DateTime.Now},
        };
        // The ‘myEvent’ event will get queued up and sent every minute
        AnalyticsService.Instance.CustomData("Level_start", parameters);

        // Optional - You can call Events.Flush() to send the event immediately
        AnalyticsService.Instance.Flush();
    }
    
    public void LevelFinish()
    {
        // Send custom event
        Dictionary<string, object> parameters = new Dictionary<string, object>()
        {
            { "time", DateTime.Now},
        };
        // The ‘myEvent’ event will get queued up and sent every minute
        AnalyticsService.Instance.CustomData("Level_finish", parameters);

        // Optional - You can call Events.Flush() to send the event immediately
        AnalyticsService.Instance.Flush();
    }
}

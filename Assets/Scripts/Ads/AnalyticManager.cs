using System;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticManager : MonoBehaviour
    {
        async  void Start()
        {
            try
            {
                await UnityServices.InitializeAsync();
                List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
            }
            catch (ConsentCheckException e)
            {
                //Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
                Send();
            }
        }

        public void Send()
        {
            //Send custom event
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                {"fabulousString", "hello there"},
                {"sparklingInt", 1337},
                {"spectacularFloat", 0.451f},
                {"peculiarBool", true},

            };
            //The 'myEvent' event will get queued up and sent every minute
            AnalyticsService.Instance.CustomData("Start",parameters);
            AnalyticsService.Instance.CustomData("Win",parameters);
            //Optional - You can call Events.Flush() to send the event immediately
            AnalyticsService.Instance.Flush();
            
            
            OnLevelStart(0, null);
            OnLevelWin(0, null);
        }
        


        public void OnLevelStart(int levelStart, string currentStartLevel)
        {
            Analytics.CustomEvent("LevelStart", new Dictionary<string, object>()
            {
                { "StartLevel", levelStart },
                { "CurrentStateStart", currentStartLevel }
            });
            
            Debug.Log("AnalyticStart");
        }

        public void OnLevelWin(int levelWin, string currentWinLevel)
        {
            Analytics.CustomEvent("LevelWin", new Dictionary<string, object>()
            {
                { "WinLevel", levelWin },
                { "CurrentStateWin", currentWinLevel }
            });
            
            Debug.Log("AnalyticWin");
        }
    }
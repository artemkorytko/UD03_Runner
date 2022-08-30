using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticManager : MonoBehaviour
    {
        public static AnalyticManager Instance { get; private set; }
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
            }
        }
        
        public void OnLevelStart()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
               
            };
            AnalyticsService.Instance.CustomData("LevelStart", parameters);
            AnalyticsService.Instance.Flush();
            Debug.Log("AnalyticStartLevel");
        }

        public void OnLevelWin()
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                

            };
            AnalyticsService.Instance.CustomData("LevelWin", parameters);
            AnalyticsService.Instance.Flush();
                
            Debug.Log("AnalyticWinLevel");
        }
    }
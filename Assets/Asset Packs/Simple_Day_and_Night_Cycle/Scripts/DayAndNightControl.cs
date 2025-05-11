// //2016 Spyblood Games
// // code adapted from Unity asset store

// using UnityEngine;
// using System.Collections;

// [System.Serializable]
// public class DayColors
// {
//     public Color skyColor;
//     public Color equatorColor;
//     public Color horizonColor;
// }

// public class DayAndNightControl : MonoBehaviour
// {
//     public bool StartDay; //start game as day time

//     // public GameObject StarDome;
//     // public GameObject moonState;
//     // public GameObject moon;
//     [Header("Skyboxes")]
// 	//blend value of skybox using SkyBox BlendShader in render settings range 0-1
//     [Range(0,1)] private float SkyboxBlendFactor = 0.0f;
//     public Material daySkybox;
//     public Material nightSkybox;
// 	public Light directionalLight; //the directional light in the scene we're going to work with

//     [Header("Colors")]
//     public DayColors dawnColors;
//     public DayColors dayColors;
//     public DayColors nightColors;

//     [Header("Day")]
//     [SerializeField] float dayLength = 120f; //number of seconds in a day
//     private float quarterDay;
//     private float halfquarterDay;
//     private float dawnTime;
//     private float dayTime;
//     private float duskTime;
//     private float nightTime;
// 	private int currentDay;
// 	private string currentPhase;
//     public float currentTime = 0;

// 	[Range(0, 1)] private float current = 0;

//     [HideInInspector]
//     public float timeMultiplier = 1f; //how fast the day goes by regardless of the dayLength var. lower values will make the days go by longer, while higher values make it go faster. This may be useful if you're siumulating seasons where daylight and night times are altered.
//     public bool showUI;
//     float lightIntensity; //static variable to see what the current light's insensity is in the inspector
//     // Material starMat;

//     Camera targetCam;

//     // Use this for initialization
//     void Start()
//     {
// 		quarterDay = dayLength * 0.25f;
// 		halfquarterDay = dayLength * 0.125f;
// 		dawnTime = 0.0f;
// 		dayTime = dawnTime + halfquarterDay;
// 		duskTime = dayTime + quarterDay + halfquarterDay;
// 		nightTime = duskTime + halfquarterDay;

// 		currentPhase = "day";

//         RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Trilight;
//         // RenderSettings.skybox = daySkybox;
//         // SkyboxBlendFactor = 0f;
//         foreach (Camera c in GameObject.FindObjectsOfType<Camera>())
//         {
//             if (c.isActiveAndEnabled)
//             {
//                 targetCam = c;
//             }
//         }
//         lightIntensity = directionalLight.intensity; //what's the current intensity of the light
//         // // starMat = StarDome.GetComponentInChildren<MeshRenderer>().material;
//         // if (StartDay)
//         // {
//         //     currentTime = 0.3f; //start at morning
//         //     // starMat.color = new Color(1f, 1f, 1f, 0f);
//         // }
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         UpdateLight();
//         UpdateSkyBox();
//         current += (Time.deltaTime / dayLength) * timeMultiplier;

// 		if (current >= 1)
// 		{
// 			current = 0;
// 		}

//         if (currentTime >= dayLength)
//         {
//             currentTime = 0;
//             currentDay++; //make the day counter go up
//         }

// 		// Update the current cycle time:
// 		currentTime += Time.deltaTime;
// 		currentTime = currentTime % dayLength;
//     }

//     void UpdateSkyBox()
//     {
// 		if (currentTime > dawnTime && currentTime < dayTime)
//         {
//             // dawn
//             SkyboxBlendFactor = Mathf.Clamp((SkyboxBlendFactor - 0.01f), 0f, 1f);
//         }
//         if (currentTime > dayTime && currentTime < duskTime)
//         {
//             // day
//             SkyboxBlendFactor = 0f;
//         }
//         if (currentTime > duskTime && currentTime < nightTime)
//         {
//             // dusk
//             SkyboxBlendFactor = Mathf.Clamp((SkyboxBlendFactor + 0.01f), 0f, 1f);
//         }
//         if (currentTime > nightTime && currentTime < dawnTime)
//         {
//             // Night
//             SkyboxBlendFactor = 1.0f;
//         }


// 		if (currentTime > nightTime && currentPhase == "dusk")
// 		{
// 			currentPhase = "night";
// 		} 
// 		else if (currentTime > duskTime && currentPhase == "day")
// 		{
// 			currentPhase = "dusk";
// 		}
// 		else if (currentTime > dayTime && currentPhase == "dawn")
// 		{
// 			currentPhase = "day";
// 		}
// 		else if (currentTime > dawnTime && currentTime < dayTime && currentPhase == "night")
// 		{
// 			currentPhase = "dawn";
// 		}

//         RenderSettings.skybox.SetFloat("_Blend", SkyboxBlendFactor);
//     }

//     void UpdateLight()
//     {
//         // StarDome.transform.Rotate(new Vector3(0, 2f * Time.deltaTime, 0));
//         // moon.transform.LookAt(targetCam.transform);
//         directionalLight.transform.localRotation = Quaternion.Euler((current * 360f) - 90, 170, 0);
//         // moonState.transform.localRotation = Quaternion.Euler((currentTime * 360f) - 100, 170, 0);
//         //^^ we rotate the sun 360 degrees around the x axis, or one full rotation times the current time variable. we subtract 90 from this to make it go up
//         //in increments of 0.25.

//         //the 170 is where the sun will sit on the horizon line. if it were at 180, or completely flat, it would be hard to see. Tweak this value to what you find comfortable.

//         float intensityMultiplier = 1;

//         //if (currentTime <= 0.23f || currentTime >= 0.75f)
// 		if (currentPhase == "dusk" || currentPhase == "dawn")
//         {
// 			// either dawn or dusk
//             intensityMultiplier = 0; //when the sun is below the horizon, or setting, the intensity needs to be 0 or else it'll look weird
//             // starMat.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, Time.deltaTime));
//         }
//         //else if (currentTime <= 0.25f)
// 		else if (currentPhase == "day")
//         {
//             intensityMultiplier = Mathf.Clamp01((current - 0.23f) * (1 / 0.02f));
//             // starMat.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, Time.deltaTime));
//         }
//         // else if (currentTime <= 0.73f)
// 		else if (currentPhase == "night")
//         {
//             intensityMultiplier = Mathf.Clamp01(1 - ((current - 0.73f) * (1 / 0.02f)));
//         }


//         //change env colors to add mood
// 		if (currentPhase == "night")
//         {
// 			// night
//             RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, nightColors.skyColor, Time.deltaTime);
//             RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, nightColors.equatorColor, Time.deltaTime);
//             RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, nightColors.horizonColor, Time.deltaTime);
//         }

// 		if (currentPhase == "dawn")
//         {
// 			// dawn
//             RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, dawnColors.skyColor, Time.deltaTime);
//             RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, dawnColors.equatorColor, Time.deltaTime);
//             RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, dawnColors.horizonColor, Time.deltaTime);
//         }

// 		if (currentPhase == "day")
//         {
// 			// day
//             RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, dayColors.skyColor, Time.deltaTime);
//             RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, dayColors.equatorColor, Time.deltaTime);
//             RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, dayColors.horizonColor, Time.deltaTime);
//         }

//         if (currentPhase == "dusk")
//         {
//             RenderSettings.ambientSkyColor = Color.Lerp(RenderSettings.ambientSkyColor, dayColors.skyColor, Time.deltaTime);
//             RenderSettings.ambientEquatorColor = Color.Lerp(RenderSettings.ambientEquatorColor, dayColors.equatorColor, Time.deltaTime);
//             RenderSettings.ambientGroundColor = Color.Lerp(RenderSettings.ambientGroundColor, dayColors.horizonColor, Time.deltaTime);
//         }
//         directionalLight.intensity = lightIntensity * intensityMultiplier;
//     }

//     public string TimeOfDay()
//     {
//         string dayState = "";
//         if (currentTime > 0f && currentTime < 0.1f)
//         {
//             dayState = "Midnight";
//         }
//         if (currentTime < 0.5f && currentTime > 0.1f)
//         {
//             dayState = "Morning";

//         }
//         if (currentTime > 0.5f && currentTime < 0.6f)
//         {
//             dayState = "Mid Noon";
//         }
//         if (currentTime > 0.6f && currentTime < 0.8f)
//         {
//             dayState = "Evening";

//         }
//         if (currentTime > 0.8f && currentTime < 1f)
//         {
//             dayState = "Night";
//         }
//         return dayState;
//     }

//     void OnGUI()
//     {
//         //debug GUI on screen visuals
//         if (showUI)
//         {
//             GUILayout.Box("Day: " + currentDay);
//             GUILayout.Box(currentPhase);
//             GUILayout.Box("Time slider");
//             GUILayout.VerticalSlider(currentTime, 0f, 1f);
//         }
//     }
// }

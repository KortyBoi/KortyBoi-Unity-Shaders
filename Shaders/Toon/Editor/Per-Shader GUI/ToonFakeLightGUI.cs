//created for Kortana's Fake Light Toon v1.0
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ToonFakeLightEditor : ShaderGUI
{
	private static class Styles
    {
        // sections
        public static GUIContent MainSection = new GUIContent("Main", "Main Texture and Normals");
		public static GUIContent ShadowSection = new GUIContent("Shadow", "Different options for the shadow");
		public static GUIContent OutlineSection = new GUIContent("Outline", "Outline Settings");
		public static GUIContent FakeLighingSection = new GUIContent("Fake Lighting", "Fake Lighting Options");
		public static GUIContent RimlightSection = new GUIContent("Rim Light", "Rim Glow Options");
		
		// Main
		public static GUIContent Color = new GUIContent("Color", "Color used for tinting the main texture");
		public static GUIContent MainTex = new GUIContent("Main Texture", "Main texture for the shader");
		public static GUIContent NormalMap = new GUIContent("Normal Map", "Bump map");
		
		// Shadow
		public static GUIContent ShadowColor = new GUIContent("Shadow Color", "Color used for tinting the shadow");
		public static GUIContent ShadowLevel = new GUIContent("Shadow Level", "Changes the intenstiy of the shadow");
		public static GUIContent ToggleShadowTex = new GUIContent("Use Shadow Texture?", "Do you want to add a dedicated texture to the shadow?");
		public static GUIContent ShadowTex = new GUIContent("Shadow Texture", "Texture for the Shadow");
		
		// Outline
		public static GUIContent LineWidth = new GUIContent("Outline Width", "Changes the thickness of the outline");
		public static GUIContent LineColor = new GUIContent("Outline Color", "Changes the color of the outline");
		
		// Light Pos
		public static GUIContent LightPos = new GUIContent("Fake Light Position", "Changes the direction of the fake sun that emits light");
		
		// Rim light
		public static GUIContent ToggleRimlight = new GUIContent("Use Rimlight?", "Do you want to add a Rim glow?");
		public static GUIContent Rimthicc = new GUIContent("Rimlight Strength", "Changes the how much glow will show");
		public static GUIContent RimColor = new GUIContent("Rimlight Color", "Changes the color of the glow");
	}
	
	GUIStyle m_sectionStyle;
    MaterialProperty m_Albedo = null;
    MaterialProperty m_Albedo_Color = null;
    MaterialProperty m_normal = null;
	
    MaterialProperty m_Shadow_Color = null;
	MaterialProperty m_shadow_level = null;
	MaterialProperty m_use_Shadow_Texture = null;
	MaterialProperty m_shadow_Texture = null;
	
	MaterialProperty m_Outline_Width;
	MaterialProperty m_Color_Outline;
	
	MaterialProperty m_fake_light_position = null;
	
	MaterialProperty m_use_rimlight = null;
	MaterialProperty m_rimlight_power = null;
	MaterialProperty m_rimlight_color = null;
	
	//MaterialProperty m_Kortana = null;
	
	bool m_mainOptions = true;
	bool m_ShadowOptions;
	bool m_OutlineOptions;
	bool m_LightPositionOptions;
	bool m_RimlightOptions;
	bool m_Credits;
	bool m_ChangeLog /*= true*/;
	
	private void FindProperties(MaterialProperty[] props)
    {
		m_Albedo_Color = FindProperty("_Albedo_Color", props);
		m_Albedo = FindProperty("_Albedo", props);
		m_normal = FindProperty("_normal", props);
		
		m_Shadow_Color = FindProperty("_Shadow_Color", props);
		m_shadow_level = FindProperty("_shadow_level", props);
		m_use_Shadow_Texture = FindProperty("_use_Shadow_Texture", props);
		m_shadow_Texture = FindProperty("_shadow_Texture", props);
		
		m_Outline_Width = FindProperty("_Outline_Width", props);
		m_Color_Outline = FindProperty("_Color_Outline", props);
		
		m_fake_light_position = FindProperty("_fake_light_position", props);
		
		m_use_rimlight = FindProperty("_use_rimlight", props);
		m_rimlight_power = FindProperty("_rimlight_power", props);
		m_rimlight_color = FindProperty("_rimlight_color", props);
		
		//m_Kortana = FindProperty("Kortana", props);
	}
	
	private void SetupStyle()
    {
        m_sectionStyle = new GUIStyle(EditorStyles.boldLabel);
        m_sectionStyle.alignment = TextAnchor.MiddleCenter;
    }

    private void ToggleDefine(Material mat, string define, bool state)
    {
        if (state)
        {
            mat.EnableKeyword(define);
        }
        else
        {
            mat.DisableKeyword(define);
        }
    }
    void ToggleDefines(Material mat)
    {
    }

    void LoadDefaults(Material mat)
    {
    }

    void DrawHeader(ref bool enabled, ref bool options, GUIContent name)
    {
        var r = EditorGUILayout.BeginHorizontal("box");
        enabled = EditorGUILayout.Toggle(enabled, EditorStyles.radioButton, GUILayout.MaxWidth(15.0f));
        options = GUI.Toggle(r, options, GUIContent.none, new GUIStyle());
        EditorGUILayout.LabelField(name, m_sectionStyle);
        EditorGUILayout.EndHorizontal();
    }
	
	void DrawMasterLabel()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.richText = true;
        style.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.LabelField("<size=20><color=#00ffaa>Fake Light Toon Shader <size=15>v1.0</size></color></size>", style, GUILayout.MinHeight(24));
		EditorGUILayout.Space();
    }
	
	void DrawBottomLabel()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.richText = true;
        style.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.LabelField("<size=15><color=#ffffff>Made by</color> <color=#00ffaa>Kortana</color></size><size=10><color=red>#4110</color></size>", style, GUILayout.MinHeight(20));
		
		EditorGUILayout.Space();
		GUILayout.BeginHorizontal();
            if (GUILayout.Button("My Website"))
            {
                Application.OpenURL("https://kortana.xyz/");
            }
            if (GUILayout.Button("Donate"))
            {
                Application.OpenURL("https://paypal.me/octob3rvirus/5");
            }
            GUILayout.EndHorizontal();
    }
	
	void DrawChangeLog()
	{
		GUIStyle style = new GUIStyle(GUI.skin.label);
        style.richText = true;
        style.alignment = TextAnchor.MiddleLeft;

        EditorGUILayout.LabelField("<size=15><color=#ffffff>Change Log:</color></size>", style, GUILayout.MinHeight(22));
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("<size=13.5><color=red><b>v1.0</b></color></size>", style, GUILayout.MinHeight(10));
		EditorGUILayout.LabelField(" <size=10> <color=red> - Initial Release</color></size>", style, GUILayout.MinHeight(10));
		/*
		EditorGUILayout.LabelField("<size=13.5><color=red><b>v </b></color></size>", style, GUILayout.MinHeight(10));
		EditorGUILayout.LabelField(" <size=10> <color=red> - </color></size>", style, GUILayout.MinHeight(10));
		*/
	}
	
	void ExtraSpace()
	{
		EditorGUILayout.Space();
		EditorGUILayout.Space();
		EditorGUILayout.Space();
	}
	
    public override void OnGUI(MaterialEditor materialEditor, MaterialProperty[] props)
    {
		Material material = materialEditor.target as Material;
		
        FindProperties(props);
		SetupStyle();
        LoadDefaults(material);
		DrawMasterLabel();
		
		// main section
        m_mainOptions = KortyToonUI.Foldout("Main Stuff", m_mainOptions);
        if (m_mainOptions)
        {
            EditorGUILayout.Space();

            materialEditor.ShaderProperty(m_Albedo, Styles.MainTex);
			materialEditor.ShaderProperty(m_Albedo_Color, Styles.Color);
			materialEditor.ShaderProperty(m_normal, Styles.NormalMap);

            EditorGUILayout.Space();
        }
		
		// shadow section
        m_ShadowOptions = KortyToonUI.Foldout("Shadow Stuff", m_ShadowOptions);
        if (m_ShadowOptions)
        {
            EditorGUILayout.Space();

            materialEditor.ShaderProperty(m_Shadow_Color, Styles.ShadowColor);
			materialEditor.ShaderProperty(m_shadow_level, Styles.ShadowLevel);
			materialEditor.ShaderProperty(m_use_Shadow_Texture, Styles.ToggleShadowTex);
			materialEditor.ShaderProperty(m_shadow_Texture, Styles.ShadowTex);

            EditorGUILayout.Space();
        }
		
		// Outline section
        m_OutlineOptions = KortyToonUI.Foldout("Outline Stuff", m_OutlineOptions);
        if (m_OutlineOptions)
        {
				GUIStyle style = new GUIStyle(GUI.skin.label);
				style.richText = true;
				style.alignment = TextAnchor.MiddleCenter;
            EditorGUILayout.Space();

            materialEditor.ShaderProperty(m_Outline_Width, Styles.LineWidth);
			materialEditor.ShaderProperty(m_Color_Outline, Styles.LineColor);
			EditorGUILayout.LabelField("<size=10><color=#ffffff>Doesn't work as plaaned, this colors the inside of the object.</color></size>", style, GUILayout.MinHeight(14));

            EditorGUILayout.Space();
        }
		
		// Fake Light section
        m_LightPositionOptions = KortyToonUI.Foldout("Fake Light Stuff", m_LightPositionOptions);
        if (m_LightPositionOptions)
        {
            EditorGUILayout.Space();

            materialEditor.ShaderProperty(m_fake_light_position, Styles.LightPos);

            EditorGUILayout.Space();
        }
		
		// Rimlight section
        m_RimlightOptions = KortyToonUI.Foldout("Rim Light Stuff", m_RimlightOptions);
        if (m_RimlightOptions)
        {
            EditorGUILayout.Space();

            materialEditor.ShaderProperty(m_use_rimlight, Styles.ToggleRimlight);
			materialEditor.ShaderProperty(m_rimlight_power, Styles.Rimthicc);
			materialEditor.ShaderProperty(m_rimlight_color, Styles.RimColor);

            EditorGUILayout.Space();
        }
		
		ExtraSpace();
		
		//Change Log
		m_ChangeLog = KortyToonUI.Foldout("Change Log", m_ChangeLog);
		if (m_ChangeLog)
		{
			EditorGUILayout.Space();

            DrawChangeLog();
			
            EditorGUILayout.Space();
        }
		
		// Credits section
        m_Credits = KortyToonUI.Foldout("Credits", m_Credits);
        if (m_Credits)
        {
            EditorGUILayout.Space();

            DrawBottomLabel();
			
            EditorGUILayout.Space();
        }
		
		
		ToggleDefines(material);
    }
}

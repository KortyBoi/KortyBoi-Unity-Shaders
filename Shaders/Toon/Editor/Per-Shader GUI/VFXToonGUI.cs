//created for KortyBoi's Visual Effects (VFX) Toon Shader v1.0 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class VFXToonEditor : ShaderGUI
{
	private static class Styles
	{
		//sections
		public static GUIContent MainSection = new GUIContent("Main", "Main Texture and Normals");
		public static GUIContent ClippingSection = new GUIContent("Clipping Options", "Options for clipping.");
		public static GUIContent SoftnessSection = new GUIContent("Softness Options", "Blend settings");
		public static GUIContent VertexSection = new GUIContent("Vertex Options", "Vertex Offsets");
		public static GUIContent DisplacementSection = new GUIContent("Displacement Options", "");
		public static GUIContent CullingSection = new GUIContent("Culling Options", "Different Culling Options");
		public static GUIContent BandingSection = new GUIContent("Banding Options", "Banding");
		public static GUIContent PolarCoordsSection = new GUIContent("Polar Coordinates Options", "");
		public static GUIContent CircleMaskSection = new GUIContent("Circular Masking Options", "Radius changes");
		public static GUIContent RectMaskSection = new GUIContent("Rectangular Mask Options", "");
		
		//Main
		public static GUIContent MainTex = new GUIContent("Main Texture","");
		public static GUIContent GradientMap = new GUIContent("Gradient Map","");
		public static GUIContent MainColor = new GUIContent("Color","Supports HDR");
		public static GUIContent MainPanSpeed = new GUIContent("Panning Speed","Image Movement");
		
		public static GUIContent SecToggle = new GUIContent("Toggle Secondary Texture","");
		public static GUIContent SecTex = new GUIContent("Secondary Texture","");
		public static GUIContent SecPanSpeed = new GUIContent("Secondary Panning Speed","Image Movement");
		
		public static GUIContent Contrast = new GUIContent("Contrast","");
		public static GUIContent Power = new GUIContent("Power","Intensity");
		
		//Clipping
		public static GUIContent CutoffToggle = new GUIContent("Cutoff","Toggle Alpha Cutoff");
		public static GUIContent CutoffSoftness = new GUIContent("Softness","");
		public static GUIContent BurnColor = new GUIContent("Color Burn","Supports HDR");
		public static GUIContent BurnSize = new GUIContent("Burn Size","How much burning is too much?");
		
		//Softness
		public static GUIContent SoftToggle = new GUIContent("Toggle Softness","");
		public static GUIContent SoftThreshold = new GUIContent("Intersection Threshold Max","");
		
		//Vertex
		public static GUIContent VertexToggle = new GUIContent("Toggle Vertex Offset","");
		public static GUIContent VertexOffset = new GUIContent("Vertex Offset","");
		
		//Displacement
		public static GUIContent DisplacementAmount = new GUIContent("Amount","");
		public static GUIContent DisplacementGuide = new GUIContent("Guide","");
		
		//Culling
		public static GUIContent CullMode = new GUIContent("Culling Mode","");
		
		//Banding
		public static GUIContent BandToggle = new GUIContent("Toggle","");
		public static GUIContent BandNum = new GUIContent("Banding Amount","How many bands");
		
		//PolarCoords
		public static GUIContent PolarCoordsToggle = new GUIContent("Toggle","Spins on Object's 0x,0y,0z Coordinates per Side");
		
		//Circle Mask
		public static GUIContent CircleMaskToggle = new GUIContent("Circle Mask","");
		public static GUIContent OuterRadius = new GUIContent("Outer Radius","");
		public static GUIContent InnerRadius = new GUIContent("Inner Radius","");
		public static GUIContent CMaskSmoothness = new GUIContent("Smoothness","");
		
		//Rect Mask
		public static GUIContent RectMaskToggle = new GUIContent("Rectangle Mask","");
		public static GUIContent RectWidth = new GUIContent("Rectangle Width","");
		public static GUIContent RectHeight = new GUIContent("Rectangle Height","");
		public static GUIContent RectMaskCutoff = new GUIContent("Cutoff","");
		public static GUIContent RMaskSmoothness = new GUIContent("Smoothness","");
	}
	
	GUIStyle m_sectionStyle;
	MaterialProperty m_MainTex = null;
	MaterialProperty m_GradientMap = null;
	MaterialProperty m_MainColor = null;
	MaterialProperty m_MainPanSpeed = null;
	MaterialProperty m_SecToggle = null;
	MaterialProperty m_SecTex = null;
	MaterialProperty m_SecPanSpeed = null;
	MaterialProperty m_Contrast = null;
	MaterialProperty m_Power = null;
	
	MaterialProperty m_CutoffToggle = null;
	MaterialProperty m_CutoffSoftness = null;
	MaterialProperty m_BurnColor = null;
	MaterialProperty m_BurnSize = null;
	
	MaterialProperty m_SoftToggle = null;
	MaterialProperty m_SoftThreshold = null;
	
	MaterialProperty m_VertexToggle = null;
	MaterialProperty m_VertexOffset = null;
	
	MaterialProperty m_CullMode = null;
	
	MaterialProperty m_DisplacementAmount = null;
	MaterialProperty m_DisplacementGuide = null;
	
	MaterialProperty m_BandToggle = null;
	MaterialProperty m_BandNum = null;
	
	MaterialProperty m_PolarCoordsToggle = null;
	
	MaterialProperty m_CircleMaskToggle = null;
	MaterialProperty m_OuterRadius = null;
	MaterialProperty m_InnerRadius = null;
	MaterialProperty m_CMaskSmoothness = null;
	
	MaterialProperty m_RectMaskToggle = null;
	MaterialProperty m_RectWidth = null;
	MaterialProperty m_RectHeight = null;
	MaterialProperty m_RectMaskCutoff = null;
	MaterialProperty m_RMaskSmoothness = null;
	
	bool m_MainSection = true;
	bool m_ClippingSection;
	bool m_SoftnessSection;
	bool m_VertexSection;
	bool m_DisplacementSection;
	bool m_CullingSection;
	bool m_BandingSection;
	bool m_PolarCoordsSection;
	bool m_CircleMaskSection;
	bool m_RectMaskSection;
	
	bool m_Credits;
	bool m_ChangeLog /*= true*/;
	
	private void FindProperties(MaterialProperty[] props)
	{
		m_MainTex = FindProperty("_MainTex", props);
		m_GradientMap = FindProperty("_GradientMap", props);
		m_MainColor = FindProperty("_Color", props);
		m_MainPanSpeed = FindProperty("_PanningSpeed", props);
		m_SecToggle = FindProperty("_SecondTex", props);
		m_SecTex = FindProperty("_SecondaryTex", props);
		m_SecPanSpeed = FindProperty("_SecondaryPanningSpeed", props);
		m_Contrast = FindProperty("_Contrast", props);
		m_Power = FindProperty("_Power", props);
		
		m_CutoffToggle = FindProperty("_Cutoff", props);
		m_CutoffSoftness = FindProperty("_CutoffSoftness", props);
		m_BurnColor = FindProperty("_BurnCol", props);
		m_BurnSize = FindProperty("_BurnSize", props);
		
		m_SoftToggle = FindProperty("_SoftBlend", props);
		m_SoftThreshold = FindProperty("_IntersectionThresholdMax", props);
		
		m_VertexToggle = FindProperty("_VertexOffset", props);
		m_VertexOffset = FindProperty("_VertexOffsetAmount", props);
		
		m_CullMode = FindProperty("_Culling", props);
		
		m_DisplacementAmount = FindProperty("_DisplacementAmount", props);
		m_DisplacementGuide = FindProperty("_DisplacementGuide", props);
		
		m_BandToggle = FindProperty("_Banding", props);
		m_BandNum = FindProperty("_Bands", props);
		
		m_PolarCoordsToggle = FindProperty("_PolarCoords", props);
		
		m_CircleMaskToggle = FindProperty("_CircleMask", props);
		m_OuterRadius = FindProperty("_OuterRadius", props);
		m_InnerRadius = FindProperty("_InnerRadius", props);
		m_CMaskSmoothness = FindProperty("_Smoothness", props);
		
		m_RectMaskToggle = FindProperty("_RectMask", props);
		m_RectWidth = FindProperty("_RectWidth", props);
		m_RectHeight = FindProperty("_RectHeight", props);
		m_RectMaskCutoff = FindProperty("_RectMaskCutoff", props);
		m_RMaskSmoothness = FindProperty("_RectSmoothness", props);
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

        EditorGUILayout.LabelField("<size=20><color=#00ffaa>Visual Effects Toon Shader <size=15>v1.0</size></color></size>", style, GUILayout.MinHeight(24));
		EditorGUILayout.Space();
    }
	
	void DrawBottomLabel()
    {
        GUIStyle style = new GUIStyle(GUI.skin.label);
        style.richText = true;
        style.alignment = TextAnchor.MiddleCenter;

        EditorGUILayout.LabelField("<size=15><color=#ffffff>Made by</color> <color=#00ffaa>KortyBoi</color></size><size=10><color=red>#0001</color></size>", style, GUILayout.MinHeight(20));
		
		EditorGUILayout.Space();
		GUILayout.BeginHorizontal();
            if (GUILayout.Button("My Website"))
            {
                Application.OpenURL("http://kortyboi.com/");
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
		EditorGUILayout.LabelField("<size=13.5><color=red><b>v1.0</b></color></size>", style, GUILayout.MinHeight(15));
		EditorGUILayout.LabelField(" <size=10> <color=red> - Initial Release</color></size>", style, GUILayout.MinHeight(15));
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
		
		//Main
		m_MainSection = KortyToonUI.Foldout("Main/Secondary", m_MainSection);
        if (m_MainSection)
        {
            EditorGUILayout.Space();

			materialEditor.ShaderProperty(m_MainTex, Styles.MainTex);
			materialEditor.ShaderProperty(m_GradientMap, Styles.GradientMap);
			materialEditor.ShaderProperty(m_MainColor, Styles.MainColor);
			materialEditor.ShaderProperty(m_MainPanSpeed, Styles.MainPanSpeed);
			
			materialEditor.ShaderProperty(m_SecToggle, Styles.SecToggle);
			materialEditor.ShaderProperty(m_SecTex, Styles.SecTex);
			materialEditor.ShaderProperty(m_SecPanSpeed, Styles.SecPanSpeed);
			
			//materialEditor.ShaderProperty(m_Contrast, Styles.Contrast);
			materialEditor.ShaderProperty(m_Power, Styles.Power);

            EditorGUILayout.Space();
        }
		
		//Clipping
		m_ClippingSection = KortyToonUI.Foldout("Clipping", m_ClippingSection);
        if (m_ClippingSection)
        {
            EditorGUILayout.Space();

			materialEditor.ShaderProperty(m_CutoffToggle, Styles.CutoffToggle);
			materialEditor.ShaderProperty(m_CutoffSoftness, Styles.CutoffSoftness);
			materialEditor.ShaderProperty(m_BurnColor, Styles.BurnColor);
			materialEditor.ShaderProperty(m_BurnSize, Styles.BurnSize);

            EditorGUILayout.Space();
        }
		
		//Softness
		m_SoftnessSection = KortyToonUI.Foldout("Softness", m_SoftnessSection);
        if (m_SoftnessSection)
        {
            EditorGUILayout.Space();

			materialEditor.ShaderProperty(m_SoftToggle, Styles.SoftToggle);
			materialEditor.ShaderProperty(m_SoftThreshold, Styles.SoftThreshold);

            EditorGUILayout.Space();
        }
		
		//Vertex
		m_VertexSection = KortyToonUI.Foldout("Vertex", m_VertexSection);
        if (m_VertexSection)
        {
            EditorGUILayout.Space();

			materialEditor.ShaderProperty(m_VertexToggle, Styles.VertexToggle);
			materialEditor.ShaderProperty(m_VertexOffset, Styles.VertexOffset);

            EditorGUILayout.Space();
        }
		
		//Displacement
		m_DisplacementSection = KortyToonUI.Foldout("Displacement", m_DisplacementSection);
        if (m_DisplacementSection)
        {
            EditorGUILayout.Space();

			materialEditor.ShaderProperty(m_DisplacementAmount, Styles.DisplacementAmount);
			materialEditor.ShaderProperty(m_DisplacementGuide, Styles.DisplacementGuide);

            EditorGUILayout.Space();
        }
		
		//Culling
		m_CullingSection = KortyToonUI.Foldout("Culling", m_CullingSection);
        if (m_CullingSection)
        {
            EditorGUILayout.Space();

			materialEditor.ShaderProperty(m_CullMode, Styles.CullMode);

            EditorGUILayout.Space();
        }
		
		//Banding
		m_BandingSection = KortyToonUI.Foldout("Banding", m_BandingSection);
        if (m_BandingSection)
        {
            EditorGUILayout.Space();

			materialEditor.ShaderProperty(m_BandToggle, Styles.BandToggle);
			materialEditor.ShaderProperty(m_BandNum, Styles.BandNum);

            EditorGUILayout.Space();
        }
		
		//Polar Coordinates
		m_PolarCoordsSection = KortyToonUI.Foldout("Polar Coordinates", m_PolarCoordsSection);
        if (m_PolarCoordsSection)
        {
            EditorGUILayout.Space();

			materialEditor.ShaderProperty(m_PolarCoordsToggle, Styles.PolarCoordsToggle);

            EditorGUILayout.Space();
        }
		
		//Circle Mask
		m_CircleMaskSection = KortyToonUI.Foldout("Circle Masking", m_CircleMaskSection);
        if (m_CircleMaskSection)
        {
            EditorGUILayout.Space();

			materialEditor.ShaderProperty(m_CircleMaskToggle, Styles.CircleMaskToggle);
			materialEditor.ShaderProperty(m_OuterRadius, Styles.OuterRadius);
			materialEditor.ShaderProperty(m_InnerRadius, Styles.InnerRadius);
			materialEditor.ShaderProperty(m_CMaskSmoothness, Styles.CMaskSmoothness);

            EditorGUILayout.Space();
        }
		
		//Rectangle Mask
		m_RectMaskSection = KortyToonUI.Foldout("Rectangle Masking", m_RectMaskSection);
        if (m_RectMaskSection)
        {
            EditorGUILayout.Space();

			materialEditor.ShaderProperty(m_RectMaskToggle, Styles.RectMaskToggle);
			materialEditor.ShaderProperty(m_RectWidth, Styles.RectWidth);
			materialEditor.ShaderProperty(m_RectHeight, Styles.RectHeight);
			materialEditor.ShaderProperty(m_RectMaskCutoff, Styles.RectMaskCutoff);
			materialEditor.ShaderProperty(m_RMaskSmoothness, Styles.RMaskSmoothness);

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
		
		//Credits
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

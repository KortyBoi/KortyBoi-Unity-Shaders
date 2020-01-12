Shader "KortyBoi/Toon/TrueColor"
{
    Properties {
    [HDR]_Color ("Color", Color) = (1,1,1,1)
	}
 
SubShader {
			Tags { 
				"Queue"="Overlay+9990110" 
				"IgnoreProjector"="True" 
				"RenderType"="Overlay" 
				"PreviewType"="Plane" }
    Color [_Color]
    ZTest Always 
    ZWrite On
    Cull Off
    Pass {}
	}
}

// Shader created with Shader Forge v1.38 - by KortyBoi#0001

Shader "KortyBoi/Toon/Fake Lighting" {
    Properties {
		
		_Albedo_Color ("Texture Color Overlay", Color) = (1,1,1,1)
		_Albedo ("Main Texture", 2D) = "gray" {}
		[Normal]_normal ("Normal Map", 2D) = "bump" {}
		
		
		_Shadow_Color ("Shadow Color", Color) = (0.8602941,0.7336058,0.6515463,1)
        _shadow_level ("Shadow Level", Range(.5, 1)) = 0.5
        [MaterialToggle] _use_Shadow_Texture ("Use Shadow Texture", Float ) = 0
        _shadow_Texture ("Shadow Texture", 2D) = "gray" {}
		
		
        _Outline_Width ("Outline Width", Range(0, 2)) = 0
        _Color_Outline ("Outline Color", Color) = (0,0,0,1)
		
		
        _fake_light_position ("Fake Light Position", Vector) = (1,1,1,0)
		
		
        [MaterialToggle] _use_rimlight ("Use Rimlight", Float ) = 0
        _rimlight_power ("Rimlight Strength", Range(0, 5)) = 3
        _rimlight_color ("Rimlight Color", Color) = (0.5,0.5,0.5,1)
		
		//[Space(20)]
		//[Header(Creator)]
		//KortyBoi("KortyBoi#0001", Range(0.9999, 0001)) = 0001
    }
	
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "Outline"
            Tags {
            }
            Cull Front
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
			
			//float4 Kortana;
			
            uniform float _Outline_Width;
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform float4 _Color_Outline;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( float4(v.vertex.xyz + v.normal*(_Outline_Width*0.0001),1) );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                return fixed4((_Albedo_var.rgb*_Color_Outline.rgb),0);
            }
            ENDCG
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _normal; uniform float4 _normal_ST;
            uniform float _shadow_level;
            uniform float4 _Shadow_Color;
            uniform sampler2D _shadow_Texture; uniform float4 _shadow_Texture_ST;
            uniform float4 _Albedo_Color;
            uniform sampler2D _Albedo; uniform float4 _Albedo_ST;
            uniform float4 _fake_light_position;
            uniform float _rimlight_power;
            uniform float4 _rimlight_color;
            uniform fixed _use_rimlight;
            uniform fixed _use_Shadow_Texture;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _normal_var = UnpackNormal(tex2D(_normal,TRANSFORM_TEX(i.uv0, _normal)));
                float3 normalLocal = _normal_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
		////// Lighting:
                float4 _Albedo_var = tex2D(_Albedo,TRANSFORM_TEX(i.uv0, _Albedo));
                float4 _shadow_Albedo_var = tex2D(_shadow_Texture,TRANSFORM_TEX(i.uv0, _shadow_Texture));
                float node_9016 = 0.5;
                float3 finalColor = (lerp((_Albedo_var.rgb*_Albedo_Color.rgb),(lerp( _Albedo_var.rgb, _shadow_Albedo_var.rgb, _use_Shadow_Texture )*_Shadow_Color.rgb),step(((max(0,dot(_fake_light_position.rgb,normalDirection))*node_9016)+node_9016),_shadow_level))+lerp( 0.0, (_rimlight_color.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),exp(_rimlight_power))), _use_rimlight ));
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ToonFakeLightEditor"
}

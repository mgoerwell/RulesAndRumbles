Shader "Unlit/Toon"
{
    Properties
    {
        _Color("Color", Color) = (0.5, 0.65, 1, 1)
        _MainTex("Main Texture", 2D) = "white" {}
        _AmbientColor("Ambient Color", Color) = (0.4,0.4,0.4,1) //for ambient light purposes
        _SpecularColor("Specular Color", Color) = (0.9,0.9,0.9,1) //reflection tint
        _Glossiness("Glossiness", Float) = 32 //reflection size
        _RimColor("Rim Color", Color) = (1,1,1,1) //rim lighting
        _RimAmount("Rim Amount", Range(0, 1)) = 0.716 //rim lighting size into object
        _RimThreshold("Rim Threshold", Range(0, 1)) = 0.1 //rim  radius
    }
        SubShader{
            Pass {
                Tags {
                "LightMode" = "ForwardBase"
                "PassFlags" = "OnlyDirectional"
                }

                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                #pragma multi_compile_fwdbase

                #include "UnityCG.cginc"
                #include "Lighting.cginc"
                #include "AutoLight.cginc" //included for shadow reception

                struct appdata
                {
                    float4 vertex : POSITION;
                    float4 uv : TEXCOORD0;
                    float3 normal : NORMAL;
                };

                struct v2f
                {
                    float4 pos : SV_POSITION;
                    float2 uv : TEXCOORD0;
                    float3 worldNormal : NORMAL;
                    float3 viewDir : TEXCOORD1; //world view
                    SHADOW_COORDS(2)
                };

                sampler2D _MainTex;
                float4 _MainTex_ST;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.pos = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    //convert Normals to world space for lighting use
                    o.worldNormal = UnityObjectToWorldNormal(v.normal);
                    o.viewDir = WorldSpaceViewDir(v.vertex);//direction from current vertex to camera
                    TRANSFER_SHADOW(o)
                    return o;
                }

                float4 _Color;
                float4 _AmbientColor;
                float _Glossiness;
                float4 _SpecularColor;
                float4 _RimColor;
                float _RimAmount;
                float _RimThreshold;

                float4 frag(v2f i) : SV_Target {
                    //normalize our normals in order to use them in a dot product with the lighting
                    float3 normal = normalize(i.worldNormal);
                    float NdotL = dot(_WorldSpaceLightPos0, normal);
                    //determine intensity based on whether the dot product is positive or negative, with shadows factored in
                    //values below 0 are 0 and above .01 are 1. this is done to blend the transition
                    float shadow = SHADOW_ATTENUATION(i);
                    float lightIntensity = smoothstep(0, 0.01, NdotL * shadow);
                    //adjust the lighting based on the colour of the main directional light
                    float4 light = lightIntensity * _LightColor0;

                    //generate reflections
                    //normalize our world view direction
                    float3 viewDir = normalize(i.viewDir);

                    float3 halfVector = normalize(_WorldSpaceLightPos0 + viewDir); //generate half vector for phong reflection math
                    float NdotH = dot(normal, halfVector); //generate dot product for specular reflection
                    // control size of reflection with pow, mix in light intensity to ensure we're lit, glossiness is done this way for usability
                    float specularIntensity = pow(NdotH * lightIntensity, _Glossiness * _Glossiness);
                    //smooth for toon-like quality
                    float specularIntensitySmooth = smoothstep(0.005, 0.01, specularIntensity);
                    float4 specular = specularIntensitySmooth * _SpecularColor;

                    //add rim lighting
                    float4 rimDot = 1 - dot(viewDir, normal);
                    //smothen and limit to lit areas
                    float rimIntensity = rimDot * pow(NdotL, _RimThreshold);;
                    rimIntensity = smoothstep(_RimAmount - 0.01, _RimAmount + 0.01, rimIntensity);
                    float4 rim = rimIntensity * _RimColor;

                    float4 sample = tex2D(_MainTex, i.uv);

                    return _Color * sample * (_AmbientColor + light + specular + rim);
                }
                ENDCG
            }
                UsePass "Legacy Shaders/VertexLit/SHADOWCASTER" //cast shadows
        }
}

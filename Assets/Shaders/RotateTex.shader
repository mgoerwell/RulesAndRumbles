Shader "Custom/RotateTex"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
        _RotationSpeed("Rotation Speed", Float) = 2.0 
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:vert

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        float _RotationSpeed;
        void vert(inout appdata_full v) {
            //generate needed values for rotation matrix, modified by time
            float timeSpeed = _Time.y / 5;
            v.texcoord.xy -= 0.5;
            float sinX = sin(_RotationSpeed * timeSpeed);
            float cosX = cos(_RotationSpeed * timeSpeed);
            float sinY = sin(_RotationSpeed * timeSpeed);
            //create rotation matrix
            //[cosX, -SinX]
            //[SinY,  CosX]
            float2x2 rotationMatrix = float2x2(cosX, -sinX, sinY, cosX);
            //center rotation
            rotationMatrix *= 0.5;
            rotationMatrix += 0.5;
            rotationMatrix = rotationMatrix * 2 - 1;
            //apply rotation to texture co-ords before texture lookup
            v.texcoord.xy = mul(v.texcoord.xy, rotationMatrix);
            //undo modification for rotation
            v.texcoord.xy += 0.5;
        }

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

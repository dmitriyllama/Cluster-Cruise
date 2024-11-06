Shader "Custom/LightDependentOpacity"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Color", Color) = (1, 1, 1, 1)
        _Smoothness ("Smoothness", Range(0,1)) = 0.5
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        CGPROGRAM
        #pragma surface surf Lambert

        sampler2D _MainTex;
        fixed4 _Color;
        float _Smoothness;

        struct Input
        {
            float2 uv_MainTex;
            float3 worldPos;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 tex = tex2D(_MainTex, IN.uv_MainTex);
        
            float lightIntensity = max(0.0, dot(o.Normal, _WorldSpaceLightPos0.xyz));

            float3 viewDir = normalize(_WorldSpaceCameraPos - IN.worldPos);
            float3 lightDir = normalize(_WorldSpaceLightPos0.xyz);

            float3 halfDir = normalize(lightDir + viewDir);
            float specular = pow(max(dot(o.Normal, halfDir), 0.0), _Smoothness * 128.0);

            o.Albedo = tex.rgb * _Color.rgb;
            o.Alpha = tex.a * lightIntensity * _Color.a;
            o.Specular = specular;
            o.Gloss = _Smoothness;
        }
        ENDCG
    }
    FallBack "Diffuse"
}

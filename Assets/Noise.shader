Shader "Custom/Noise" {
  Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
    _Intesity ("Intensity", Range(0.0, 1.0)) = 0.5
  }
  SubShader {
    Tags { "RenderType"="Opaque" }
    LOD 200

    CGPROGRAM
    #pragma surface surf Lambert

    sampler2D _MainTex;

    struct Input {
      float2 uv_MainTex;
    };

    void surf (Input IN, inout SurfaceOutput o) {
      half4 c = tex2D (_MainTex, IN.uv_MainTex);
      o.Albedo = c.rgb * 1 - (_CosTime * _SinTime) / 10;
      c.rb = c.gb * 1 - (_CosTime / _SinTime) / 10;
      c.gb = c.rg * 1 - (_CosTime * _SinTime) / 10;
      o.Alpha = c.a;
    }
    ENDCG
  }
  FallBack "Diffuse"
}

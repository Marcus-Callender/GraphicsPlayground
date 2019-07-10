// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/OutlineShader"
{
    Properties
    {
		[PerRendererData]_Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0

		_OutlineColor("Outline Color", Color) = (0,0,0,1)
		// values above .03 start to look ugly so it is capped at .03
		_OutlineWidth("Outline width", Range(0, 0.03)) = .005
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

		// block for rendering the outline
		Pass
		{
			// Reverse the normals so only the back is rendered
			Cull Front

			// required for the functions used
			CGPROGRAM
			#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag

			uniform float _OutlineWidth;
			uniform float4 _OutlineColor;

			struct v2f
			{
				float4 pos : POSITION;
				float4 color : COLOR;
			};

			v2f vert(appdata_base v)
			{
				// standard shader naming convention
				v2f o;
				
				// this converts world space to the cameras view
				o.pos = UnityObjectToClipPos(v.vertex);

				// multiplies Inverse Transpose & view matrix with the vertexs normal to get the view matrix normal.
				float3 norm = mul((float3x3)UNITY_MATRIX_IT_MV, v.normal);

				// gets the direction we will move the face in so it peeks out from behind the original model.
				float2 offset = TransformViewToProjection(norm.xy);

				// moves the newly created face away from it's original face by the specified value.
				o.pos.xy += offset * _OutlineWidth;
				
				// sets the outline pixels colour
				o.color = _OutlineColor;
				
				// returns the value
				return o;
			}

			// this colours the pixel if it needs colour
			half4 frag(v2f i) :COLOR
			{
				return i.color;
			}

			// ends the block for creating the outline
			ENDCG
		}

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

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

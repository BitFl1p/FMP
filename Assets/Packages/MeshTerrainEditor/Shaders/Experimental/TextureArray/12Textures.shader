Shader "MTE/Experimental/TextureArray/Diffuse"
{
	Properties
	{
		_SplatArray ("Splat Layers", 2DArray) = "" {}
		_UVScaleOffset ("UV Scale & Offset", Vector) = (10, 10, 0, 0)
		_Control0 ("Control 0 (RGBA)", 2D) = "red" {}
		_Control1 ("Control 1 (RGBA)", 2D) = "black" {}
		_Control2 ("Control 2 (RGBA)", 2D) = "black" {}
	}

	CGINCLUDE
		#pragma surface surf Lambert vertex:MTE_SplatmapVert finalcolor:MTE_SplatmapFinalColor finalprepass:MTE_SplatmapFinalPrepass finalgbuffer:MTE_SplatmapFinalGBuffer
		#pragma multi_compile_fog
		#pragma exclude_renderers d3d9
		//#pragma enable_d3d11_debug_symbols //for debug

		struct Input
		{
			float4 tc;
			UNITY_FOG_COORDS(0)
		};
		
		#include "../../MTE Common.cginc"

		sampler2D _Control0,_Control1,_Control2;
		float4 _UVScaleOffset;
        UNITY_DECLARE_TEX2DARRAY(_SplatArray);

		void MTE_SplatmapVert(inout appdata_full v, out Input data)
		{
			UNITY_INITIALIZE_OUTPUT(Input, data);
			data.tc = v.texcoord;
			float4 pos = UnityObjectToClipPos(v.vertex);
			UNITY_TRANSFER_FOG(data, pos);
		}

		void MTE_SplatmapMix(Input IN, out fixed4 mixedDiffuse)
		{
			half4 splat_control_0 = tex2D(_Control0, IN.tc.xy);
			half4 splat_control_1 = tex2D(_Control1, IN.tc.xy);
			half4 splat_control_2 = tex2D(_Control2, IN.tc.xy);
			
            fixed control[12];
			control[ 0] = splat_control_0.r;
			control[ 1] = splat_control_0.g;
			control[ 2] = splat_control_0.b;
			control[ 3] = splat_control_0.a;
			control[ 4] = splat_control_1.r;
			control[ 5] = splat_control_1.g;
			control[ 6] = splat_control_1.b;
			control[ 7] = splat_control_1.a;
			control[ 8] = splat_control_2.r;
			control[ 9] = splat_control_2.g;
			control[10] = splat_control_2.b;
			control[11] = splat_control_2.a;
				
			half4 weights = 0;
			half4 indexes = 0;

            int i = 0;
            for (i = 0; i < 12; ++i)
            {
               fixed w = control[i];
               if (w >= weights[0])
               {
                  weights[3] = weights[2];
                  indexes[3] = indexes[2];
                  weights[2] = weights[1];
                  indexes[2] = indexes[1];
                  weights[1] = weights[0];
                  indexes[1] = indexes[0];
                  weights[0] = w;
                  indexes[0] = i;
               }
               else if (w >= weights[1])
               {
                  weights[3] = weights[2];
                  indexes[3] = indexes[2];
                  weights[2] = weights[1];
                  indexes[2] = indexes[1];
                  weights[1] = w;
                  indexes[1] = i;
               }
               else if (w >= weights[2])
               {
                  weights[3] = weights[2];
                  indexes[3] = indexes[2];
                  weights[2] = w;
                  indexes[2] = i;
               }
               else if (w >= weights[3])
               {
                  weights[3] = w;
                  indexes[3] = i;
               }
			   //less weighted layers are ignored: not considered in weighted-blending
            }

			mixedDiffuse = 0;
			float2 TransformedUV = IN.tc.xy * _UVScaleOffset.xy + _UVScaleOffset.zw;
            for (i = 0; i < 4; ++i)
			{
				half w = weights[i];
				if(w>0)
				{
					mixedDiffuse +=
						UNITY_SAMPLE_TEX2DARRAY(_SplatArray, float3(TransformedUV, indexes[i]))*w;
				}
			}
		}

		void surf(Input IN, inout SurfaceOutput o)
		{
			fixed4 mixedDiffuse;
			MTE_SplatmapMix(IN, mixedDiffuse);
			o.Albedo = mixedDiffuse.rgb;
			o.Alpha = 1.0;
		}
	ENDCG
	
	Category
	{
		Tags
		{
			"Queue" = "Geometry-99"
			"RenderType" = "Opaque"
		}
		SubShader//for target 3.0+
		{
			CGPROGRAM
				#pragma target 3.0
			ENDCG
		}
	}

	Fallback "Diffuse"
}
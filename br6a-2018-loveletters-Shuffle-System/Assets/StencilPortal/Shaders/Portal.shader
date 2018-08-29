Shader "Custom/StencilPortal"
{
	
	Properties
	{
		_ID ("Card ID", Int) = 1
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" "Queue"="Geometry+1" }
		LOD 200
		ZWrite Off

		Stencil
		{
			Ref [_ID]
			Comp always
			Pass replace
		}

		ColorMask 0

		Pass
		{
			
		}
	}
}

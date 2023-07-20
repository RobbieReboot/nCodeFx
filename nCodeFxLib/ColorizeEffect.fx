//--------------------------------------------------------------------------------------
// 
// WPF ShaderEffect HLSL -- ColorizeEffect
//
//--------------------------------------------------------------------------------------
#define D3DXSHADER_DEBUG
#define D3DXSHADER_SKIPOPTIMIZATION

//-----------------------------------------------------------------------------------------
// Shader constant register mappings (scalars - float, double, Point, Color, Point3D, etc.)
//-----------------------------------------------------------------------------------------

float4 colorFilter : register(C0);
float  alphaIgnore : register(C1);
float  ignoredAlphaValue : register(C2);

#define eightBitTolerance (1.0f/256)

//--------------------------------------------------------------------------------------
// Sampler Inputs (Brushes, including ImplicitInput)
//--------------------------------------------------------------------------------------

sampler2D implicitInputSampler : register(S0);


//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------


float4 main(float2 uv : TEXCOORD) : COLOR
{
	float4	srcPixel = tex2D(implicitInputSampler, uv);
	if (alphaIgnore != 0.0f) 
	{
		if (abs(srcPixel.a - alphaIgnore) <= eightBitTolerance)
		{
			srcPixel.a = lerp(0,srcPixel.a,ignoredAlphaValue);
			return srcPixel;
		}
	}

	float3  LuminanceWeights = float3(0.299,0.587,0.114);
	float	luminance = dot(srcPixel,LuminanceWeights);
	float4	dstPixel = luminance * colorFilter;

    dstPixel *= colorFilter;
    
	//retain the incoming alpha
	dstPixel.a = srcPixel.a;

	return dstPixel;
}



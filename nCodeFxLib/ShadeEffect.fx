//--------------------------------------------------------------------------------------
// 
// WPF ShaderEffect HLSL -- ShadeEffect
//
//--------------------------------------------------------------------------------------

//-----------------------------------------------------------------------------------------
// Shader constant register mappings (scalars - float, double, Point, Color, Point3D, etc.)
//-----------------------------------------------------------------------------------------

float  Shade : register(C0);
float  alphaIgnore : register(C1);
float  ignoredAlphaValue : register(C2);

#define eightBitTolerance (1.0f/256)
//--------------------------------------------------------------------------------------
// Sampler Inputs (Brushes, including ImplicitInput)
//--------------------------------------------------------------------------------------

sampler2D implicitInputSampler : register(S0);

// Brightness: -1.0 to 1.0, default 0.0
// Contrast: 0.0 to 10.0, default 1.0
// Shade: -180.0 to +180.0, default 0.0
// Saturation: 0.0 to 10.0, default 1.0

#define PI			acos(-1)
#define ymin		(16.0/255)
#define ymax		(235.0/255)
#define Brightness	0.0
#define Contrast	1.0
#define Saturation	1.0

static float4x4 r2y = {
	0.299, 0.587, 0.114, 0,
	-0.147, -0.289, 0.437, 0,
	0.615, -0.515, -0.100, 0,
	0, 0, 0, 0
};
static float4x4 y2r = {
	1.0, 0.0, 1.140, 0, 
	1.0, -0.394, -0.581, 0,
	1.0, 2.028, 0.0, 0, 
	0, 0, 0, 0
};

float4 main(float2 uv : TEXCOORD0) : COLOR
{
    float4 inColor = tex2D(implicitInputSampler, uv);
    if (alphaIgnore != 0.0f) 
	{
		if (abs(inColor.a - alphaIgnore) <= eightBitTolerance)
		{
			inColor.a = lerp(0,inColor.a,ignoredAlphaValue);
			return inColor;
		}
	}
    float4 outColor = inColor;
	outColor = mul(r2y, outColor);
	outColor.r = Contrast * (outColor.r - ymin) + ymin + Brightness;
	
	clamp(Shade,0,1);
	float inHue = (Shade * 360.0);
	if (inHue>=180)
		inHue-=360;
	float2x2 HueMatrix = {
		cos(inHue * PI / 180), sin(inHue * PI / 180),
		-sin(inHue * PI / 180), cos(inHue * PI / 180)
	};	
	
	outColor.gb = mul(HueMatrix, outColor.gb) * Saturation;
	outColor = mul(y2r, outColor);
	
	outColor.a = inColor.a;
	return outColor; 
}

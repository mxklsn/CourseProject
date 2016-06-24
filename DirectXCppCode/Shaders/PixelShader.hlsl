//--------------------------------------------------------------------------------------
struct VS_OUTPUT
{
	float4 Pos : SV_POSITION;
	float4 Normal: NORMAL;
	float4 Color : COLOR0;
};
//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(VS_OUTPUT input) : SV_Target
{
	VS_OUTPUT output = (VS_OUTPUT)0;
	return input.Color;
}
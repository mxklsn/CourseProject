//--------------------------------------------------------------------------------------
// Constant Buffer Variables
//--------------------------------------------------------------------------------------
cbuffer ConstantBuffer : register(b0)
{
	matrix View;
	matrix Projection;
	matrix World;
}
//--------------------------------------------------------------------------------------
struct VS_OUTPUT
{
	float4 Pos : SV_POSITION;
	float4 depthPosition : TEXTURE0;
	float4 Normal: NORMAL;
	float4 Color : COLOR0;
};
//--------------------------------------------------------------------------------------
// Vertex Shader
//--------------------------------------------------------------------------------------
VS_OUTPUT main(float4 Pos : POSITION, float4 Color : COLOR, float4 Normal : NORMAL)
{
	VS_OUTPUT output = (VS_OUTPUT)0;
	output.Pos = Pos;
	/*depth*/
	//output.Pos = mul(Pos, World);
	/**/

	output.Pos = mul(Pos, View);
	output.Normal = mul(Normal, View);
	output.Color = Color;

	output.Pos = mul(output.Pos, Projection);
	//output.Normal = mul(output.Normal, Projection);
	output.Color = Color;
	output.Color = Color;

	//output.depthPosition = output.Pos;

	return output;
}

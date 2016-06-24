//--------------------------------------------------------------------------------------
// Constant Buffer Variables
//--------------------------------------------------------------------------------------
cbuffer ConstantBuffer : register(b0)
{
	matrix View;
	matrix Projection;
	//matrix World;
}
//--------------------------------------------------------------------------------------
struct VS_OUTPUT
{
	float4 Pos : SV_POSITION;
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
	output.Pos = mul(Pos, View);
	output.Normal = mul(Normal, View);
	output.Color = Color;
	//output.color = (output.Pos + 1) / 2.0;
	//output.Color = Color;

	//float distanceToLighter0 = 0.5 - output.Pos[0];
	//float distanceToLighter1 = -0.5 - output.Pos[1];
	//float distanceToLighter2 = -1 - output.Pos[2];
	//float distanceToLighter = sqrt(distanceToLighter0*distanceToLighter0 + distanceToLighter1*distanceToLighter1 + distanceToLighter2*distanceToLighter2);
	////float distanceToLighter = sqrt(distanceToLighter2*distanceToLighter2);
	//float normalToLighter0 = distanceToLighter0 / distanceToLighter;
	//float normalToLighter1 = distanceToLighter1 / distanceToLighter;
	//float normalToLighter2 = distanceToLighter2 / distanceToLighter;
	//float intensity = normalToLighter0 * output.Normal[0] + normalToLighter1 * output.Normal[1] + normalToLighter2 * output.Normal[2];
	////float intensity = normalToLighter2 * output.Normal[2];
	//intensity = max(0, intensity);
	//output.Color *= intensity;

	//output.Pos = mul(output.Pos, Projection);
	output.Normal = mul(output.Normal, Projection);
	output.Color = Color;

	/*if (Normal[0] != 0){
	if (Normal[0] > 0){
	output.Color[0] = Pos[2] - 0.5;
	output.Color[1] = Pos[1];
	output.Color[2] = 0;
	}
	else{
	output.Color[0] = Pos[1];
	output.Color[1] = Pos[2] - 0.5;
	output.Color[2] = 1;
	}
	}

	if (Normal[1] != 0){
	if (Normal[1] > 0){
	output.Color[0] = Pos[0];
	output.Color[1] = Pos[2] - 0.5;
	output.Color[2] = 2;
	}
	else{
	output.Color[0] = Pos[2] - 0.5;
	output.Color[1] = Pos[0];
	output.Color[2] = 3;
	}
	}
	if (Normal[2] != 0){
	if (Normal[2] < 0){
	output.Color[0] = Pos[0];
	output.Color[1] = Pos[1];
	output.Color[2] = 4;
	}
	else{
	output.Color[0] = Pos[1];
	output.Color[1] = Pos[0];
	output.Color[2] = 5;
	}
	}*/
	//output.Color = mul(Pos, Projection);
	output.Color = Color;
	return output;

}

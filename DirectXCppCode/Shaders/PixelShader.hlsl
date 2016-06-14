//--------------------------------------------------------------------------------------
struct VS_OUTPUT
{
	float4 Pos : SV_POSITION;
	float4 Normal: NORMAL;
	float4 Color : COLOR0;
};

//Texture2D  gTexture : register(t0);
Texture2D  gTexture : register(t0);
Texture2D  gTexture1 : register(t1);
Texture2D  gTexture2 : register(t2);
Texture2D  gTexture3 : register(t3);
Texture2D  gTexture4 : register(t4);
Texture2D  gTexture5 : register(t5);

SamplerState TextureSampler
{
	Filter = MIN_MAG_MIP_LINEAR;
};
//--------------------------------------------------------------------------------------
// Pixel Shader
//--------------------------------------------------------------------------------------
float4 main(VS_OUTPUT input) : SV_Target
{
	VS_OUTPUT output = (VS_OUTPUT)0;
	
	float2 texturecoor;
	texturecoor[0] =  (0.35 + input.Color[0])/0.7;
	texturecoor[1] =  (0.35 - input.Color[1])/0.7;

	switch (input.Color[2]){
		case 0: return gTexture.Sample(TextureSampler, texturecoor);
		case 1: return gTexture1.Sample(TextureSampler, texturecoor);
		case 2: return gTexture2.Sample(TextureSampler, texturecoor);
		case 3: return gTexture3.Sample(TextureSampler, texturecoor);
		case 4: return gTexture4.Sample(TextureSampler, texturecoor);
		case 5: return gTexture5.Sample(TextureSampler, texturecoor);
	}
	return gTexture.Sample(TextureSampler, texturecoor);
	//return input.Color;
}
/*
float4 main(VS_OUTPUT input) : SV_Target
{
	//float intens=mul(input.Normal,float4(0,-1,0,0));
	float2 texturecoor;
	texturecoor[0] = input.Color[0];
	texturecoor[1] = 1 - input.Color[1];
	return gTexture.Sample(TextureSampler, texturecoor);
	//    return input.color;
}*/
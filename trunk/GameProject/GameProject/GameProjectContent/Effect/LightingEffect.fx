float4x4 xWorld;
float4x4 xView;
float4x4 xProjection;
float4x4 WorldInverseTranspose; //Matrix.Transpose(Matrix.Invert(world));
//Model: world = Bone.Transform * world

float4 AmbientColor = float4(1, 1, 1, 1);
float AmbientIntensity = 0.1;

float3 DiffuseLightDirection = float3(1, -1, 1);
float4 DiffuseColor = float4(1, 1, 1, 1);
float DiffuseIntensity = 1.0;

float Shininess = 200;
float4 SpecularColor = float4(1, 1, 1, 1);
float SpecularIntensity = 1;
float3 ViewVector = float3(1, 0, 0);

Texture xTexture;
sampler TextureSampler = sampler_state { texture = <xTexture>;
 magfilter = LINEAR; minfilter = LINEAR; mipfilter=LINEAR;
  AddressU = mirror; AddressV = mirror;};


struct VertexShaderInput
{
    float4 Position : POSITION0;    
    float4 Normal : NORMAL0;
	float2 TexCoords: TEXCOORD0;
};

struct VertexShaderOutput
{
    float4 Position : POSITION0;
    float4 Color : COLOR0;
	float2 TexCoords: TEXCOORD1;
	float4 Normal : TEXCOORD0;
};

VertexShaderOutput VertexShaderFunction(VertexShaderInput input)
{
    VertexShaderOutput output;

    float4 worldPosition = mul(input.Position, xWorld);
    float4 viewPosition = mul(worldPosition, xView);
    output.Position = mul(viewPosition, xProjection);	

    float4 normal = normalize(mul(input.Normal, WorldInverseTranspose));
    float lightIntensity = dot(normal, DiffuseLightDirection);	
    output.Color = saturate(DiffuseColor * DiffuseIntensity * lightIntensity);
	
	output.Normal = normal;	
	output.TexCoords = input.TexCoords;
    return output;
}

float4 PixelShaderFunction(VertexShaderOutput input) : COLOR0
{
    float3 light = normalize(DiffuseLightDirection);
    float3 normal = normalize(input.Normal);
    float3 r = normalize(2 * dot(light, normal) * normal - light);
    float3 v = normalize(mul(normalize(ViewVector), xWorld));
    float dotProduct = dot(r, v);
 
    float4 specular = SpecularIntensity * SpecularColor 
	* max(pow(dotProduct, Shininess), 0) * length(input.Color);
 
    float4 textureColor = tex2D(TextureSampler, input.TexCoords);
    textureColor.a = 1;
	 
    return saturate(textureColor * (input.Color) + AmbientColor 
	* AmbientIntensity + specular);
}

technique Lighting
{
    pass Pass1
    {
        VertexShader = compile vs_3_0 VertexShaderFunction();
        PixelShader = compile ps_3_0 PixelShaderFunction();
    }
}
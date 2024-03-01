void GetLighting_float(float3 WorldPos,
    out float3 Direction,
    out float3 Color,
    out float Attenuation)
{
    #ifdef SHADERGRAPH_PREVIEW
        Direction = half3(0.5, 0.5, 0);
        Color = 1;
        Attenuation = 1;
    #else

        float4 sCoord = TransformWorldToShadowCoord(WorldPos); //ShadowCoordinates

        Light mainLight = GetMainLight(sCoord);
        Direction = mainLight.direction;
        Color = mainLight.color;
        Attenuation = mainLight.shadowAttenuation;
    #endif
}
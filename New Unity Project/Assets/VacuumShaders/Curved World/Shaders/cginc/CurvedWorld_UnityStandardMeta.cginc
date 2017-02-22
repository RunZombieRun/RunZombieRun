#ifndef VACUUM_CURVEDWORLD_UNITY_STANDARD_META_INCLUDED
#define VACUUM_CURVEDWORLD_UNITY_STANDARD_META_INCLUDED

// Functionality for Standard shader "meta" pass
// (extracts albedo/emission for lightmapper etc.)

// define meta pass before including other files; they have conditions
// on that in some places
#define UNITY_PASS_META 1

#include "UnityCG.cginc"
#include "../cginc/CurvedWorld_UnityStandardInput.cginc" 
#include "UnityMetaPass.cginc"
#include "../cginc/CurvedWorld_UnityStandardCore.cginc"

struct v2f_meta
{
	float4 uv		: TEXCOORD0;
	float4 pos		: SV_POSITION;
};

v2f_meta vert_meta (VertexInput v)
{
	v2f_meta o;

	V_CW_TransformPointAndNormal(v.vertex, v.normal, v.tangent);

	o.pos = UnityMetaVertexPosition(v.vertex, v.uv1.xy, v.uv2.xy, unity_LightmapST, unity_DynamicLightmapST);
	o.uv = TexCoords(v);
	return o;
}

// Albedo for lightmapping should basically be diffuse color.
// But rough metals (black diffuse) still scatter quite a lot of light around, so
// we want to take some of that into account too.
half3 UnityLightmappingAlbedo (half3 diffuse, half3 specular, half smoothness)
{
	half roughness = SmoothnessToRoughness(smoothness);
	half3 res = diffuse;
	res += specular * roughness * 0.5;
	return res;
}

float4 frag_meta (v2f_meta i) : SV_Target
{
	// we're interested in diffuse & specular colors,
	// and surface roughness to produce final albedo.
	FragmentCommonData data = UNITY_SETUP_BRDF_INPUT (i.uv);

	UnityMetaInput o;
	UNITY_INITIALIZE_OUTPUT(UnityMetaInput, o);

	o.Albedo = UnityLightmappingAlbedo (data.diffColor, data.specColor, data.smoothness);
	o.Emission = Emission(i.uv.xy);

	return UnityMetaFragment(o);
}

#endif // UNITY_STANDARD_META_INCLUDED
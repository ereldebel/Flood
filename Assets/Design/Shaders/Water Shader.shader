// The path to the shader in the dropdown.
Shader "Unlit/Water Shader"
{
    // Input Data
    Properties
    {
        // The type here affects how the property is displayed in the Inspector.
        // Name ("Display Name", type) = Default Value
        _Color ("Color", Color) = (1, 1, 1, 1) // Opaque white
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }

    SubShader
    {
        // Sub-shader tags. These are different from pass tags.
        Tags
        {
            "RenderType"="Opaque"
        }

        Pass
        {
            CGPROGRAM
            // Assign the functions for the vertex and fragment shaders.
            #pragma vertex vert
            #pragma fragment frag

            // Include Unity specific things.
            #include "UnityCG.cginc"
            // Includes _LightColor0
            #include "UnityLightingCommon.cginc"

            // Variables. Reference the properties from above.
            float4 _Color;

            // Per vertex mesh data that will be automatically passed to the
            // vertex shader by Unity.
            // There are a limited number of channels (AKA Semantics) you can
            // use here.
            struct MeshData
            {
                // type name : SEMANTIC
                // Object space vertex position.
                float4 vertex : POSITION;
                // Object space normal direction.
                float3 normal : NORMAL;
                // Tangent vector is used to define the Tangent-Space. It is a
                // per-triangle coordinate system that is used to unpack data
                // that describes the surface of the 3D object instead of the
                // object itself. For example, a normal map describes the surface.
                // It makes sense to make it independent of Object-Space.
                float4 tangent : TANGENT;
                float4 color : COLOR;
                // Usually texture mapping. Can be float4.
                float2 uv : TEXCOORD0;
                // Can hold whatever data you set in the mesh. Can also be float4.
                float4 uv1 : TEXCOORD1;
            };

            // Data that will be returned from the vertex shader and interpolated
            // when passed to the fragment shader.
            struct v2f
            {
                // Clip space position.
                float4 vertex : SV_POSITION;
                // TEXCOORD# can be whatever data you need here. Can also be float4.
                float3 normal : TEXCOORD0;
                float2 uv : TEXCOORD1;
                float3 worldPos : TEXCOORD3;
            };

            v2f vert(MeshData i)
            {
                v2f o;
                // Converts local space (object space) to "screen space" (clip space).
                // Without this line, the shape will be rendered directly to the
                // screen regardless of the object's Transform. Try it!
                o.vertex = UnityObjectToClipPos(i.vertex) + float4(0, cos(_Time.x) / 8, 0, 0);

                // World space coordinates.
                o.worldPos = mul(unity_ObjectToWorld, i.vertex);

                // Pass the object space normal as is.
                //o.normal = i.normal;

                // Convert the normal to world space.
                //o.normal = normalize(mul((float3x3)unity_ObjectToWorld, i.normal));
                o.normal = UnityObjectToWorldNormal(i.normal);

                o.uv = i.uv;

                return o;
            }

            // bool - 0 or 1
            // int
            // float (32 bit float) - use that everywhere until you need to optimize
            // half (16 bit float) - good enough for most cases
            // fixed (lower precision) - only usable in the range -1 to 1
            // float4x4 - 4x4 matrix
            // float4.xyzw === float4.rgba
            // float4[1] === float4.y === float4.g

            // SV_Target - output to frame buffer (screen).
            // return_type functionName(InterpolatedDataType paramName) : OUTPUT_TARGET
            float4 frag(v2f i) : SV_Target
            {
                float3 N = i.normal;

                // Direction to light in case of a directional light.
                float3 L = _WorldSpaceLightPos0.xyz;
                // Lambertian lighting.
                float3 diffuseLight = saturate(dot(N, L)) * _LightColor0.xyz;
                return float4(diffuseLight, 1);

                // _Time is super useful. _Time = float4(t/20, t, t*2, t*3)
                //return cos(_Time.y) * 0.5 + 0.5;

                // View vector. From fragment to the camera.
                // float3 V = normalize(_WorldSpaceCameraPos - i.worldPos);
                // Fresnel effect.
                // float fresnel = 1 - dot(V, N);
                // fresnel *= fresnel;
                // float4 result = fresnel.x * _Time;
                // return frac(float4(result.xyz, 1));

                // Vectors and colors are interchangeable.
                // return _Color;
            }

            // Useful functions:
            // normalize(v)
            // length(v)
            // distance(p0, p1)
            // saturate(v) - clamp01
            // lerp(a, b, t)
            // reflect(incoming, N) - reflect incoming vector around the normal N
            // dot(v, u) - dot product
            // pow(v, exp)
            // exp2(v) - 2^v
            ENDCG
        }
    }
}
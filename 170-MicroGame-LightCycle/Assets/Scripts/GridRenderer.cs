using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public struct CellInstance {
    public Matrix4x4 trs;     // position + rotation + scale
    public Vector4   color;   // RGBA
    public Vector2   uvOffset;
}

public class GridRenderer : MonoBehaviour {
    public int width  = 20;
    public int height = 20;
    public float cellSize = 1f;

    Mesh quad;
    Material mat;
    MaterialPropertyBlock mpb;
    RenderParams rp;

    Matrix4x4[] matrices;
    Vector4[]   colors;
    Vector2[]   uvs;
    int count;

    void Start() {
        quad = MakeQuad();

        // URP/Lit works, but for a neon look use URP/Unlit with emission
        var shader = Shader.Find("Universal Render Pipeline/Unlit");
        mat = new Material(shader);

        // The instance properties we want to use MUST be declared in the shader
        // (we'll set this up in step 3). For now, assume _BaseColor exists.
        mat.enableInstancing = true;

        matrices = new Matrix4x4[width * height];
        colors   = new Vector4  [width * height];
        uvs      = new Vector2  [width * height];
        mpb      = new MaterialPropertyBlock();

        BuildGrid();

        rp = new RenderParams(mat) {
            worldBounds = new Bounds(transform.position, Vector3.one * 1000f),
            shadowCastingMode = ShadowCastingMode.Off,
            receiveShadows = false,
            layer = gameObject.layer,
            camera = null // null = all cameras
        };
    }

    void BuildGrid() {
        count = 0;
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                var pos = new Vector3(x * cellSize, 0, z * cellSize);
                matrices[count] = Matrix4x4.TRS(pos, Quaternion.identity, Vector3.one * (cellSize * 0.95f));
                colors[count]   = new Vector4(0f, 0.8f, 1f, 1f); // cyan safe
                uvs[count]      = Vector2.zero;
                count++;
            }
        }
    }

    void Update() {
        // Push per-instance data
        mpb.SetVectorArray("_BaseColor",   colors);
        mpb.SetVectorArray("_UVOffset",    uvs);

        // One call draws all 400 cells
        Graphics.RenderMeshInstanced(rp, quad, 0, matrices, count);
    }

    Mesh MakeQuad() {
        var m = new Mesh();
        m.vertices = new[] {
            new Vector3(-0.5f, 0, -0.5f),
            new Vector3( 0.5f, 0, -0.5f),
            new Vector3( 0.5f, 0,  0.5f),
            new Vector3(-0.5f, 0,  0.5f),
        };
        m.uv = new[] {
            new Vector2(0,0), new Vector2(1,0),
            new Vector2(1,1), new Vector2(0,1)
        };
        m.triangles = new[] { 0,2,1, 0,3,2 };
        m.RecalculateNormals();
        return m;
    }

    // Call this from your game logic when a cell's state changes
    public void SetCellColor(int x, int z, Color c) {
        int i = x * height + z;
        if (i < 0 || i >= count) return;
        colors[i] = c;
    }
}
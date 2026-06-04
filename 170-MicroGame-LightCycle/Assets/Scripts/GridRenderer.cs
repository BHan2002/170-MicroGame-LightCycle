using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public struct CellInstance {
    public Matrix4x4 trs;     // position + rotation + scale
    public Vector4   color;   // RGBA
    public Vector2   uvOffset;
}

public class GridRenderer : MonoBehaviour {
    public int width  = 10;
    public int height = 10;
    public float cellSize = 1f;

    Mesh quad;
    Material mat;
    MaterialPropertyBlock mpb;
    RenderParams rp;

    Matrix4x4[] matrices;
    Vector4[]   colors;
    Vector4[]   uvs;
    int count;

    CellState[] states;
    Vector3[] positions;

    void Awake() {
        quad = MakeQuad();

        var shader = Shader.Find("Custom/InstancedUnlit");
        if (shader == null)
        {
            Debug.LogError("Could not find Custom/InstancedUnlit shader!");
            return;
        }

        mat = new Material(shader);
        mat.enableInstancing = true;

        matrices = new Matrix4x4[width * height];
        colors   = new Vector4  [width * height];
        uvs      = new Vector4  [width * height];
        mpb      = new MaterialPropertyBlock();
        states = new CellState[width * height];
        positions = new Vector3[width * height];

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
                positions[count] = pos;
                states[count] = CellState.Safe;
                
                matrices[count] = Matrix4x4.TRS(
                    pos,
                    Quaternion.identity,
                    Vector3.one * (cellSize * 0.95f)
                );
                
                colors[count] = GetColorForState(CellState.Safe, 0f);
                uvs[count]      = Vector4.zero;
                count++;
            }
        }
    }

    void Update()
    {
        UpdateCellColors();

        mpb.SetVectorArray("_BaseColor", colors);
        mpb.SetVectorArray("_UVOffset", uvs);

        rp.matProps = mpb;

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
   public void SetCellState(int x, int z, CellState state)
    {
        if (x < 0 || x >= width || z < 0 || z >= height) return;

        int i = x * height + z;

        states[i] = state;

        Vector3 scale =
            state == CellState.Gone
            ? Vector3.zero
            : Vector3.one * (cellSize * 0.95f);

        matrices[i] = Matrix4x4.TRS(
            positions[i],
            Quaternion.identity,
            scale
        );
    }

    void UpdateCellColors()
    {
        float flash = Mathf.PingPong(Time.time * 8f, 1f);

        for (int i = 0; i < count; i++)
        {
            colors[i] = GetColorForState(states[i], flash);
        }
    }

    Vector4 GetColorForState(CellState state, float flash)
    {
        switch (state)
        {
            case CellState.Safe:
                return new Vector4(0.00f, 0.00f, 0.00f, 1f); // dark grid

            case CellState.Warning:
                return new Vector4(1f, 0f, 0f, 1f); // red

            case CellState.Falling:
                return Vector4.Lerp(
                    new Vector4(1f, 0f, 0f, 1f),
                    new Vector4(1f, 1f, 1f, 1f),
                    flash
                ); // flashing

            case CellState.Gone:
                return Vector4.zero; // invisible, if shader supports alpha

            case CellState.Trail:
                return new Vector4(0f, 1f, 0.8f, 1f);

            default:
                return Vector4.one;
        }
    }
}
using UnityEngine;
public enum PipeState { Empty, Oil, Fuel, Broken }
public enum PipeOrientation { Vertical, CornerNE, T, Cross }

public class PipelineRenderer : MonoBehaviour
{
    [Header("Vide")]
    public Sprite emptyVertical;
    public Sprite emptyCornerNE;
    public Sprite emptyT;
    public Sprite emptyCross;

    [Header("Pétrole")]
    public Sprite oilVertical;
    public Sprite oilCornerNE;
    public Sprite oilT;
    public Sprite oilCross;

    [Header("Carburant")]
    public Sprite fuelVertical;
    public Sprite fuelCornerNE;
    public Sprite fuelT;
    public Sprite fuelCross;

    [Header("Cassé")]
    public Sprite brokenVertical;
    public Sprite brokenCornerNE;
    public Sprite brokenT;
    public Sprite brokenCross;

    SpriteRenderer sr;
    PipeState state = PipeState.Empty;
    PipeOrientation orientation = PipeOrientation.Vertical;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void SetOrientation(PipeOrientation o)
    {
        orientation = o;
        Refresh();
    }

    public void SetState(PipeState s)
    {
        state = s;
        Refresh();
    }

    void Refresh()
    {
        sr.sprite = (state, orientation) switch
        {
            (PipeState.Empty, PipeOrientation.Vertical) => emptyVertical,
            (PipeState.Empty, PipeOrientation.CornerNE) => emptyCornerNE,
            (PipeState.Empty, PipeOrientation.T) => emptyT,
            (PipeState.Empty, PipeOrientation.Cross) => emptyCross,

            (PipeState.Oil, PipeOrientation.Vertical) => oilVertical,
            (PipeState.Oil, PipeOrientation.CornerNE) => oilCornerNE,
            (PipeState.Oil, PipeOrientation.T) => oilT,
            (PipeState.Oil, PipeOrientation.Cross) => oilCross,

            (PipeState.Fuel, PipeOrientation.Vertical) => fuelVertical,
            (PipeState.Fuel, PipeOrientation.CornerNE) => fuelCornerNE,
            (PipeState.Fuel, PipeOrientation.T) => fuelT,
            (PipeState.Fuel, PipeOrientation.Cross) => fuelCross,

            (PipeState.Broken, PipeOrientation.Vertical) => brokenVertical,
            (PipeState.Broken, PipeOrientation.CornerNE) => brokenCornerNE,
            (PipeState.Broken, PipeOrientation.T) => brokenT,
            (PipeState.Broken, PipeOrientation.Cross) => brokenCross,

            _ => emptyVertical
        };
    }

    public PipeState GetState() => state;
    public PipeOrientation GetOrientation() => orientation;

}

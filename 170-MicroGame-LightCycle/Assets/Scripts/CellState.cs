public enum CellState
{
    Safe,       // light grey/dark, player can move here
    Warning,    // yellow, about to fall
    Falling,    // bright red, shaking/flashing
    Gone,       // destroyed, player cannot move here
    Trail       // tron light blue
}
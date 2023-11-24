namespace Agent.UI.Models.Components.Toasts;

public class ToastPlacement
{
    public string[] Placemnt { get; }

    private ToastPlacement(params string[] placemnt)
    {
        Placemnt = placemnt;
    }

    public override string ToString()
        => string.Join(" ", Placemnt);

    public static ToastPlacement TopLeft = new("top-0", "start-0");
    public static ToastPlacement TopCenter = new("top-0", "start-50", "translate-middle-x");
    public static ToastPlacement TopRight = new("top-0", "end-0");
    public static ToastPlacement MiddleLeft = new("top-50", "start-0", "translate-middle-y");
    public static ToastPlacement MiddleCenter = new("top-50", "start-50", "translate-middle");
    public static ToastPlacement MiddleRight = new("top-50", "end-0", "translate-middle-y");
    public static ToastPlacement BottomLeft = new("bottom-0", "start-0");
    public static ToastPlacement BottomCenter = new("bottom-0", "start-50", "translate-middle-x");
    public static ToastPlacement BottomRight = new("bottom-0", "end-0");
}
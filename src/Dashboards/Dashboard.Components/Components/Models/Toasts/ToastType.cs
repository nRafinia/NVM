namespace Agent.UI.Models.Components.Toasts;

public class ToastType
{
    public string ShowingType { get; }

    private ToastType(string type)
    {
        ShowingType = type;
    }

    public static ToastType Primary = new ("bg-primary");
    public static ToastType Secondary = new ("bg-secondary");
    public static ToastType Success = new ("bg-success");
    public static ToastType Danger = new ("bg-danger");
    public static ToastType Warning = new ("bg-warning");
    public static ToastType Info = new ("bg-info");
    public static ToastType Dark = new ("bg-dark");
}
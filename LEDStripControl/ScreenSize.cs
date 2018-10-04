public struct ScreenSize
{
    public int Height { get; set; }
    public int Width { get; set; }

    public ScreenSize(int width, int height)
    {
        Height = height;
        Width = width;
    }
}
using System.Drawing;

public sealed class AreaForLED
{
    public Rectangle Rectangle { get; private set; }

    public byte R { get; private set; }
    public byte G { get; private set; }
    public byte B { get; private set; }

    public AreaForLED(int x_pos, int y_pos, int width, int height)
    {
        Rectangle = new Rectangle(x_pos, y_pos, width, height);
    }

    public void SetRGB(byte r, byte g, byte b)
    {
        R = r;
        G = g;
        B = b;
    }
}
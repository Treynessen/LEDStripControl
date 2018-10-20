public struct DataForExport
{
    public byte[] StandartRGB { get; private set; }
    public int NumVerticalLeds { get; private set; }
    public int NumHorizontalLeds { get; private set; }
    public bool HaveCornerLeds { get; private set; }
    public Modes CurrentMode { get; private set; }

    public DataForExport(byte[] standart_rgb, int num_vertical_leds, int num_horizontal_leds,
        bool have_corner_leds, Modes current_mode)
    {
        StandartRGB = new byte[3];
        StandartRGB[0] = standart_rgb[0];
        StandartRGB[1] = standart_rgb[1];
        StandartRGB[2] = standart_rgb[2];
        NumVerticalLeds = num_vertical_leds;
        NumHorizontalLeds = num_horizontal_leds;
        HaveCornerLeds = have_corner_leds;
        CurrentMode = current_mode;
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using SharpDX;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using Device = SharpDX.Direct3D11.Device;
using MapFlags = SharpDX.Direct3D11.MapFlags;

public sealed class ScreenCapture : IDisposable
{
    private Device device;
    private OutputDescription outputDescription;
    private OutputDuplication outputDuplication;
    private Texture2D desktopImageTexture;

    private Bitmap image;
    private Rectangle boundsRect;

    private int width, height;

    public int Width => width;
    public int Height => height;

    public ScreenCapture(int numAdapter, int numOutput)
    {
        var factory = new Factory1();
        Adapter1 adapter = new Factory1().GetAdapter1(numAdapter);
        device = new Device(adapter);
        Output output = adapter.GetOutput(numOutput);
        var output1 = output.QueryInterface<Output1>();
        outputDescription = output.Description;
        outputDuplication = output1.DuplicateOutput(device);

        width = outputDescription.DesktopBounds.GetWidth();
        height = outputDescription.DesktopBounds.GetHeight();

        desktopImageTexture = new Texture2D(device, new Texture2DDescription()
        {
            CpuAccessFlags = CpuAccessFlags.Read,
            BindFlags = BindFlags.None,
            Format = Format.B8G8R8A8_UNorm,
            Width = width,
            Height = height,
            OptionFlags = ResourceOptionFlags.None,
            MipLevels = 1,
            ArraySize = 1,
            SampleDescription = { Count = 1, Quality = 0 },
            Usage = ResourceUsage.Staging
        });

        image = new Bitmap(width, height, PixelFormat.Format32bppRgb);
        boundsRect = new Rectangle(0, 0, width, height);
    }

    /* Код взят с: 
    https://github.com/sharpdx/SharpDX-Samples/blob/master/Desktop/Direct3D11.1/ScreenCapture/Program.cs
    https://github.com/fabsenet/adrilight/blob/master/adrilight/DesktopDuplication/DesktopDuplicator.cs метод ProcessFrame */
    public Bitmap GetFrame()
    {
        SharpDX.DXGI.Resource desktopResource = null;
        var frameInfo = new OutputDuplicateFrameInformation();

        // Try to get duplicated frame within given time
        try
        {
            outputDuplication.AcquireNextFrame(500, out frameInfo, out desktopResource);
        }
        catch (SharpDXException ex)
        {
            if (ex.ResultCode.Code == SharpDX.DXGI.ResultCode.WaitTimeout.Result.Code)
            {
                return null;
            }
            MessageBox.Show("Невозможно обработать SharpDXException", "Ошибка");
            return null;
        }

        // copy resource into memory that can be accessed by the CPU
        using (var tempTexture = desktopResource.QueryInterface<Texture2D>())
        {
            device.ImmediateContext.CopyResource(tempTexture, desktopImageTexture);
        }
        desktopResource.Dispose();

        // Get the desktop capture texture
        var mapSource = device.ImmediateContext.MapSubresource(desktopImageTexture, 0, MapMode.Read, MapFlags.None);

        // Copy pixels from screen capture Texture to GDI bitmap
        BitmapData mapDest = image.LockBits(boundsRect, ImageLockMode.WriteOnly, image.PixelFormat);
        var sourcePtr = mapSource.DataPointer;
        var destPtr = mapDest.Scan0;

        if (mapSource.RowPitch == mapDest.Stride)
        {
            // Fast copy
            Utilities.CopyMemory(destPtr, sourcePtr, height * mapDest.Stride);
        }
        else
        {
            // Safe copy
            for (int y = 0; y < height; y++)
            {
                // Copy a single line 
                Utilities.CopyMemory(destPtr, sourcePtr, width * 4);

                // Advance pointers
                sourcePtr = IntPtr.Add(sourcePtr, mapSource.RowPitch);
                destPtr = IntPtr.Add(destPtr, mapDest.Stride);
            }
        }

        // Release source and dest locks
        image.UnlockBits(mapDest);
        device.ImmediateContext.UnmapSubresource(desktopImageTexture, 0);

        desktopResource.Dispose();
        outputDuplication.ReleaseFrame();

        return image;
    }

    public void RefreshScreenResolution()
    {
        width = outputDescription.DesktopBounds.GetWidth();
        height = outputDescription.DesktopBounds.GetHeight();
        desktopImageTexture = new Texture2D(device, new Texture2DDescription()
        {
            CpuAccessFlags = CpuAccessFlags.Read,
            BindFlags = BindFlags.None,
            Format = Format.B8G8R8A8_UNorm,
            Width = width,
            Height = height,
            OptionFlags = ResourceOptionFlags.None,
            MipLevels = 1,
            ArraySize = 1,
            SampleDescription = { Count = 1, Quality = 0 },
            Usage = ResourceUsage.Staging
        });
    }

    public void Dispose()
    {
        image.Dispose();
        device.Dispose();
        desktopImageTexture.Dispose();
    }
}
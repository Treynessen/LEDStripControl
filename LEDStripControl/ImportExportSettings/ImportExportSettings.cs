using System;
using System.IO;
using Pair = System.Collections.Generic.KeyValuePair<int, string>;

public class ImportExportSettings
{
    public byte[] StandartRGB { get; private set; } = new byte[3];
    public int NumVerticalLeds { get; private set; }
    public int NumHorizontalLeds { get; private set; }
    public bool HaveCornerLeds { get; private set; }
    public Modes CurrentMode { get; private set; }

    private string program_folder;
    private string file_with_config = "Config.led";

    public ImportExportSettings()
    {
        program_folder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\LEDControl\\";
        Directory.CreateDirectory(program_folder);
    }
    public bool OpenConfigFile()
    {
        bool success = true;
        string config_file_data = null;
        if (File.Exists(program_folder + file_with_config))
        {
            using (StreamReader stream_reader = new StreamReader(program_folder + file_with_config))
            {
                try
                {
                    config_file_data = stream_reader.ReadToEnd();
                }
                catch
                {
                    success = false;
                }
            }
        }
        else success = false;
        if (success && config_file_data != null)
        {
            Pair data_from_string = new Pair();
            try
            {
                data_from_string = DataParsing(config_file_data, "StaticModeRGB: \"R", "G", 0);
                StandartRGB[0] = Convert.ToByte(data_from_string.Value);
                data_from_string = DataParsing(config_file_data, "G", "B", data_from_string.Key);
                StandartRGB[1] = Convert.ToByte(data_from_string.Value);
                data_from_string = DataParsing(config_file_data, "B", "\"", data_from_string.Key);
                StandartRGB[2] = Convert.ToByte(data_from_string.Value);
                data_from_string = DataParsing(config_file_data, "NumVerticalLeds: \"", "\"", data_from_string.Key);
                NumVerticalLeds = Convert.ToByte(data_from_string.Value);
                data_from_string = DataParsing(config_file_data, "NumHorizontalLeds: \"", "\"", data_from_string.Key);
                NumHorizontalLeds = Convert.ToByte(data_from_string.Value);
                data_from_string = DataParsing(config_file_data, "HaveCornerLeds: \"", "\"", data_from_string.Key);
                switch (data_from_string.Value)
                {
                    case "YES":
                        HaveCornerLeds = true;
                        break;
                    case "NO":
                        HaveCornerLeds = false;
                        break;
                    default:
                        throw new FormatException();
                }
                data_from_string = DataParsing(config_file_data, "CurrentMode: \"", "\"", data_from_string.Key);
                if (Enum.TryParse(data_from_string.Value, out Modes mode)) CurrentMode = mode;
                else throw new FormatException();
            }
            catch (NotImplementedException)
            {
                success = false;
            }
            catch (FormatException)
            {
                success = false;
            }
            catch (OverflowException)
            {
                success = false;
            }
        }
        return success;
    }

    public bool SaveConfigToFile(byte[] standart_rgb, int num_vertical_leds, int num_horizontal_leds, bool have_corner_leds, Modes current_mode)
    {
        StandartRGB = standart_rgb;
        NumVerticalLeds = num_vertical_leds;
        NumHorizontalLeds = num_horizontal_leds;
        HaveCornerLeds = have_corner_leds;
        CurrentMode = current_mode;

        bool success = true;
        using (FileStream file = new FileStream(program_folder + file_with_config, FileMode.OpenOrCreate))
        {
            StreamWriter stream_writter = null;
            try
            {
                file.SetLength(0);
                stream_writter = new StreamWriter(file);
                stream_writter.Write(CreateStringData());
            }
            catch
            {
                success = false;
            }
            finally
            {
                if (stream_writter != null) stream_writter.Close();
            }
        }
        return success;
    }

    private String CreateStringData()
    {
        string config = "";
        config += $"StaticModeRGB: \"R{StandartRGB[0].ToString()}G{StandartRGB[1].ToString()}B{StandartRGB[2].ToString()}\"\n";
        config += $"NumVerticalLeds: \"{NumVerticalLeds.ToString()}\"\n";
        config += $"NumHorizontalLeds: \"{NumHorizontalLeds.ToString()}\"\n";
        config += "HaveCornerLeds: \"" + (HaveCornerLeds ? "YES" : "NO") + "\"\n";
        config += $"CurrentMode: \"{CurrentMode.ToString()}\"";
        return config;
    }

    private Pair DataParsing(string config_file_data, string left_data, string right_data, int start_index)
    {
        int index_begin, index_end;
        index_begin = config_file_data.IndexOf(left_data, start_index);
        if (index_begin == -1) throw new NotImplementedException();
        index_begin += left_data.Length;
        index_end = config_file_data.IndexOf(right_data, index_begin);
        if (index_end == -1) throw new NotImplementedException();
        return new Pair(index_end, config_file_data.Substring(index_begin, index_end - index_begin));
    }
}
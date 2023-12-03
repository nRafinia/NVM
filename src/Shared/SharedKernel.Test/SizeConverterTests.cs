namespace SharedKernel.Test;

public class SizeConverterTests
{
    [Fact]
    public void ConvertBytesToString_ReturnsCorrectString_ForPositiveBytes()
    {
        var result = SizeConverter.ConvertBytesToString(1500);
        Assert.Equal("1.5 KB", result);
    }

    [Fact]
    public void ConvertBytesToString_ReturnsCorrectString_ForNegativeBytes()
    {
        var result = SizeConverter.ConvertBytesToString(-1500);
        Assert.Equal("-1.5 KB", result);
    }

    [Fact]
    public void ConvertBytesToString_ReturnsCorrectString_ForZeroBytes()
    {
        var result = SizeConverter.ConvertBytesToString(0);
        Assert.Equal("0.0 bytes", result);
    }

    [Fact]
    public void ConvertStringToBytes_ReturnsCorrectBytes_ForValidString()
    {
        var result = SizeConverter.ConvertStringToBytes("1.5KB");
        Assert.Equal(1536, result);
    }

    [Fact]
    public void ConvertStringToBytes_ReturnsNegativeOne_ForInvalidString()
    {
        var result = SizeConverter.ConvertStringToBytes("invalid string");
        Assert.Equal(-1, result);
    }

}
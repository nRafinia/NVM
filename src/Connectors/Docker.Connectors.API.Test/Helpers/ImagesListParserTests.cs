using Docker.Connectors.API.Helpers;
using Docker.DotNet.Models;

namespace Docker.Connectors.API.Test.Helpers;

public class ImagesListParserTests
{
    private const string RawResult =
        "[{\"Containers\":-1,\"Created\":2023/12/01T1:35:00 PM,\"Id\":\"sha256:2552cbe87d6db5ffe84a9d2053b1728905be28a82b0effd114bdc1897eaa362b\",\"Labels\":{\"maintainer\":\"Emby LLC <apps@emby.media>\"},\"ParentId\":\"\",\"RepoDigests\":[\"emby/embyserver@sha256:fe2044bd3cd3b22dd38c77c1d05c50588520bc6b4083a36e8062dc08e923599f\"],\"RepoTags\":[\"emby/embyserver:latest\"],\"SharedSize\":-1,\"Size\":571411119,\"VirtualSize\":571411119}]";

    [Fact]
    public void Parse_Return_Success()
    {
        //arrange
        var jsonData = new List<ImagesListResponse>()
        {
            new ()
            {
                Containers = -1,
                Created = DateTime.Now,
                ID ="sha256:2552cbe87d6db5ffe84a9d2053b1728905be28a82b0effd114bdc1897eaa362b",
                Labels = new Dictionary<string, string>()
                {
                    {"maintainer","Emby LLC <apps@emby.media>"}
                },
                ParentID = string.Empty,
                RepoDigests = new List<string>()
                {
                    "emby/embyserver@sha256:fe2044bd3cd3b22dd38c77c1d05c50588520bc6b4083a36e8062dc08e923599f"
                },
                RepoTags = new List<string>()
                {
                    "emby/embyserver:latest"
                },
                SharedSize = -1,
                Size = 571411119,
                VirtualSize = 571411119
            }
        };

        //act
        var convertor = ImagesParser.List(jsonData!);

        //assert
        Assert.NotEmpty(convertor);
        Assert.Equal("emby/embyserver", convertor[0].Repository);
        Assert.Equal(DateTime.Now.Day, convertor[0].CreatedAt.Day);
        Assert.Equal(571411119, convertor[0].Size);
        Assert.Equal(-1, convertor[0].SharedSize);
    }    
    
    [Fact]
    public void Parse_Return_Empty()
    {
        //arrange
        var jsonDataEmpty =new List<ImagesListResponse>(0);


        //act
        var convertor = ImagesParser.List(jsonDataEmpty);

        //assert
        Assert.Empty(convertor);
    }
}
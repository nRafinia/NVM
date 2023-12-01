using System.Text.Json;
using Docker.Connectors.API.Helpers;
using Docker.Connectors.API.Models;

namespace Docker.Connectors.API.Test.Helpers;

public class ContainersListParserTests
{
    private const string RawResultMultiple =
        "[{\"Command\":\"docker-entrypoint.sh redis-server\",\"Created\":1701416108,\"HostConfig\":{\"NetworkMode\":\"redis_default\"},\"Id\":\"5cd808b82e0e732f53a44e7d3b92e68e5dd502cba10540ea6ff49f7d7a5c000b\",\"Image\":\"redis:latest\",\"ImageID\":\"sha256:961dda256baa7a35297d34cca06bc2bce8397b0ef8b68d8064c30e338afc5a7d\",\"Labels\":{\"com.docker.compose.config-hash\":\"d63b91d8bd0a08703bad6840822c844a44a01110bf6ea70a8f73f211f63b3242\",\"com.docker.compose.container-number\":\"1\",\"com.docker.compose.depends_on\":\"\",\"com.docker.compose.image\":\"sha256:961dda256baa7a35297d34cca06bc2bce8397b0ef8b68d8064c30e338afc5a7d\",\"com.docker.compose.oneoff\":\"False\",\"com.docker.compose.project\":\"redis\",\"com.docker.compose.project.config_files\":\"/home/naser/dockers/redis/docker-compose.yml\",\"com.docker.compose.project.working_dir\":\"/home/naser/dockers/redis\",\"com.docker.compose.replace\":\"08d2097466a7664b45319772636969b392221ab55ae767109f1ccb34a4525000\",\"com.docker.compose.service\":\"redis\",\"com.docker.compose.version\":\"2.21.0\"},\"Mounts\":[{\"Destination\":\"/data\",\"Mode\":\"rw\",\"Propagation\":\"rprivate\",\"RW\":true,\"Source\":\"/home/naser/dockers/redis/data\",\"Type\":\"bind\"}],\"Names\":[\"/redis\"],\"NetworkSettings\":{\"Networks\":{\"redis_default\":{\"Aliases\":null,\"DriverOpts\":null,\"EndpointID\":\"0a317e44ad75f8b622459850fb64cd3d32209f12c5a4059962c29a5e5529605c\",\"Gateway\":\"172.18.0.1\",\"GlobalIPv6Address\":\"\",\"GlobalIPv6PrefixLen\":0,\"IPAddress\":\"172.18.0.2\",\"IPAMConfig\":null,\"IPPrefixLen\":16,\"IPv6Gateway\":\"\",\"Links\":null,\"MacAddress\":\"02:42:ac:12:00:02\",\"NetworkID\":\"3e1687d10884c55152aa0cc27e623d5acb1cc971b666486d8809d4b98e189fe2\"}}},\"Ports\":[{\"IP\":\"0.0.0.0\",\"PrivatePort\":6379,\"PublicPort\":6379,\"Type\":\"tcp\"},{\"IP\":\"::\",\"PrivatePort\":6379,\"PublicPort\":6379,\"Type\":\"tcp\"}],\"State\":\"running\",\"Status\":\"Up 3 seconds\"},{\"Command\":\"/init\",\"Created\":1697136949,\"HostConfig\":{\"NetworkMode\":\"host\"},\"Id\":\"0ebda6ef1109e1e1d1447222508dbf6f545e494aff377e0afafae4294983506e\",\"Image\":\"emby/embyserver:latest\",\"ImageID\":\"sha256:2552cbe87d6db5ffe84a9d2053b1728905be28a82b0effd114bdc1897eaa362b\",\"Labels\":{\"com.docker.compose.config-hash\":\"7bde31b5e030b18f41fb9cdc0c398e9f90650ef70d07e2391a8c24f88bbb161b\",\"com.docker.compose.container-number\":\"1\",\"com.docker.compose.depends_on\":\"\",\"com.docker.compose.image\":\"sha256:2552cbe87d6db5ffe84a9d2053b1728905be28a82b0effd114bdc1897eaa362b\",\"com.docker.compose.oneoff\":\"False\",\"com.docker.compose.project\":\"emby\",\"com.docker.compose.project.config_files\":\"/home/naser/dockers/emby/docker-compose.yml\",\"com.docker.compose.project.working_dir\":\"/home/naser/dockers/emby\",\"com.docker.compose.service\":\"emby\",\"com.docker.compose.version\":\"2.21.0\",\"maintainer\":\"Emby LLC <apps@emby.media>\"},\"Mounts\":[{\"Destination\":\"/media/disk700/picture\",\"Mode\":\"rw\",\"Propagation\":\"rprivate\",\"RW\":true,\"Source\":\"/media/disk700/picture\",\"Type\":\"bind\"},{\"Destination\":\"/config\",\"Mode\":\"rw\",\"Propagation\":\"rprivate\",\"RW\":true,\"Source\":\"/home/naser/dockers/emby/emby\",\"Type\":\"bind\"},{\"Destination\":\"/media/disk700/Movie\",\"Mode\":\"rw\",\"Propagation\":\"rprivate\",\"RW\":true,\"Source\":\"/media/disk700/Movie\",\"Type\":\"bind\"},{\"Destination\":\"/media/disk700/Music\",\"Mode\":\"rw\",\"Propagation\":\"rprivate\",\"RW\":true,\"Source\":\"/media/disk700/Music\",\"Type\":\"bind\"},{\"Destination\":\"/media/disk700/TV Show\",\"Mode\":\"rw\",\"Propagation\":\"rprivate\",\"RW\":true,\"Source\":\"/media/disk700/TV Show\",\"Type\":\"bind\"}],\"Names\":[\"/embyserver\"],\"NetworkSettings\":{\"Networks\":{\"host\":{\"Aliases\":null,\"DriverOpts\":null,\"EndpointID\":\"0ecda90b78cf74b25cf64ac75ae2d83d32efe420a0e87a94731321462a6710f3\",\"Gateway\":\"\",\"GlobalIPv6Address\":\"\",\"GlobalIPv6PrefixLen\":0,\"IPAddress\":\"\",\"IPAMConfig\":null,\"IPPrefixLen\":0,\"IPv6Gateway\":\"\",\"Links\":null,\"MacAddress\":\"\",\"NetworkID\":\"d83269a8ec4711ed8860ee0dec37cb03e0c5c01fec688478c419af7f9ce0d885\"}}},\"Ports\":[],\"State\":\"running\",\"Status\":\"Up 2 days\"}]";
    private const string RawResultSingle =
        "[{\"Command\":\"docker-entrypoint.sh redis-server\",\"Created\":1701416108,\"HostConfig\":{\"NetworkMode\":\"redis_default\"},\"Id\":\"5cd808b82e0e732f53a44e7d3b92e68e5dd502cba10540ea6ff49f7d7a5c000b\",\"Image\":\"redis:latest\",\"ImageID\":\"sha256:961dda256baa7a35297d34cca06bc2bce8397b0ef8b68d8064c30e338afc5a7d\",\"Labels\":{\"com.docker.compose.config-hash\":\"d63b91d8bd0a08703bad6840822c844a44a01110bf6ea70a8f73f211f63b3242\",\"com.docker.compose.container-number\":\"1\",\"com.docker.compose.depends_on\":\"\",\"com.docker.compose.image\":\"sha256:961dda256baa7a35297d34cca06bc2bce8397b0ef8b68d8064c30e338afc5a7d\",\"com.docker.compose.oneoff\":\"False\",\"com.docker.compose.project\":\"redis\",\"com.docker.compose.project.config_files\":\"/home/naser/dockers/redis/docker-compose.yml\",\"com.docker.compose.project.working_dir\":\"/home/naser/dockers/redis\",\"com.docker.compose.replace\":\"08d2097466a7664b45319772636969b392221ab55ae767109f1ccb34a4525000\",\"com.docker.compose.service\":\"redis\",\"com.docker.compose.version\":\"2.21.0\"},\"Mounts\":[{\"Destination\":\"/data\",\"Mode\":\"rw\",\"Propagation\":\"rprivate\",\"RW\":true,\"Source\":\"/home/naser/dockers/redis/data\",\"Type\":\"bind\"}],\"Names\":[\"/redis\"],\"NetworkSettings\":{\"Networks\":{\"redis_default\":{\"Aliases\":null,\"DriverOpts\":null,\"EndpointID\":\"0a317e44ad75f8b622459850fb64cd3d32209f12c5a4059962c29a5e5529605c\",\"Gateway\":\"172.18.0.1\",\"GlobalIPv6Address\":\"\",\"GlobalIPv6PrefixLen\":0,\"IPAddress\":\"172.18.0.2\",\"IPAMConfig\":null,\"IPPrefixLen\":16,\"IPv6Gateway\":\"\",\"Links\":null,\"MacAddress\":\"02:42:ac:12:00:02\",\"NetworkID\":\"3e1687d10884c55152aa0cc27e623d5acb1cc971b666486d8809d4b98e189fe2\"}}},\"Ports\":[{\"IP\":\"0.0.0.0\",\"PrivatePort\":6379,\"PublicPort\":6379,\"Type\":\"tcp\"},{\"IP\":\"::\",\"PrivatePort\":6379,\"PublicPort\":6379,\"Type\":\"tcp\"}],\"State\":\"running\",\"Status\":\"Up 3 seconds\"}]";

    [Fact]
    public void Parse_Return_Multiple_Success()
    {
        //arrange
        var jsonData = JsonSerializer.Deserialize<List<ContainerRaw>>(RawResultMultiple);
        
        //act
        var convertor = ContainersListParser.Parse(jsonData!);

        //assert
        Assert.NotEmpty(convertor);
        Assert.True(convertor.Count == 2);
        Assert.Equal("redis:latest", convertor[0].Image);
        Assert.NotEmpty(convertor[0].Labels);
        Assert.NotEmpty(convertor[0].Ports);
        Assert.Equal("redis_default", convertor[0].Networks[0]);
    }    
    
    [Fact]
    public void Parse_Return_Single_Success()
    {
        //arrange
        var jsonData = JsonSerializer.Deserialize<List<ContainerRaw>>(RawResultSingle);

        //act
        var convertor = ContainersListParser.Parse(jsonData!);

        //assert
        Assert.NotEmpty(convertor);
        Assert.True(convertor.Count == 1);
        Assert.Equal("redis:latest", convertor[0].Image);
        Assert.NotEmpty(convertor[0].Labels);
        Assert.Equal("redis_default", convertor[0].Networks[0]);
    }
    
    [Fact]
    public void Parse_Return_Empty()
    {
        //arrange
        var jsonDataEmpty =new List<ContainerRaw>(0);

        //act
        var convertor = ContainersListParser.Parse(jsonDataEmpty);

        //assert
        Assert.Empty(convertor);
    }
}
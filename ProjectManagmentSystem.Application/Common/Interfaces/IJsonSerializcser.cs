namespace ProjectManagementSystem.Application.Common.Interfaces;

public interface IJsonSerializcser
{
    string Serialize<T>(T value);
    T Deserialize<T>(string value);
}

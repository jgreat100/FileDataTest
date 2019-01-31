namespace FileData
{
    public interface IFileDetailsService
    {
        int Size(string filePath);
        string Version(string filePath);
    }
}
using ThirdPartyTools;

namespace FileData
{
    public class FileDetailsService : IFileDetailsService
    {
        private FileDetails _fileDetails;

        public FileDetailsService()
        {
            _fileDetails = new FileDetails();
        }

        public string Version(string filePath)
        {
            return _fileDetails.Version(filePath);
        }

        public int Size(string filePath)
        {
            return _fileDetails.Size(filePath);
        }
    }
}

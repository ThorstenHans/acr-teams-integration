namespace Thns.Functions
{
    public class ImageMetadata
    {
        public string Registry { get; private set; }
        public string Tag { get; private set; }
        public string Repository { get; private set; }

        public static ImageMetadata FromPayload(dynamic d)
        {

            string repository = d?.target?.repository;
            string tag = d?.target?.tag;
            string registry = d?.request?.host;
            if (
                    string.IsNullOrWhiteSpace(repository) ||
                    string.IsNullOrWhiteSpace(tag) ||
                    string.IsNullOrWhiteSpace(registry))
            {
                return null;

            }
            return new ImageMetadata
            {
                Registry = registry,
                Repository = repository,
                Tag = tag,
            };
        }
    }
}

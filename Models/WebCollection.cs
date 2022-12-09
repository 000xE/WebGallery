using System.Text;

namespace WebGallery.Models
{
    public class WebCollection : BaseEntity
    {
        public string Name { get; set; }

        public string OriginalFilePath { get; set; }

        public string ResourceFolderPath { get; set; }

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append(this.Id);
            stringBuilder.Append(" - ");
            stringBuilder.Append(this.Name);

            return stringBuilder.ToString();
        }
    }
}

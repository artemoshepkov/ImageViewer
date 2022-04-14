using Avalonia.Media.Imaging;

namespace Gallery.Models
{
    public class MyImage : MyFile
    {
        public string ParentPath { get; }

        public Bitmap Image { get; }

        public MyImage(string path)
        {
            Path = path;
            Image = new Bitmap(Path);
        }

        public MyImage(string name, string path, string parentPath) : this(path)
        {
            Name = name;
            ParentPath = parentPath;
        }
    }
}

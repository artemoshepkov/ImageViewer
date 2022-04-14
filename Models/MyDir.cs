using System.Collections.ObjectModel;

namespace Gallery.Models
{
    public class MyDir : MyFile
    {
        public bool IsInit { get; set; } = false;

        public ObservableCollection<MyFile> SubDirs { get; set; }

        public MyDir()
        {
            SubDirs = new ObservableCollection<MyFile>();
        }

        public MyDir(string name, string path)
        {
            Name = name;
            Path = path;
            SubDirs = new ObservableCollection<MyFile>();
        }
    }
}

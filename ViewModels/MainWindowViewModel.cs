using System.Collections.Generic;
using Gallery.Models;
using System.Collections.ObjectModel;
using System.IO;
using ReactiveUI;

namespace Gallery.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<MyFile> DirList { get; } = new ObservableCollection<MyFile>();

        private ObservableCollection<MyImage> _showingImages = new ObservableCollection<MyImage>();
        public ObservableCollection<MyImage> ShowingImages
        {
            get
            {
                return _showingImages;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _showingImages, value);
            }
        }

        public MainWindowViewModel()
        {
            MyGetDrives();
        }

        private void MyGetDrives()
        {
            List<DriveInfo> drives = new List<DriveInfo>(DriveInfo.GetDrives());

            foreach (DriveInfo drive in drives)
            {
                if (drive.IsReady)
                {
                    DirList.Add(new MyDir(drive.Name, drive.Name));
                }
            }
        }

        public void UpdateShowingImages(object obj)
        {
            var file = obj as MyFile;

            string[] fileExtensions = new string[] { "ico", "png", "jpeg", "jpg" };

            List<MyImage> newImages = new List<MyImage>();

            if (ShowingImages != null)
                ShowingImages.Clear();
            else
                ShowingImages = new ObservableCollection<MyImage>();

            try
            {
                DirectoryInfo directory;
                if (file is MyImage im)
                {
                    directory = new DirectoryInfo(im.ParentPath);
                }
                else
                {
                    directory = new DirectoryInfo(file.Path);
                }

                if (directory.Exists)
                {
                    if (file is MyDir dir)
                    {
                        if (!dir.IsInit)
                        {
                            DirectoryInfo[] dirsInfo = directory.GetDirectories();
                            foreach (DirectoryInfo d in dirsInfo)
                            {
                                dir.SubDirs.Add(new MyDir(d.Name, d.FullName));
                            }

                            FileInfo[] locallyImages;

                            foreach (string ext in fileExtensions)
                            {
                                locallyImages = directory.GetFiles($"*.{ext}");
                                foreach (FileInfo i in locallyImages)
                                {
                                    dir.SubDirs.Add(new MyImage(i.Name, i.FullName, i.DirectoryName));
                                }
                            }

                            dir.IsInit = true;
                        }
                    }

                    FileInfo[] images;

                    foreach (string ext in fileExtensions)
                    {
                        images = directory.GetFiles($"*.{ext}");
                        foreach (FileInfo i in images)
                        {
                            ShowingImages.Add(new MyImage(i.FullName));
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}


using DMSkin.CloudMusic.Model;
using DMSkin.CloudMusic.Controls;
using DMSkin.Core.Common;
using DMSkin.Core.MVVM;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;
using Ookii.Dialogs.Wpf;
using System.Runtime.Versioning;

namespace DMSkin.CloudMusic.ViewModel
{
    public class PageLocalMusicViewModel : MusicViewModelBase
    {
        public PageLocalMusicViewModel()
        {
            Read();
        }

        /// <summary>
        /// 读取配置
        /// </summary>
        public void Read()
        {
            if (File.Exists("LocalMusic.json"))
            {
                //尝试从 配置文件中读取 上次 增加的 播放列表
                string json = File.ReadAllText("LocalMusic.json");
                if (!string.IsNullOrEmpty(json))
                {
                    List<Music> list = JsonConvert.DeserializeObject<List<Music>>(json);
                    foreach (var item in list)
                    {
                        MusicList.Add(item);
                    }
                    //显示歌曲列表
                    ShowMusicList();
                }
            }
        }

        /// <summary>
        /// 写入配置
        /// </summary>
        public void Save()
        {

            string json = JsonConvert.SerializeObject(MusicList);
            File.WriteAllText("LocalMusic.json", json);
        }

     
        /// <summary>
        /// 新增文件夹
        /// </summary> 
         [SupportedOSPlatform("windows")]
        public ICommand AddFileCommand
        {
            get
            {
               
                return new DelegateCommand(obj =>
                {


                    var dialog = new VistaFolderBrowserDialog();
                    dialog.Description = "请选择文件夹.";
                    dialog.UseDescriptionForTitle = true;  

                     

                    if ((bool)dialog.ShowDialog( ))
                    {
                        Task.Run(() =>
                        {
                            string[] filelist = Directory.GetFiles(dialog.SelectedPath);
                            FindMusic(filelist.ToList());
                        });
                    }
                    
                });
            }
        }


        private void FindMusic(List<string> filelist)
        {
            int index = 1;
            List<Music> TempList = new List<Music>();
            foreach (var item in filelist)
            {
                FileInfo file = new(item);

                MP3Info m = new();
                m.GetMP3Info(file.DirectoryName, file.Name);

                if (file.Extension.Contains(".mp3"))
                {
                    TempList.Add(new Music()
                    {
                        Title = m.trackName,
                        Size = file.Length,
                        Index = index.ToString(),
                        Url = file.FullName,
                        Album = m.Album,
                        Artist = m.Artist,
                        TimeStr = m.time
                    });
                    index++;
                }
            }
            if (TempList.Count > 0)
            {
                Execute.OnUIThread(() =>
                {
                    //清空上次的文件夹,防止重复
                    MusicList.Clear();
                    foreach (var item in TempList)
                    {
                        if (MusicList.Where(p => p.Title == item.Title).Count() == 0)
                        {
                            MusicList.Add(item);
                        }
                    }
                    //显示歌曲列表
                    ShowMusicList();
                    //保存找到的歌曲
                    Save();
                     
                });
            }
        }
    }
}

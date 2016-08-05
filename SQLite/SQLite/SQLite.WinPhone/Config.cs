using SQLite.Net.Interop;
using SQLite.Net.Platform.WinRT;
using Windows.Storage;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite.WinPhone.Config))]


namespace SQLite.WinPhone
{
    public class Config : IConfig
    {
        private string directorioDB;
        private ISQLitePlatform plataforma;

        public string DirectorioDB
        {
            get
            {
                if (string.IsNullOrEmpty(directorioDB))
                {
                    directorioDB = ApplicationData.Current.LocalFolder.Path;
                }
                return directorioDB;
            }
        }

        public ISQLitePlatform Plataforma
        {
            get
            {
                if (plataforma == null)
                {
                    //plataforma = new SQLite.Net.Platform.WindowsPhone8.SQLi
                    //plataforma = new Net.Platform.WindowsPhone8.SQLitePlatformWP8();
                    plataforma = new Net.Platform.WinRT.SQLitePlatformWinRT();
                    //plataforma = new SQLitePlatformWinRT();
                }
                return plataforma;
            }
        }
    }
}


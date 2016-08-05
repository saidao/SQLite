using System;
using SQLite.Net.Interop;

namespace SQLite
{
    public interface IConfig
    {
        string DirectorioDB { get; }
        ISQLitePlatform Plataforma { get; }
    }

}

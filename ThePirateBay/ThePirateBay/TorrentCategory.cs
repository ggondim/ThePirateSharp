using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePirateBay
{
    public class TorrentCategory
    {
        public const int All = 0;
        public const int AllAudio = 100;
        public const int AllVideo = 200;
        public const int AllApplication = 300;
        public const int AllGames = 400;
        public const int AllPorn = 500;
        public const int AllOther = 600;

        public class Audio
        {
            public const int Music = 101;
            public const int Audiobooks = 102;
            public const int Soundclips = 103;
            public const int FLAC = 104;
            public const int Other = 199;
        }

        public class Video
        {
            public const int Movies = 201;
            public const int MoviesDVDR = 202;
            public const int Musicvideos = 203;
            public const int Movieclips = 204;
            public const int TVshows = 205;
            public const int Handheld = 206;
            public const int HDMovies = 207;
            public const int HDTVshows = 208;
            public const int Movies3D = 209;
            public const int Other = 299;
        }

        public class Applications
        {
            public const int Windows = 301;
            public const int Mac = 302;
            public const int UNIX = 303;
            public const int Handheld = 304;
            public const int IOS = 305;
            public const int Android = 306;
            public const int OtherOS = 399;
        }

        public class Games
        {
            public const int PC = 401;
            public const int Mac = 402;
            public const int PSx = 403;
            public const int XBOX360 = 404;
            public const int Wii = 405;
            public const int Handheld = 406;
            public const int IOS = 407;
            public const int Android = 408;
            public const int Other = 499;
        }

        public class Porn
        {
            public const int Movies = 501;
            public const int MoviesDVDR = 502;
            public const int Pictures = 503;
            public const int Games = 504;
            public const int HDMovies = 505;
            public const int Movieclips = 506;
            public const int Other = 599;
        }

        public class Other
        {
            public const int Ebooks = 601;
            public const int Comics = 602;
            public const int Pictures = 603;
            public const int Covers = 604;
            public const int Physibles = 605;
            public const int OtherOther = 699;
        }
    }
}

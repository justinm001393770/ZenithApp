using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZenithApp1.Models.APIModels.Twitch;

namespace ZenithApp1.ViewModels.Twitch
{
    public class TwitchViewModel
    {
        public IList<Stream> streams { get; set; }
    }
}
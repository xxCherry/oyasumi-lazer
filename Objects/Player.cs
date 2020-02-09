using System;
using System.Collections.Generic;
using System.Text;
using oyasumi_lazer.Handlers;

namespace oyasumi_lazer.Objects
{
    public class Player
    {
        private long _userId;
        OAuthScheme _OAuth;
        public Player(long userID, OAuthScheme OAuth)
        {
            _userId = userID;
            _OAuth = OAuth;
        }
    }
}

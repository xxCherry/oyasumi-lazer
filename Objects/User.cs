using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using oyasumi_lazer.Handlers;

namespace oyasumi_lazer.Objects
{
    public class UserStatistics
    {
        [JsonProperty]
        public User User;

        [JsonProperty(@"level")]
        public LevelInfo Level;

        public struct LevelInfo
        {
            [JsonProperty(@"current")]
            public int Current;

            [JsonProperty(@"progress")]
            public int Progress;
        }

        [JsonProperty(@"pp")]
        public decimal? PP;

        [JsonProperty(@"pp_rank")] // the API sometimes only returns this value in condensed user responses
        private int? rank
        {
            set => Ranks.Global = value;
        }

        [JsonProperty(@"rank")]
        public UserRanks Ranks;

        [JsonProperty(@"ranked_score")]
        public long RankedScore;

        [JsonProperty(@"hit_accuracy")]
        public decimal Accuracy;


        [JsonProperty(@"play_count")]
        public int PlayCount;

        [JsonProperty(@"play_time")]
        public int? PlayTime;

        [JsonProperty(@"total_score")]
        public long TotalScore;

        [JsonProperty(@"total_hits")]
        public int TotalHits;

        [JsonProperty(@"maximum_combo")]
        public int MaxCombo;

        [JsonProperty(@"replays_watched_by_others")]
        public int ReplaysWatched;

        [JsonProperty(@"grade_counts")]
        public Grades GradesCount;

        public struct Grades
        {
            [JsonProperty(@"ssh")]
            public int? SSPlus;

            [JsonProperty(@"ss")]
            public int SS;

            [JsonProperty(@"sh")]
            public int? SPlus;

            [JsonProperty(@"s")]
            public int S;

            [JsonProperty(@"a")]
            public int A;
        }

        public struct UserRanks
        {
            [JsonProperty(@"global")]
            public int? Global;

            [JsonProperty(@"country")]
            public int? Country;
        }

        public User.RankHistoryData RankHistory;
    }
    public class Country : IEquatable<Country>
    {
        /// <summary>
        /// The name of this country.
        /// </summary>
        [JsonProperty(@"name")]
        public string FullName;

        /// <summary>
        /// Two-letter flag acronym (ISO 3166 standard)
        /// </summary>
        [JsonProperty(@"code")]
        public string FlagName;

        public bool Equals(Country other) => FlagName == other?.FlagName;
    }
    public class User
    {
        [JsonProperty(@"id")]
        public long Id = 1;

        [JsonProperty(@"join_date")]
        public DateTimeOffset JoinDate;

        [JsonProperty(@"username")]
        public string Username;

        [JsonProperty(@"previous_usernames")]
        public string[] PreviousUsernames;

        [JsonProperty(@"country")]
        public Country Country;


        //public Team Team;

        [JsonProperty(@"profile_colour")]
        public string Colour;

        [JsonProperty(@"avatar_url")]
        public string AvatarUrl;

        [JsonProperty(@"cover_url")]
        public string CoverUrl
        {
            get => Cover?.Url;
            set => Cover = new UserCover { Url = value };
        }

        [JsonProperty(@"cover")]
        public UserCover Cover;

        public class UserCover
        {
            [JsonProperty(@"custom_url")]
            public string CustomUrl;

            [JsonProperty(@"url")]
            public string Url;

            [JsonProperty(@"id")]
            public int? Id;
        }

        [JsonProperty(@"is_admin")]
        public bool IsAdmin;

        [JsonProperty(@"is_supporter")]
        public bool IsSupporter;

        [JsonProperty(@"support_level")]
        public int SupportLevel;

        [JsonProperty(@"is_gmt")]
        public bool IsGMT;

        [JsonProperty(@"is_qat")]
        public bool IsQAT;

        [JsonProperty(@"is_bng")]
        public bool IsBNG;

        [JsonProperty(@"is_bot")]
        public bool IsBot;

        [JsonProperty(@"is_active")]
        public bool Active;

        [JsonProperty(@"is_online")]
        public bool IsOnline;

        [JsonProperty(@"pm_friends_only")]
        public bool PMFriendsOnly;

        [JsonProperty(@"interests")]
        public string Interests;

        [JsonProperty(@"occupation")]
        public string Occupation;

        [JsonProperty(@"title")]
        public string Title;

        [JsonProperty(@"location")]
        public string Location;

        [JsonProperty(@"last_visit")]
        public DateTimeOffset? LastVisit;

        [JsonProperty(@"twitter")]
        public string Twitter;

        [JsonProperty(@"lastfm")]
        public string Lastfm;

        [JsonProperty(@"skype")]
        public string Skype;

        [JsonProperty(@"discord")]
        public string Discord;

        [JsonProperty(@"website")]
        public string Website;

        [JsonProperty(@"post_count")]
        public int PostCount;

        [JsonProperty(@"follower_count")]
        public int FollowerCount;

        [JsonProperty(@"favourite_beatmapset_count")]
        public int FavouriteBeatmapsetCount;

        [JsonProperty(@"graveyard_beatmapset_count")]
        public int GraveyardBeatmapsetCount;

        [JsonProperty(@"loved_beatmapset_count")]
        public int LovedBeatmapsetCount;

        [JsonProperty(@"ranked_and_approved_beatmapset_count")]
        public int RankedAndApprovedBeatmapsetCount;

        [JsonProperty(@"unranked_beatmapset_count")]
        public int UnrankedBeatmapsetCount;

        [JsonProperty(@"scores_first_count")]
        public int ScoresFirstCount;

        [JsonProperty]
        private string[] playstyle
        {
            set => PlayStyles = value?.Select(str => Enum.Parse(typeof(PlayStyle), str, true)).Cast<PlayStyle>().ToArray();
        }

        public PlayStyle[] PlayStyles;

        [JsonProperty(@"playmode")]
        public string PlayMode;

        [JsonProperty(@"profile_order")]
        public string[] ProfileOrder;

        [JsonProperty(@"kudosu")]
        public KudosuCount Kudosu;

        public class KudosuCount
        {
            [JsonProperty(@"total")]
            public int Total;

            [JsonProperty(@"available")]
            public int Available;
        }

        [JsonProperty(@"statistics")]
        public UserStatistics Statistics;

        public class RankHistoryData
        {
            [JsonProperty(@"mode")]
            public string Mode;

            [JsonProperty(@"data")]
            public int[] Data;
        }

        [JsonProperty(@"rankHistory")]
        private RankHistoryData rankHistory
        {
            set => Statistics.RankHistory = value;
        }

        [JsonProperty("badges")]
        public Badge[] Badges;

        [JsonProperty("user_achievements")]
        public UserAchievement[] Achievements;

        public class UserAchievement
        {
            [JsonProperty("achieved_at")]
            public DateTimeOffset AchievedAt;

            [JsonProperty("achievement_id")]
            public int ID;
        }

        [JsonProperty("monthly_playcounts")]
        public UserHistoryCount[] MonthlyPlaycounts;

        [JsonProperty("replays_watched_counts")]
        public UserHistoryCount[] ReplaysWatchedCounts;

        public class UserHistoryCount
        {
            [JsonProperty("start_date")]
            public DateTime Date;

            [JsonProperty("count")]
            public long Count;
        }

        public class Badge
        {
            [JsonProperty("awarded_at")]
            public DateTimeOffset AwardedAt;

            [JsonProperty("description")]
            public string Description;

            [JsonProperty("image_url")]
            public string ImageUrl;
        }

        public enum PlayStyle
        {
            Keyboard,
            Mouse,
            Tablet,
            Touch,
        }
    }
}

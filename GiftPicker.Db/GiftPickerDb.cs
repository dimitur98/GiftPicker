using Base.Db;
using GiftPicker.Db.Dal;

namespace GiftPicker.Db
{
    public class GiftPickerDb : BaseDb
    {
        private static GiftPickerDb _instance = null;

        private Gifts _gifts;
        private Users _users;
        private UserVotings _userVotings;
        private Votes _votes;

        public static Gifts Gifts => _instance._gifts ??= new Gifts(_instance);
        public static Users Users => _instance._users ??= new Users(_instance);
        public static UserVotings UserVotings => _instance._userVotings ??= new UserVotings(_instance);
        public static Votes Votes => _instance._votes ??= new Votes(_instance);

        private GiftPickerDb(string connectionString, int? unableToConnectToHostErrorRetryInterval = null, bool unableToConnectToHostErrorRetryTillConnect = false)
            : base(connectionString, unableToConnectToHostErrorRetryInterval: unableToConnectToHostErrorRetryInterval, unableToConnectToHostErrorRetryTillConnect: unableToConnectToHostErrorRetryTillConnect)
        {
        }

        public static void SetupConnection(string connectionString, int? unableToConnectToHostErrorRetryInterval = null, bool unableToConnectToHostErrorRetryTillConnect = false)
        {
            _instance = new GiftPickerDb(connectionString, unableToConnectToHostErrorRetryInterval: unableToConnectToHostErrorRetryInterval, unableToConnectToHostErrorRetryTillConnect: unableToConnectToHostErrorRetryTillConnect);
        }
    }
}

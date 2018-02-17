using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aymk_engine.Util
{
    public class Constants
    {
        public enum NotificationTypes
        {
            EMAIL = 1,
            MOBILEPHONE = 2,
            SMS = 3

        }

        public enum NotificationStyles
        {
            INFO = 1,
            WARNING = 2,
            DANGER = 3,

        }
        public enum NotificationStatus
        {
            WAITING = 1,
            SENT = 2,
            HASERROR = 3,
        }

        public enum NotificationTitleType
        {
            PRICE_BELOW = 1,
            PRICE_ABOWE = 2,
            PERCENTANGE_ABOWE = 3,
            PERCENTANGE_BELOW = 4
        }

        public enum AlertValueType
        {
            price,
            percent

        }
    }
}

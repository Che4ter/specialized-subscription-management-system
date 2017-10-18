using essentialAdmin.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;

namespace essentialAdmin.Extensions
{

        public static class NotificationExtensions
        {
            private static IDictionary<String, String> NotificationKey = new Dictionary<String, String>
        {
            { "Error",      "App.Notifications.Error" },
            { "Warning",    "App.Notifications.Warning" },
            { "Success",    "App.Notifications.Success" },
            { "Info",       "App.Notifications.Info" }
        };


            public static void AddNotification(this BaseController controller, String message, String notificationType)
            {
                string NotificationKey = getNotificationKeyByType(notificationType);
                ICollection<String> messages = controller.TempData[NotificationKey] as ICollection<String>;

                if (messages == null)
                {
                    controller.TempData[NotificationKey] = (messages = new HashSet<String>());
                }

                messages.Add(message);
            }

            public static IEnumerable<String> GetNotifications(this ViewContext context, String notificationType)
            {
                string NotificationKey = getNotificationKeyByType(notificationType);
                return context.TempData[NotificationKey] as ICollection<String> ?? null;
            }

            private static string getNotificationKeyByType(string notificationType)
            {
                try
                {
                    return NotificationKey[notificationType];
                }
                catch (IndexOutOfRangeException e)
                {
                    ArgumentException exception = new ArgumentException("Key is invalid", "notificationType", e);
                    throw exception;
                }
            }
        }

        public static class NotificationType
        {
            public const string ERROR = "Error";
            public const string WARNING = "Warning";
            public const string SUCCESS = "Success";
            public const string INFO = "Info";

        }
}

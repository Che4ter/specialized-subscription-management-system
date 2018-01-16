using esencialAdmin.Controllers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace esencialAdmin.Extensions
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
            if (controller.TempData != null)
            {
                string NotificationKey = getNotificationKeyByType(notificationType);
                HashSet<String> notify;
                if (controller.TempData[NotificationKey] == null)
                {
                    notify = new HashSet<String>();
                }
                else
                {
                    notify = JsonConvert.DeserializeObject<HashSet<String>>(controller.TempData[NotificationKey].ToString());
                }
                notify.Add(message);
                controller.TempData[NotificationKey] = JsonConvert.SerializeObject(notify);
            }
        }

        public static IEnumerable<String> GetNotifications(this ViewContext context, String notificationType)
        {
            string NotificationKey = getNotificationKeyByType(notificationType);
            if (context.TempData[NotificationKey] != null)
            {
                var tmp = JsonConvert.DeserializeObject<IEnumerable<String>>(context.TempData[NotificationKey].ToString());
                return tmp;
            }
            else
            {
                return null;
            }
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
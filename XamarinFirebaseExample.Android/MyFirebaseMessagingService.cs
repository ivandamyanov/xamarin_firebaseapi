using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;

namespace XamarinFirebaseExample.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        private const string defaultNotificationTitle = "App name";
        private const string newsChannelId = "1"; // not sure I just use 1 you should be able to use anything as long as its unique per channel
        private const string newsChannelDescription = "Channel name shown to user e.g. News";
        private long[] vibrationPattern = { 500, 500, 500, 500, 500, 500, 500, 500, 500 };
        private NotificationManager notificationManager;

        public override void OnNewToken(string newToken)
        {
            base.OnNewToken(newToken);
            Log.Info("MyFirebaseMessagingService", "Firebase Token: " + newToken);
            saveRegistrationToApp(newToken);
        }

        public override void OnMessageReceived(RemoteMessage remoteMessage)
        {
            base.OnMessageReceived(remoteMessage);
            // depending on how you send the notifications you might get the message as per documentation
            // using remoteMessage.getNotification().getBody()
            var message = remoteMessage.Data["message"];
            Log.Debug("MyFirebaseMessagingService", "From:    " + remoteMessage.From);
            Log.Debug("MyFirebaseMessagingService", "Message: " + message);

            sendNotification(defaultNotificationTitle, message);
        }

        private void sendNotification(string title, string message)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.SingleTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);
            notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);

            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                NotificationImportance importance = NotificationImportance.High;
                NotificationChannel notificationChannel = new NotificationChannel(newsChannelId, newsChannelDescription, importance);
                notificationChannel.EnableLights(true);
                notificationChannel.LightColor = Color.Red;
                notificationChannel.EnableVibration(true);
                notificationChannel.SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification), new AudioAttributes.Builder()
            .SetContentType(AudioContentType.Sonification)
            .SetUsage(AudioUsageKind.Notification)
            .Build());
                notificationManager.CreateNotificationChannel(notificationChannel);
            }

            Notification notification = new NotificationCompat.Builder(this, newsChannelId)
                .SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Mipmap.icon)) //newest projects use mipmaps and drawables, you can go to drawables too
                .SetSmallIcon(Resource.Mipmap.icon)
                .SetContentTitle(title)
                .SetContentText(message)
                .SetAutoCancel(true)
                .SetVisibility((int)NotificationVisibility.Private)
                .SetContentIntent(pendingIntent)
                .SetVibrate(vibrationPattern)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                .Build();

            notificationManager.Notify(0, notification); // overrides old notification if it's still visible because it uses same Id
        }

        private void saveRegistrationToApp(string token)
        {
           // send to server or store locally
        }
    }
}
# Xamarin Android - sending push notifications and updating token using the newest Firebase API
An example project for using the newest APIs to send push notifications using Firebase

Recently Firebase made the FirebaseInstanceIdService deprecated and now we are supposed to use the FirebaseMessagingService
to override the onNewToken method instead of the also deprecated onTokenRefresh method.

Xamarin documentation is out of date so here are what changes I did for it to work, the android manifest is a lot cleaner now too.

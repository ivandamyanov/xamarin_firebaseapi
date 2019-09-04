# xamarin_firebaseapi
An example project for using the newest APIs to send push notifications using the Firebase

Recently Firebase made the firebaseinstanceidservice deprecated and now we are supposed to use the FirebaseMessagingService
to override the onNewToken method instead of the also deprecated onTokenRefresh method.

Xamarin documentation is out of date so here's what changes I did for it to work, the android manifest is a lot cleaner now too.

# CircularUIMediaPlayer

The **CircularUIMediaPlayer** application shows how to build a video player for Tizen Wearables.

You can get detail information about `how to build a video player for Tizen Wearables` from [here][circularui_mediaplayer].

## Circular UI MediaView & MediaPlayer APIs

- [MediaView][API-MediaView]
- [MediaPlayer][API-MediaPlayer]
- [MediaSource][API-MediaSource]

## [How to test it](#how_to_test)

 - Precondition: Network connectivity is available.

The MainPage looks like below.

 ![][mainpage]

 If you want to play a local media source, press the `Local Resource` purple button. 

 Otherwise, this application plays the video from the external URL.

| Local Resource | External URL  |
| :-: | :-: |
| ![][local_resource] | ![][external_resource] |


In this application, [`UsesEmbeddingControls`][UsesEmbeddingControls] is used in case of `Local Resource`. Plus, it automatically plays a video file because [`AutoPlay`][AutoPlay]  is true.

However, in `External URL`, [`AutoPlay`][AutoPlay] is not applied so you need to touch `play` button in `custom control` view to play it.

In addition, you can jump the video forward or backward 10 seconds by pressing `forward` or `backward` button. ([`Seek(int ms)`][Method-Seek])


| Use Embedding Controls | Without Embedding Controls / Make a Custom Control  |
| :-----: | :-: |
| ![][ApplyUsesEmbeddingControls] | ![][DoesNotUseEmbeddingControls] |


[circularui_mediaplayer]: https://samsung.github.io/Tizen.NET/wearables/CircularUI-MediaPlayer
[API-MediaView]: https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.MediaView.html
[API-MediaPlayer]: https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.MediaPlayer.html
[API-MediaSource]: https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.MediaSource.html
[Method-Seek]: https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.MediaPlayer.html#Tizen_Wearable_CircularUI_Forms_MediaPlayer_Seek_System_Int32_
[UsesEmbeddingControls]: https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.MediaPlayer.html#Tizen_Wearable_CircularUI_Forms_MediaPlayer_UsesEmbeddingControls
[AutoPlay]: https://samsung.github.io/Tizen.CircularUI/api/Tizen.Wearable.CircularUI.Forms.MediaPlayer.html#Tizen_Wearable_CircularUI_Forms_MediaPlayer_AutoPlay
[mainpage]: ./screenshots/circularuimediaplayer_mainpage.png
[local_resource]: ./screenshots/circularuimediaplayer_local_resource.png
[external_resource]: ./screenshots/circularuimediaplayer_external_resource.png
[ApplyUsesEmbeddingControls]: ./screenshots/circularuimediaplayer_using_embedding_controls.png
[DoesNotUseEmbeddingControls]: ./screenshots/circularuimediaplayer_using_custom_controller_instead_of_embedding_controls.png

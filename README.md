# UniPrefabLayoutGroupTest

プレハブにアタッチされている LayoutGroup と ContentSizeFitter の enabled が true になっていないかテストできる機能

## 使い方

![2020-09-06_135114](https://user-images.githubusercontent.com/6134875/92318495-18a50180-f048-11ea-83fc-d0f23a3408a3.png)

プレハブに LayoutGroup と ContentSizeFitter がアタッチされており、  
どちらの enabled も true になっている場合、  

![2020-09-06_135133](https://user-images.githubusercontent.com/6134875/92318496-193d9800-f048-11ea-9f24-fa0146db2a28.png)

Unity Test Runner でテストに失敗します  

## 補足

* プレハブにアタッチされている LayoutGroup と ContentSizeFitter の enabled が true になっていると  
シーンを開いた時にそのプレハブのインスタンスが毎回変更状態になってしまい、  
RectTransform の値がオーバーライドされてしまうことで、  
プレハブ側を編集してもインスタンス側に編集が反映されないことがあったため  
そのような状況を防ぐためにテストできるようにしました  
